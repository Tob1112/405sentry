/*********************************************************************
 *
 *                Microchip USB C18 Firmware Version 1.0
 *
 *********************************************************************
 * FileName:        main.c
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

/** I N C L U D E S **********************************************************/
#include <p18f4550.h>
#include <delays.h>
#include <usart.h>
#include "system\typedefs.h"                        // Required
#include "system\usb\usb.h"                         // Required
#include "io_cfg.h"                                 // Required
#include "system\usb\usb_compile_time_validation.h" // Optional
#include "user\user.h"                              // Modifiable
#include "I2C\i2cstuff.h"

/** V A R I A B L E S ********************************************************/
#pragma udata

#pragma config PLLDIV = 5   // 96 MHz PLL Prescaler.. (20 mhz input
#pragma config CPUDIV = OSC1_PLL2 // CPU System Clock (48 mhz)
#pragma config USBDIV = 2  //2 normally    // Full-Speed USB Clock Source Selection
#pragma config FOSC = HSPLL_HS //HSPLL_HS normally // Oscillator Selection bits --> HS oscillator, PLL enabled, HS used by USB
#pragma config FCMEN = OFF   // Fail-Safe Clock Monitor --> Disabled
#pragma config IESO = ON   // Internal/External Switch Over --> Use External Oscillator
#pragma config PWRT = OFF   // Power-up Timer --> Disabled
#pragma config BOR = OFF   // Brown-out Reset
#pragma config BORV = 2
#pragma config VREGEN = ON   // USB Voltage Regulator Enable
#pragma config WDT = OFF   // Watchdog Timer --> Disabled
#pragma config WDTPS = 32768
#pragma config MCLRE = ON   // MCLR Enable --> Disabled
#pragma config LPT1OSC = OFF  // Low Power Timer1 Oscillator Enable --> High Power
#pragma config PBADEN = OFF   // PORTB A/D Enable --> Pins 4:0 are digital
#pragma config CCP2MX = ON //goes to rc1
#pragma config STVREN = ON   // Stack Overflow Reset --> Enabled
#pragma config LVP = OFF   // Low Voltage ICSP
#pragma config ICPRT = ON   // Dedicated In-Circuit Debug/Programming Enable --> Disabled
#pragma config XINST = OFF   // Extended Instruction Set Enable --> Disabled
#pragma config DEBUG = OFF   // Background Debugger Enable --> Disabled
#pragma config CP0 = OFF   // Code Protection Block 0 --> Disabled
#pragma config CP1 = OFF   // Code Protection Block 1 --> Disabled
#pragma config CP2 = OFF   // Code Protection Block 1 --> Disabled
#pragma config CP3 = OFF   // Code Protection Block 1 --> Disabled
#pragma config CPB = OFF   // Boot Block Code Protection --> Disabled
#pragma config CPD = OFF  //code protect eeprom
#pragma config WRT0 = OFF   // Write Protection Block 0 --> Disabled
#pragma config WRT1 = OFF   // Write Protection Block 1 --> Disabled
#pragma config WRT2 = OFF   // Write Protection Block 0 --> Disabled
#pragma config WRT3 = OFF   // Write Protection Block 1 --> Disabled
#pragma config WRTB = ON   // Boot Block Write Protection --> Disabled 
#pragma config WRTC = ON  //config write protect
#pragma config WRTD = OFF  //data eeprom write protect
#pragma config EBTR0 = OFF
#pragma config EBTR1 = OFF
#pragma config EBTR2 = OFF
#pragma config EBTR3 = OFF
#pragma config EBTRB = OFF



/** P R I V A T E  P R O T O T Y P E S ***************************************/
static void InitializeSystem(void);
void USBTasks(void);
void interrupt1(void);

/** V E C T O R  R E M A P P I N G *******************************************/

extern void _startup (void);        // See c018i.c in your C18 compiler dir


//this code works with bootloader to remap vectors
//
#pragma code _RESET_INTERRUPT_VECTOR = 0x000800
void _reset (void)
{
    _asm goto _startup _endasm
}
#pragma code

#pragma code _HIGH_INTERRUPT_VECTOR = 0x000808
void _high_ISR (void)
{
	_asm goto interrupt1 _endasm
}
#pragma code


#pragma code _LOW_INTERRUPT_VECTOR = 0x000818
void _low_ISR (void)
{
	;
}
#pragma code



/*
#pragma code _HIGH_INTERRUPT_VECTOR = 0x000008
void _high_ISR (void)
{
	_asm goto interrupt1 _endasm
}
#pragma code


#pragma code _LOW_INTERRUPT_VECTOR = 0x000018
void _low_ISR (void)
{
_asm goto interrupt1 _endasm
}
#pragma code
*/


#pragma interrupt interrupt1
#pragma code

void interrupt1(void)
{
	interrupthingie();
}




/** D E C L A R A T I O N S **************************************************/
#pragma code


unsigned int notdone;
unsigned int timerdun;
unsigned int b,c;
char a;





/******************************************************************************
 * Function:        void main(void)
 *
 * PreCondition:    None
 *
 * Input:           None
 *
 * Output:          None
 *
 * Side Effects:    None
 *
 * Overview:        Main program entry point.
 *
 * Note:            None
 *****************************************************************************/
