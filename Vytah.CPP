#include<stdio.h>
#include<dos.h>
#include<conio.h>

#define p1 0x300
#define p2 0x301

int MotUp= 0xF2;	//pohyb motoru + signalizace + svetlo
int MotDown= 0xE8;	//pohyb motoru + signalizace + svetlo
int MotSt =0xFF;	//stop motoru
int Dver = 0x20;	//maska pro dvere
int Podl = 0x40;	//maska pro podlahovy snimac
bool Jede = false;	//signalizace zda je vytah v pohybu
int led;			//BCD kod
int input;			//vstup
int hold;			//ulozeni vstupu
int i;
int k;
int fin=0;			//kam chceme dojet
int apos;			//aktualni pozice vytahu
int stat=0;
int zvukON=0xEF;	//zapnuti zvuku
int zvukOF=0x10;	//vypnuti zvuku
int svetON=0xFB;	//svetlo sviti
int svetOF=0x04;	//svetlo zhasnuto
int outp;			//vystup

int tab[4][3]=		//tlaèítka
{
{0x01,0x10,1},
{0x02,0x20,2},
{0x04,0x40,3},
{0x08,0x80,4},
};
int pos[4][3]=		//èidla
{	
{0x01,1,0xF1},
{0x02,2,0xF2},
{0x04,3,0xF3},
{0x08,4,0xF4},
};

int main (void)
{
outportb(p1,MotSt);
while(1)
{
switch (stat)
{
	case 0:		//kontrola dveøí
	input = inportb(p2);
	input &= Dver;
	if (input == 0)
	{	//dvere zavrene
		stat = 1;
	}
	else
	{	//otevrene dvere
		stat = 0;
		outp = MotSt & svetON;
		outportb(p1,outp);
		Jede = false;
	}
	break;

	case 1:	//kontrola pohybu
	if (Jede)
	{
		input = inportb(p2);
		for (k=0;k<4;k++)
		{
			hold = input & pos[k][0];
			if (hold == 0)
			{
				led = pos[k][2];
                outportb(p2,led);
				apos = pos[k][1];
				if (apos==fin)
				{										
					led = led & zvukON;	//zapnuti zvuku
					outportb(p2,led);
					outp= MotSt | svetOF;
					outportb(p1,outp);
					Jede = false; 
					delay(100);
					led = led | zvukOF;	//vypnuti zvuku
					outportb(p2,led);
					stat = 0;
				}
			}
		}		
	}
	else
	{
		stat = 2;
	}
	break;


	case 2:	//je nekdo v kabine
	input = inportb(p2);
	input &= Podl;
	if (input == 0)
	{
		stat = 3;
		outp = MotSt & svetON;
		outportb(p1,outp);
	}
	else 
	{
		outportb(p1,MotSt);
		stat = 4;
	}
	break;


	case 3:	//tlacitka uvnitr kabiny
	input = inportb(p1);
	for (i=0;i<4;i++)
	{
		hold = input & tab[i][0];
		if (hold == 0)
		{
			fin = tab[i][2];
			stat = 5;
			break;
		}
		else
		{
			stat = 0;
		}
	}
	break;
	
	case 4:		//ovladni na patrech
	input = inportb(p1);
	for (i=0;i<4;i++)
	{
		hold = input & tab[i][1];
		if (hold == 0)
		{
			fin = tab[i][2];
			stat = 5;
            break;
		}
		else
		{
			stat = 0;
		}
	}
	break;

	case 5:		//urceni smeru
		if(fin<apos)
		{
			outportb(p1,MotDown);
		}
		else
		{
			outportb(p1,MotUp);
		}
	Jede = true;
	stat = 0;
	break;
}
}
}