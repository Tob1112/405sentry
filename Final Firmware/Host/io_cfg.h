/*********************************************************************
 *
 *                Microchip USB C18 Firmware Version 1.0
 *
 *********************************************************************
 * FileName:        io_cfg.h
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
 * Rawin Rojvanit       11/19/04    Original.
 ********************************************************************/

#ifndef IO_CFG_H
#define IO_CFG_H

/** I N C L U D E S *************************************************/
#include "autofiles\usbcfg.h"

/** T R I S *********************************************************/
#define INPUT_PIN					1
#define OUTPUT_PIN					0
#define ANALOG_PIN					1

/** U S B stuff uses RA4, RA5***********************************************************/
#define tris_usb_bus_sense  TRISAbits.TRISA4    // change to ra4 in revision B

#if defined(USE_USB_BUS_SENSE_IO)
#define usb_bus_sense       PORTAbits.RA4   ///change to ra4 in revision B
#else
#define usb_bus_sense       1
#endif

#define self_power          1








///********************************************/
#define mInitmisc()		    TRISA=0x10;ADCON0=0x00;ADCON1=0x0F; ADCON2=0x3C;CMCON=0x07;TRISB=0xFF;TRISC=0xBF;TRISD=0xB9;LATD=0x04;TRISE=0x00;LATE=0x00; 



#define IO1_TRIS				 TRISAbits.TRISA0
#define IO2_TRIS 				 TRISAbits.TRISA1
#define IO3_TRIS				 TRISAbits.TRISA2
#define IO4_TRIS				 TRISAbits.TRISA3
#define IO5_TRIS				 TRISAbits.TRISA5
#define IO6_TRIS				 TRISEbits.TRISE0
#define IO7_TRIS				 TRISEbits.TRISE1
#define IO8_TRIS				 TRISEbits.TRISE2

#define IO1						 PORTAbits.RA0		
#define IO2						 PORTAbits.RA1
#define IO3						 PORTAbits.RA2
#define IO4						 PORTAbits.RA3
#define IO5						 PORTAbits.RA5
#define IO6						 PORTEbits.RE0
#define IO7						 PORTEbits.RE1
#define IO8						 PORTEbits.RE2

#define xbee_sleep           PORTDbits.RD2

#define xbee_rts			 PORTDbits.RD1

#define xbee_cts			 PORTDbits.RD0


#endif //IO_CFG_H
