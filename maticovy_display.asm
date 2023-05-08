.NOLIST
.INCLUDE "m128def.inc"
.LIST
.DSEG
	TAB1: .BYTE 20	;rezervovani 20 bajtu
.CSEG
.ORG 0x0000
.EQU ddrKLA = DDRB
.EQU portKLA = PORTB
.EQU pinKLA = PINB
.EQU ddrDIS = DDRE
.EQU portDIS = PORTE
.EQU pinDIS = PINE

JMP RESET

.ORG 0x0014			;casovac 2
JMP ZOBRAZ

.ORG 0x0020 		;ctc rezim casovace 0
JMP CHECK

.ORG 0x001C 		;preteceni casovace 1
JMP ZMENA

.ORG 0x0050
RESET:
CLI
LDI R16, LOW(RAMEND) ;nastaveni zásobniku
OUT SPL, R16
LDI R16, HIGH(RAMEND)
OUT SPH, R16

LDI ZL, LOW(2*TAB)		;nulovani tabulky hodnot
LDI ZH, HIGH(2*TAB)
LDI XL, LOW(TAB1)		;nulovani tabulky pozice
LDI XH, HIGH(TAB1)

LDI R16, 0x0F 		;nastavení ovladani
OUT ddrKLA, R16

LDI R16, 0xFF
OUT ddrDIS, R16

LDI R21, 0x00
LDI R22, 0x00
LDI R23, 0x00
LDI R24, 0x00

LDI R16, 0x03		;preddelic casoac 2
OUT TCCR2, R16

LDI R16, 0x07 		;preddelic casovace 1
OUT TCCR1B, R16		

LDI R16, 0x07 	;hodnota pro pøeddìlièe pro èasovaè 0 a zapnuti ctc rezimu
OUT TCCR0, R16 
	
LDI R16, 0x40 		;hodnota pro povoleni generovani preruseni casovacu (0x86 PRO ZAPNUTI)
OUT TIMSK, R16 	;povolení generovaní pøerušeni
SEI

MAIN: 			;hlavni smycka
LDI R17, 0xFE 		;aktivovani prvniho sloupce
OUT portKLA, R17	;vstup do ukladaciho rezimu
RCALL DELAY
ULOZ:
IN R17, pinKLA
CPI R17, 0x7E
BRNE ULOZ
LDI R16, 0x41	;zapnout cteni klavesnice
OUT TIMSK, R16
LDI XL, LOW (TAB1)		;nulovani tabulky pozice
LDI XH, HIGH (TAB1)

JMP MAIN
				;1
CHECK: 			;kontrola klavesnice 
LDI R16, 0x40	;jen zobrazeni
OUT TIMSK, R16

LDI R17, 0xFE 		;aktivovani prvniho sloupce
OUT portKLA, R17
RCALL DELAY
IN R17, pinKLA
CPI R17, 0xEE 	;kontrola prvniho radku v prvnim sloupci
BRNE CIS1 		;pokud je tlacitko aktivni tak posli hodnotu
LDI R23, 0x08
CALL LEAVE

CIS1:			;4
CPI R17, 0xDE 		;kontrola druheho radku v prvnim sloupci
BRNE CIS2 		
LDI R23, 0x20
CALL LEAVE

CIS2:			;7
CPI R17, 0xBE 		;kontrola tretiho radku v prvnim sloupci
BRNE CIS3 
LDI R23, 0x38
CALL LEAVE

CIS3:			;2
LDI R17, 0xFD 		;aktivovani druheho sloupce
OUT portKLA, R17
RCALL DELAY
IN R17, pinKLA
CPI R17, 0xED
BRNE CIS4
LDI R23, 0x10
CALL LEAVE

CIS4:			;5
CPI R17, 0xDD
BRNE CIS5
LDI R23, 0x28
CALL LEAVE

CIS5:			;8
CPI R17, 0xBD
BRNE CIS6
LDI R23, 0x40
CALL LEAVE

CIS6:			;0
CPI R17, 0x7D
BRNE CIS7
LDI R23, 0x00
CALL LEAVE
				;3
CIS7:				;aktivovani tretiho sloupce
LDI R17, 0xFB
OUT portKLA, R17
RCALL DELAY
IN R17, pinKLA
CPI R17, 0xEB
BRNE CIS8
LDI R23, 0x18
CALL LEAVE