void main(void)
{
 
//do some startup tasks...

		InitializeSystem();


//small delay for things to cook up


	for (b=0;b<1026;b++)
			{
				Delay1KTCYx(1);
			}



//start I2C system at 1 Mhz speed
	initi2c();


//board default configuration registers are changed here
//make changed to loafconfigfriendly for a more friendly version
 loadconfig();


//
//setup your IO configuration
//
// NOTE: you should change this 1st before anything else
// this allows you to change ports to input, output, or analog
// this function is in user.c
//
loadconfigfriendly();


for (c=0;c<10;c++)
{

//small delay for things to cook up
	for (b=0;b<1036;b++)
			{
				Delay1KTCYx(10);
			}
}

//start the USART module at 57600 buad rate
	startUsart(200000);  


//small delay for things to cook up
	for (b=0;b<1026;b++)
			{
				Delay1KTCYx(10);
			}



//test xbee module at given baudrate of 57600
	if (testxbee())
	{

		//57600 baud failed so lets test to see if its a new module
		//start the USART module at 9600 buad rate
		startUsart(9600);  

		//small delay for things to cook up
		for (b=0;b<1060;b++)
		{
			Delay1KTCYx(1);
		}
		//testing xbee at 9600 baudrate
		if (testxbee())
		{

//LOOP FOR NO XBEE MODULE FOUND (FAILED AT BOTH BAUDRATES)
		//xbee module failed at 9600 baud so no module present at either baudrate
		//new modules come with a 9600 baud rate by default.
		//
		//this is the no module operating code in here, put what you want when no module present
		//can use as a regular controller or PLC.
			//
			//	NO XBEE MODULE FOUND MAIN LOOP STARTS HERE
			//

			mInitializeUSBDriver(); 


			for(c=0;c<60;c++)
			{
				for (b=0;b<1060;b++)
				{
					Delay1KTCYx(1);
					USBTasks(); 
					ProcessIO(); 
				}
			}



			while(1)
			{
				//small delay so dont roast the USB system too much 
				Delay1KTCYx(10);

				//USB polling tasks optional

				for (b=0;b<1060;b++)
				{
					Delay1KTCYx(1);
					USBTasks(); 
					ProcessIO(); 
				}


		
			//
			//  NO XBEE MODULE FOUND MAIN LOOP ENDS HERE
			//
			} //end no xbee found main loop
//END LOOP
		}
		//found module at 9600 baud rate
        //
        //now we will change the baud rate to 57600 in the module to speed things up
		else
		{
				//small delay for things to cook up
				for (b=0;b<1060;b++)
				{
					Delay1KTCYx(20);
				}


			//change xbee module to 57600 baud rate
			changebaudrate();

			//start the USART module at 57600 buad rate
			startUsart(200000);

			for (c=0;c<30;c++)
			{

				//small delay for things to cook up
				for (b=0;b<3000;b++)
				{
					Delay1KTCYx(1);
				}
			}

	   	

			//initialize xbee module to the paramters specified in the following function
			//
			// NOTE: you need to edit this function to tailor this node's network parameters and such
			// this function is found in user.c
      		//			

		//	initxbee();

		}

	}
	//found xbee module at 57600 baud rate
	else
	{
		//initialize xbee module to the paramters specified in the following function
		//
		// NOTE: you need to edit this function to tailor this node's network parameters and such
		// this function is found in user.c
        //

		//small delay for things to cook up
		for (b=0;b<1026;b++)
			{
				Delay1KTCYx(10);
			
			}


	//	initxbee();
	}
	


	


	//configure xbee sleep mode here but it is not written to non volatile memory
 	//since may have problems re-entering command mode later on!!
  //  sleep_config_xbee();





//put xbee to pin sleep 
//	xbee_sleep=1; //put module to sleep!! (edge triggeered wakeup)





//shutoff USB module to save 8 mA current
//...uncomment to turn module off

//		UCONbits.USBEN=0;
//		UCONbits.SUSPND=1;	


//uncomment to turn USB on
   		mInitializeUSBDriver(); 
//end usb turn on



	for(c=0;c<40;c++)
	{
		for (b=0;b<1060;b++)
		{
				Delay1KTCYx(1);
				USBTasks(); 
				ProcessIO(); 
		}
	}



// set processor primary idle mode (not sleep!!)
//	  	OSCCONbits.IDLEN=1;


//start timer 1 to time periodic functions
  		starttimer();


//start USART receive interrupt
		PIE1bits.RCIE=1;



//MAIN LOOP FOR XBEE MODULE NORMAL
	//
	//	START MAIN LOOP FOR XBEE MODULE FOUND AT 200 K BAUD AND INTIALIZED TO YOUR PARAMETERS
	//



    while(1)
    {

	
		//loop to service USB requests (polling)
		for (b=0;b<1060;b++)
		{
				Delay1KTCYx(1);

		//service USB tasks
				USBTasks(); 
				ProcessIO(); 

			
		}


	}//end while

	//
	//  END MAIN LOOP FOR XBEE MODULE FOUND AT 57600 BAUD AND INITIALIZED TO YOUR PARAMETERS
	//
//END LOOP


}//end main



/******************************************************************************
 * Function:        static void InitializeSystem(void)
 *
 *
 * Overview:        InitializeSystem is a centralize initialization routine.
 *                  All required USB initialization routines are called from
 *                  here.
 *
 *                  User application initialization routine should also be
 *                  called from here.                  
 *
 * Note:            None
 *****************************************************************************/
static void InitializeSystem(void)
{
	mInitmisc();		//inits A/D and port A and port b for i2c ops
//	mInitializeUSBDriver();         // See usbdrv.h
}//end InitializeSystem

/******************************************************************************
 * Function:        void USBTasks(void)
 *
 * PreCondition:    InitializeSystem has been called.
 *
 * Overview:        Service loop for USB tasks.
 *
 * Note:            None
 *****************************************************************************/
void USBTasks(void)
{
    /*
     * Servicing Hardware
     */
    USBCheckBusStatus();                    // Must use polling method
    if(UCFGbits.UTEYE!=1)
        USBDriverService();                 // Interrupt or polling method

}// end USBTasks






/** EOF main.c ***************************************************************/
