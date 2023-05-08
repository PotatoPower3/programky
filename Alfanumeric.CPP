#include<stdio.h>
#include<dos.h>
#include<conio.h>

#define DATA 0x300
#define CLK 0x301

int stat=0;
int klav;
char stisk;
int num =0;
int i;
int poc = 0;	//pocitadlo znaku
int znak;

int tabulkaN[10][8]=
{
	{48,1,1,1,1,1,1,0},//0
	{49,0,1,1,0,0,0,0},//1
	{50,1,1,0,1,1,0,1},//2
	{51,1,1,1,1,0,0,1},//3
	{52,0,1,1,0,0,1,1},//4
	{53,1,0,1,1,0,1,1},//5
	{54,1,0,1,1,1,1,1},//6
	{55,1,1,1,0,0,0,0},//7
	{56,1,1,1,1,1,1,1},//8
	{57,1,1,1,1,0,1,1},//9
};
int tabulkaA[36][17]=
{
	{48 , 0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0},//znak 0
	{49 , 1,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0},//1
	{50 , 2,1,1,0,1,1,0,0,0,1,0,0,0,1,0,0},//2
	{51 , 3,1,1,1,1,0,0,0,0,1,0,0,0,1,0,0},//3
	{52 , 4,0,1,1,0,0,1,0,0,1,0,0,0,1,0,0},//4
	{53 , 5,1,0,1,1,0,1,0,0,1,0,0,0,1,0,0},//5
	{54 , 6,1,0,1,1,1,1,0,0,1,0,0,0,1,0,0},//6
	{55 , 7,1,0,0,0,0,0,0,1,0,0,0,1,0,0,0},//7
	{56 , 8,1,1,1,1,1,1,0,0,1,0,0,0,1,0,0},//8
	{57 , 9,1,1,1,1,0,1,0,0,1,0,0,0,1,0,0},//9
	{97 ,10,1,1,1,0,1,1,0,0,1,0,0,0,1,0,0},//A
	{98 ,11,0,0,1,1,1,1,0,0,1,0,0,0,1,0,0},//b
	{99 ,12,1,0,0,1,1,1,0,0,0,0,0,0,0,0,0},//C
	{100,13,0,0,0,0,1,1,0,0,0,0,0,1,0,1,0},//D
	{101,14,1,0,0,1,1,1,0,0,1,0,0,0,1,0,0},//E
	{102,15,1,0,0,0,1,1,0,0,1,0,0,0,1,0,0},//F
	{103,16,1,0,1,1,1,1,0,0,1,0,0,0,0,0,0},//G
	{104,17,0,1,1,0,1,1,0,0,1,0,0,0,1,0,0},//H
	{105,18,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0},//I
	{106,19,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0},//J
	{107,20,0,0,0,0,0,0,1,1,0,0,1,0,0,0,0},//K
	{108,21,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0},//L
	{109,22,0,1,1,0,1,1,0,1,0,0,0,0,0,1,0},//M
	{110,23,0,1,1,0,1,1,0,0,0,1,0,0,0,1,0},//N
	{111,24,0,0,1,1,1,0,0,0,1,0,0,0,1,0,0},//o
	{112,25,1,1,0,0,1,1,0,0,1,0,0,0,1,0,0},//P
	{113,26,1,1,1,1,1,1,0,0,0,1,0,0,0,0,0},//Q
	{114,27,1,1,0,0,1,1,0,0,1,1,0,0,1,0,0},//R
	{115,28,1,1,0,1,1,0,0,0,0,1,0,0,0,1,0},//S
	{116,29,1,0,0,0,0,0,1,0,0,0,1,0,0,0,0},//T
	{117,30,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0},//U
	{118,31,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0},//v
	{119,32,0,0,1,0,1,0,0,0,0,1,0,1,0,0,0},//w
	{120,33,0,0,0,0,0,0,0,1,0,1,0,1,0,1,0},//X
	{121,34,0,0,0,0,0,0,0,1,0,0,1,0,0,1,0},//Y
	{122,35,1,0,0,1,0,0,0,1,0,0,0,1,0,0,0},//Z
};
int zobraz[5]=   //zobrazuje spravne
{0,0,0,0,0};
int clc()
{
	outportb(CLK, 1);
	outportb(CLK, 0);
}


