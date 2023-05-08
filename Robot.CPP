#include <stdio.h>
#include <dos.h>
#include <conio.h>
#define PORT 0x300 

char stisk;
int klav;
int drz;
int input;
int pomoc;	
int i;
int x;

int speed=10;//rychlost otaceni
//doporucuji max 20

int zac[4][3]=
{
/*
1. a 2. sloupec - smer + motor s taktem
3. slupec - koncak
*/
	{0xED,0xEC,0x01},
	{0xDD,0xDC,0x02},
	{0xBD,0xBC,0x04},
	{0x7F,0x7E,0x08},
};
int rizeni[8][6]=
{
/*
1. sloupec - klavesa
2. sloupec - smer + motor
3. sloupec - koncak
4. sloupec - krok
5. sloupec - aktualni stav
6. sloupec - hodnota imaginarni zavory
*/
	{113,0xEF,0xFF,-1,2000,2000}, //q
	{ 97,0xED,0x01, 1,0xFF,0x00}, //a
	{119,0xDF,0xFF,-1,2000,2000}, //w
	{115,0xDD,0x02, 1,0xFF,0x00}, //s
	{101,0xBF,0xFF,-1, 500, 500}, //e
	{100,0xBD,0x04, 1,0xFF,0x00}, //d
	{102,0x7D,0xFF,-1,4000,4000}, //f
	{114,0x7F,0x08, 1,0xFF,0x00}, //r
	
};

int konec ()
{	//fce pro reset tabulky 
	delay(150);
	if(rizeni[i][3]<0)
	{	//pokud robot narazil na IR zavoru tak nul imaginarni
	rizeni[i][4]=rizeni[i][5];		
	}
 return 0;
}

int res ()
{	//robot do zakldani polohy
	printf("Robot se pohybuje do zakladnich poloh \n");
	for (x = 0; i < 4; x++)
	{
	input = inportb(PORT);
	input &= zac[x][2];
		while (input != 0)
		{
		outportb(PORT,zac[x][0]);
		delay(2);
		outportb(PORT,zac[x][1]);
		delay(2);
		input = inportb(PORT);
		input &= zac[x][2];
		}
	}
return 0;
}

int main (void)
{
	res();
	printf("Ovládejte pomocí Q A W S E D R F \n");
	while (1)
	{	
		stisk = getch();	//nacteni klavenice
		klav = (int)stisk;	//ulozeni stisknute klavesy
		for (i = 0; i < 8; i++)	//smycka pro kontrolu klavesnice
		{
			drz = rizeni[i][0];	//ulozeni aktualni zkoumane klavesnice
			while (drz == klav)	//kontrola zda se stisknuta klavesa shoduje 
			{
				if (i%2==1) //kontrola zda je i liche 
				{pomoc=i-1;}	//posun o -1
				else
				{pomoc=i;}	

				for (int k = 0; k < speed;k++)
				{		//cyklus pro zrychleni otacek
				input = inportb(PORT);
				input &= rizeni[i][2];
				if (input == 0 || rizeni[i][4] == 0)
				{		//kontrola zda robot jiz neni v meznich hodnotach
					printf("V tomto smeru uz dal nemuze \n");
					konec();	
					break;
				}
				else
				{	//ovladani motoru
				 int takt = rizeni[i][1] & 0xFE;
				 outportb(PORT,takt);
				 delay(2);
				 takt = rizeni[i][1] | 0x01;
				 outportb(PORT,takt);
				 delay(1);
				 rizeni[pomoc][4]=rizeni[pomoc][4]+rizeni[i][3];
				}
				}
			stisk =getch();
			klav = (int)stisk;
			}
		}
	}
}