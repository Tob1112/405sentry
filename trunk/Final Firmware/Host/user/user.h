/*********************************************************************
 *
 *                Microchip USB C18 Firmware Version 1.0
 *
 *********************************************************************
 * FileName:        user.h
 * Dependencies:    See INCLUDES section below
 * Processor:       PIC18
 * Compiler:        C18 2.30.01+
 * Company:         Microchip Technology, Inc.
 *
 * Software License Agreement
 *
 * The software supplied herewith by Microchip Technology Incorporated
 * (the “Company”) for its PICmicro® Microcontroller is intended and
 * supplied to you, the Company’s customer, for use solely and
 * exclusively on Microchip PICmicro Microcontroller products. The
 * software is owned by the Company and/or its supplier, and is
 * protected under applicable copyright laws. All rights are reserved.
 * Any use in violation of the foregoing restrictions may subject the
 * user to criminal sanctions under applicable laws, as well as to
 * civil liability for the breach of the terms and conditions of this
 * license.
 *
 * THIS SOFTWARE IS PROVIDED IN AN “AS IS” CONDITION. NO WARRANTIES,
 * WHETHER EXPRESS, IMPLIED OR STATUTORY, INCLUDING, BUT NOT LIMITED
 * TO, IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
 * PARTICULAR PURPOSE APPLY TO THIS SOFTWARE. THE COMPANY SHALL NOT,
 * IN ANY CIRCUMSTANCES, BE LIABLE FOR SPECIAL, INCIDENTAL OR
 * CONSEQUENTIAL DAMAGES, FOR ANY REASON WHATSOEVER.
 *
 * Author               Date        Comment
 *~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * Rawin Rojvanit       11/19/04     Original.
 ********************************************************************/




#ifndef PICDEM_FS_DEMO_H
#define PICDEM_FS_DEMO_H

/** I N C L U D E S **********************************************************/
#include "system\typedefs.h"

/** D E F I N I T I O N S ****************************************************/
/* PICDEM FS USB Demo Version */
#define MINOR_VERSION   0x00    //Demo Version 1.00
#define MAJOR_VERSION   0x01

/* Temperature Mode */
#define TEMP_REAL_TIME  0x00
#define TEMP_LOGGING    0x01




/** S T R U C T U R E S ******************************************************/
typedef union DATA_PACKET
{
    byte _byte[USBGEN_EP_SIZE];  //For byte access
    word _word[USBGEN_EP_SIZE/2];  //For word access(USBGEN_EP_SIZE msut be even)
    struct
    {
        enum
        {
			gunMove			= 0xAA,
			bootmodecheck	= 0x44,
            READ_VERSION    = 0x00,
			RESET = 0xFF,
			getrcv_buffa	= 0xA0 
        }CMD;
        byte len;
    };
    struct
    {
        unsigned :8;
        byte ID;
    };
    struct
    {
        unsigned :8;
        byte led_num;
        byte led_status;
    };
	struct
    {
        unsigned :8;
        word word_data;
    };  
} DATA_PACKET;

/** P U B L I C  P R O T O T Y P E S *****************************************/
void UserInit(void);
void ProcessIO(void);

void loadconfig(void);

int a2dinputread(unsigned char iopin);

void USBTasks2(void);

void startUsart(long baudrate);

char setupmodule(void);

void putbUSART(char *data);

void Writeuart(char data);

void Writeuart_1byte(char data);

int fulluart(void);

char dataready(void);

void getsuart(char *buffer, unsigned char len);

char readusart(void);

void initxbee(void);

void putsuart( char *data);

void putPacket(char *data);

void putrsuart(const rom char *data);

void commo(void);

void USBTasks2(void);

char testxbee(void);

void changebaudrate(void);

void getdelayuart(char *buffer, unsigned char len);

void starttimer(void);

void inctime(void);

void interrupthingie(void);

int ReadADC2(void);

void function25mS(void);

void function100mS(void);

void function1Sec(void);

void function1min(void);

void function1hour(void);

void function1day(void);

void loadconfigfriendly( void );

void sleep_config_xbee(void);

void write_rcvbuffa(unsigned char c);

unsigned char read_rcvbuffa(void);

void write_sndbuffa(unsigned char blah123);

unsigned char read_sndbuffa(void); 

unsigned char port_to_ascii(unsigned char portvalue);

void a2d2string(int tempdata);


#endif //PICDEM_FS_DEMO_H