int main(void)
{
	while(1)
	{
		switch (stat)
		{
			case 0: 
			printf("Zvol typ displeje a-Alfanumericky | n-Numericky \n");
			stisk= getch();
			klav = (int)stisk;
			if (klav == 97)	//alfanumericky(a)
			{
				printf("Zkontrolujte zda mate jumpery pro alfanumeric \n");
				stat = 2;
				poc = 0;
				printf("Na klavesnici zvolte 4-mistnou kombinaci cisel a pismen \n");
			}
			else if (klav == 110)	//numericky(n)
			{
				printf("Zkontrolujte zda mate jumpery pro numeric \n");
				stat = 1;
				poc = 0;
				printf("Na klavesnici zvolte 5-mistnou kombinaci cisel \n");
			}
			else 
			{
				printf("Zadejte prosim platny symbol \n");
			}
			break;

			case 1:		//zadavani hodnot pro numeric
			stisk= getch();
			klav = (int)stisk;
			for (i = 0; i < 10; i++)
			{
				if (klav == tabulkaN[i][0])
				{
					zobraz[poc] = i;
					poc++;
				}

				if (poc == 5)
				{
					poc = 0;
					stat = 3;
				}
			}
			break;
			
			case 2:		//zadavani hodnot pro alfanumeric
			stisk = getch();
			klav = (int)stisk;
			for (i = 0; i < 36; i++)
			{
				if (klav == tabulkaA[i][0])
				{
					zobraz[poc] = tabulkaA[i][1];
					poc++;
				}

				if (poc == 4)
				{
					poc = 0;
					stat = 4;
					printf("Pro vypnuti zobrazovani stisknete ESCAPE \n");
				}
			}
			break;
			
			case 3:		//zobrazeni pro numeric
			outportb(DATA, 1);         //start bit
			clc();
			for (int k = 0; k < 5; k++)
			{
				znak = zobraz[k];
				for (int i = 1; i < 8; i++)
				{
					outportb(DATA, tabulkaN[znak][i]);
					clc();
				}
			}
			stat = 0;
			break;

			case 4:		//zobrazeni alfanumeric
			outportb(DATA, 1);	//start bit
			clc();
			for (int u = 0; u < 3; u = u+2)
			{
				znak = zobraz[u];
				for (int i = 2; i < 17; i++)
				{
					outportb(DATA, tabulkaA[znak][i]);
					clc();
				}
			}
			outportb(DATA, 0);	//vyber dvojce displeju
			clc();
			outportb(DATA, 1);
			clc();
			for (int p = 0; p < 3; p++)	//vyplnovaci bity (do 36)
			{
				outportb(DATA, 0);
				clc();
			}
			outportb(DATA, 1);	//start bit
			clc();
			for (int a = 1; a < 4; a = a + 2)
			{
					znak = zobraz[a];
					for (i = 2; i < 17; i++)
					{
						outportb(DATA, tabulkaA[znak][i]);
						clc();
					}
			}
			outportb(DATA, 1);	//vyber dvojice displeju
			clc();
			outportb(DATA, 0);
			clc();
			for (p = 0; p < 3; p++)		//vyplnovaci bity (do 36)
			{
				outportb(DATA, 0);
				clc();
			}

			if (kbhit())	//moznost zastaveni zobrazovani na displeji
			{
				stisk = getch();
				klav = (int)stisk;
				if (klav == 27)	//ascii pro ESC
				{
					stat = 0;					
				}
			}
			break;
		}
	}

}