*version 9.1 82148
u 52
U? 2
V? 4
R? 5
D? 3
@libraries
@analysis
@targets
@attributes
@translators
a 0 u 13 0 0 0 hln 100 PCBOARDS=PCB
a 0 u 13 0 0 0 hln 100 PSPICE=PSPICE
a 0 u 13 0 0 0 hln 100 XILINX=XILINX
@setup
unconnectedPins 0
connectViaLabel 0
connectViaLocalLabels 0
NoStim4ExtIFPortsWarnings 1
AutoGenStim4ExtIFPorts 1
@index
pageloc 1 0 3590 
@status
n 0 108:02:26:13:26:44;1206556004 e 
s 0 108:02:26:13:26:45;1206556005 e 
*page 1 0 970 720 iA
@ports
port 24 GND_ANALOG 220 410 h
port 26 GND_ANALOG 120 240 h
port 35 GND_ANALOG 330 290 h
port 36 GND_ANALOG 330 80 u
port 42 GND_ANALOG 630 200 h
@parts
part 37 r 540 200 h
a 0 sp 0 0 0 10 hlb 100 PART=r
a 0 s 0:13 0 0 0 hln 100 PKGTYPE=RC05
a 0 s 0:13 0 0 0 hln 100 GATE=
a 0 a 0:13 0 0 0 hln 100 PKGREF=R3
a 0 ap 9 0 15 0 hln 100 REFDES=R3
a 0 u 13 0 15 25 hln 100 VALUE=500
part 6 r 220 390 v
a 0 sp 0 0 0 10 hlb 100 PART=r
a 0 s 0:13 0 0 0 hln 100 PKGTYPE=RC05
a 0 s 0:13 0 0 0 hln 100 GATE=
a 0 a 0:13 0 0 0 hln 100 PKGREF=R2
a 0 ap 9 0 15 0 hln 100 REFDES=R2
a 0 u 13 0 15 25 hln 100 VALUE=68
part 2 uA741 290 180 h
a 0 sp 11 0 0 70 hcn 100 PART=uA741
a 0 s 0:13 0 0 0 hln 100 PKGTYPE=DIP8
a 0 s 0:13 0 0 0 hln 100 GATE=
a 0 a 0:13 0 0 0 hln 100 PKGREF=U1
a 0 ap 9 0 14 0 hln 100 REFDES=U1
part 49 r 200 180 h
a 0 u 13 0 15 25 hln 100 VALUE=10.24k
a 0 sp 0 0 0 10 hlb 100 PART=r
a 0 s 0:13 0 0 0 hln 100 PKGTYPE=RC05
a 0 s 0:13 0 0 0 hln 100 GATE=
a 0 a 0:13 0 0 0 hln 100 PKGREF=R4
a 0 ap 9 0 15 0 hln 100 REFDES=R4
part 5 r 430 330 h
a 0 u 13 0 15 25 hln 100 VALUE=4.2k
a 0 sp 0 0 0 10 hlb 100 PART=r
a 0 s 0:13 0 0 0 hln 100 PKGTYPE=RC05
a 0 s 0:13 0 0 0 hln 100 GATE=
a 0 a 0:13 0 0 0 hln 100 PKGREF=R1
a 0 ap 9 0 15 0 hln 100 REFDES=R1
part 25 VDC 120 200 h
a 1 u 13 0 -11 18 hcn 100 DC=3.3V
a 0 sp 0 0 22 37 hln 100 PART=VDC
a 0 a 0:13 0 0 0 hln 100 PKGREF=V3
a 1 ap 9 0 24 7 hcn 100 REFDES=V3
part 3 VDC 330 120 u
a 1 u 13 0 -11 18 hcn 100 DC=14V
a 0 sp 0 0 22 37 hln 100 PART=VDC
a 0 a 0:13 0 0 0 hln 100 PKGREF=V1
a 1 ap 9 0 24 7 hcn 100 REFDES=V1
part 4 VDC 330 250 h
a 1 u 13 0 -11 18 hcn 100 DC=-14V
a 0 sp 0 0 22 37 hln 100 PART=VDC
a 0 a 0:13 0 0 0 hln 100 PKGREF=V2
a 1 ap 9 0 24 7 hcn 100 REFDES=V2
part 1 titleblk 970 720 h
a 1 s 13 0 350 10 hcn 100 PAGESIZE=A
a 1 s 13 0 180 60 hcn 100 PAGETITLE=
a 1 s 13 0 340 95 hrn 100 PAGECOUNT=1
a 1 s 13 0 300 95 hrn 100 PAGENO=1
@conn
w 15
a 0 up 0:33 0 0 0 hln 100 V=
s 430 330 220 330 14
a 0 up 33 0 325 329 hct 100 V=
s 220 330 220 220 16
s 220 220 290 220 18
s 220 330 220 350 20
w 23
a 0 up 0:33 0 0 0 hln 100 V=
s 220 390 220 410 22
a 0 up 33 0 222 400 hlt 100 V=
w 32
a 0 up 0:33 0 0 0 hln 100 V=
s 330 120 330 170 31
a 0 up 33 0 332 145 hlt 100 V=
w 34
a 0 up 0:33 0 0 0 hln 100 V=
s 330 250 330 230 33
a 0 up 33 0 332 240 hlt 100 V=
w 8
a 0 up 0:33 0 0 0 hln 100 V=
s 370 200 490 200 7
s 470 330 490 330 9
s 490 330 490 200 11
a 0 up 33 0 492 265 hlt 100 V=
s 490 200 540 200 38
w 41
a 0 up 0:33 0 0 0 hln 100 V=
s 580 200 630 200 40
a 0 up 33 0 605 199 hct 100 V=
w 28
a 0 up 0:33 0 0 0 hln 100 V=
s 120 180 120 200 29
s 120 180 200 180 44
a 0 up 33 0 160 179 hct 100 V=
w 47
a 0 up 0:33 0 0 0 hln 100 V=
s 230 180 240 180 46
a 0 up 33 0 260 179 hct 100 V=
s 240 180 290 180 50
@junction
j 370 200
+ p 2 OUT
+ w 8
j 470 330
+ p 5 2
+ w 8
j 430 330
+ p 5 1
+ w 15
j 290 220
+ p 2 -
+ w 15
j 220 350
+ p 6 2
+ w 15
j 220 330
+ w 15
+ w 15
j 220 390
+ p 6 1
+ w 23
j 220 410
+ s 24
+ w 23
j 120 240
+ s 26
+ p 25 -
j 120 200
+ p 25 +
+ w 28
j 330 120
+ p 3 +
+ w 32
j 330 170
+ p 2 V+
+ w 32
j 330 250
+ p 4 +
+ w 34
j 330 230
+ p 2 V-
+ w 34
j 330 290
+ s 35
+ p 4 -
j 330 80
+ s 36
+ p 3 -
j 540 200
+ p 37 1
+ w 8
j 490 200
+ w 8
+ w 8
j 580 200
+ p 37 2
+ w 41
j 630 200
+ s 42
+ w 41
j 290 180
+ p 2 +
+ w 47
j 200 180
+ p 49 1
+ w 28
j 240 180
+ p 49 2
+ w 47
@attributes
a 0 s 0:13 0 0 0 hln 100 PAGETITLE=
a 0 s 0:13 0 0 0 hln 100 PAGENO=1
a 0 s 0:13 0 0 0 hln 100 PAGESIZE=A
a 0 s 0:13 0 0 0 hln 100 PAGECOUNT=1
@graphics
t 51 t 6 520 180 600 140 0 30
Relay internal coil resistance