CIS8:			;6
CPI R17, 0xDB
BRNE CIS9
LDI R23, 0x30
CALL LEAVE

CIS9:			;9
CPI R17, 0xBB
BRNE CIS12
LDI R23, 0x48
CALL LEAVE

CIS12:			;#
CPI R17, 0x7B
BRNE BACK
CPI R24, 0x05		;kontrola na nedostatek hodnot
BRLO SKIP
LDI R23, 0xFF
ST X+, R23
LDI R17, 0xFE 		;aktivovani prvniho sloupce
OUT portKLA, R17	;vstup do ukladaciho rezimu
RCALL DELAY
LDI XL, LOW(TAB1)		;nulovani tabulky pozice
LDI XH, HIGH(TAB1)
LDI R16, 0x44		;vypne cteni klavesnice, zap. posun
OUT TIMSK, R16
RETI
SKIP:
LDI R16, 0x41
OUT TIMSK, R16
RETI

BACK:
LDI R17, 0xFE 		;aktivovani prvniho sloupce
OUT portKLA, R17	;vstup do ukladaciho rezimu
RCALL DELAY
LDI R16, 0x41
OUT TIMSK, R16
RETI

LEAVE:
CPI R24, 0x10	;kontrola presahu ulozenych hodnot
BRSH SKIP2
ST X+, R23
INC R24 		;pocet ulozenych hodnot
RCALL DELAY2
SKIP2:
RET

ZOBRAZ: 			
LPM R21, Z+
OUT portDIS, R21
CPI R21, 0xFF
BRNE NUL
LDI ZL, LOW(2*TAB)		;nulovani tabulky hodnot
LDI ZH, HIGH(2*TAB)
ADD ZL, R23
NUL:
RETI

ZMENA:
LD R23, X+
CPI R23, 0xFF
BRNE NUL2
LDI XL, LOW(TAB1)		;nulovani tabulky pozice
LDI XH, HIGH(TAB1)
RJMP ZMENA
NUL2:
RETI

DELAY: 				;zpozdeni 0,5 ms
LDI R18, 0x63 
LDI R19, 0x0B
LDI R20, 0x01
JMP ZPOZDENI

DELAY2:				;zpozdeni 250 ms
LDI R18, 0xBF
LDI R19, 0x4B
LDI R20, 0x15

ZPOZDENI:
DEC R18
BRNE ZPOZDENI
DEC R19
BRNE ZPOZDENI	
DEC R20
BRNE ZPOZDENI
RET

.ORG 0x120
TAB: .db 0b11001_000, 0b10110_100, 0b10110_010, 0b10110_110, 0b10110_001, 0b10110_101, 0b11001_011, 0xFF	;0
	 .db 0b11101_000, 0b11001_100, 0b10101_010, 0b11101_110, 0b11101_001, 0b11101_101, 0b11101_011, 0xFF	;1
	 .db 0b11001_000, 0b10110_100, 0b10110_010, 0b11101_110, 0b11011_001, 0b10111_101, 0b10000_011, 0xFF	;2
	 .db 0b11001_000, 0b10110_100, 0b11001_010, 0b11110_110, 0b11110_001, 0b10110_101, 0b11001_011, 0xFF	;3
	 .db 0b11011_000, 0b11011_100, 0b10101_010, 0b10101_110, 0b10000_001, 0b11101_101, 0b11101_011, 0xFF	;4
	 .db 0b10000_000, 0b10111_100, 0b10001_010, 0b10110_110, 0b11110_001, 0b10110_101, 0b11001_011, 0xFF	;5
	 .db 0b11001_000, 0b10111_100, 0b10111_010, 0b10001_110, 0b10110_001, 0b10110_101, 0b11001_011, 0xFF	;6
	 .db 0b10000_000, 0b11110_100, 0b11101_010, 0b11101_110, 0b11011_001, 0b10111_101, 0b10111_011, 0xFF	;7
	 .db 0b11001_000, 0b10110_100, 0b10110_010, 0b11001_110, 0b10110_001, 0b10110_101, 0b11001_011, 0xFF	;8
	 .db 0b11001_000, 0b10110_100, 0b10110_010, 0b11000_110, 0b11110_001, 0b11110_101, 0b11001_011, 0xFF	;9

