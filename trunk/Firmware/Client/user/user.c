
/** I N C L U D E S **********************************************************/
#include <p18f4550.h>
#include <timers.h>
#include <i2c.h>
#include <adc.h>
#include <delays.h>
#include <usart.h>
#include <string.h>
#include "system\typedefs.h"
#include "system\usb\usb.h"
#include "io_cfg.h"             // I/O pin mapping
#include "user\user.h"
#include "I2C\i2cstuff.h"

/** V A R I A B L E S ********************************************************/
#pragma udata
byte counter;
byte trf_state;

DATA_PACKET dataPacket;

//****I2C variables************************************************************/
unsigned char ControlByte;                //Control Byte
unsigned char HighAdd;                    //High Order Address Byte
unsigned char LowAdd;                     //Low Order Address Byte
unsigned char Data;                       //Data Byte
unsigned char Data2;
unsigned char Length;                     //Length of bytes to read
unsigned int  PageSize;                   //Page size in bytes
unsigned int  n;
unsigned char datamm;
unsigned char addressh;                    //High Order Address Byte
unsigned char addressl;                     //Low Order Address Byte
unsigned char Data1;
unsigned int  seqrun;
unsigned int  i2cerror1;
unsigned int  i,j,d,resultant;
unsigned int temptemp;
unsigned long templong1;

unsigned char timeqats;
unsigned char timetenths;
unsigned char timehours;
unsigned char timemins;
unsigned char timesecs;
unsigned char timerdoneyet;
unsigned char timedays;

volatile char datatemp,datatemp2;
 

char temp_buffa[5];
char data;
unsigned int m;
unsigned int baudrateval;


volatile unsigned int head, tail = 0; 
volatile unsigned int Shead,Stail=0;
volatile unsigned int length = 63;
volatile unsigned int Slength=31;
volatile unsigned char buffer_full=0;
volatile unsigned char buffer_empty=1; 
volatile unsigned char Sbuffer_full=0;
volatile unsigned char Sbuffer_empty=1;

volatile char rcv_buffa[64];
volatile char snd_buffa[32];




/** P R I V A T E  P R O T O T Y P E S ***************************************/
void ServiceRequests(void);

/** D E C L A R A T I O N S **************************************************/
#pragma code

/******************************************************************************
 * Function:        void ProcessIO(void)
 *
 * PreCondition:    None
 *
 * Input:           None
 *
 * Output:          None
 *
 * Side Effects:    None
 *
 * Overview:        This function is a place holder for other user routines.
 *                  It is a mixture of both USB and non-USB tasks.
 *
 * Note:            None
 *****************************************************************************/
void ProcessIO(void)
{   
    // User Application USB tasks
    if((usb_device_state < CONFIGURED_STATE)||(UCONbits.SUSPND==1)) return;
    {
    ServiceRequests();

    }//end if
}//end ProcessIO

void ServiceRequests(void)
{



    byte index;
  

    if(USBGenRead((byte*)&dataPacket,sizeof(dataPacket)))
    {


        counter = 0;
      
	switch(dataPacket.CMD)
        {

		case gunMove:
			//copy all of the USB data to the Xbee interface
			temp_buffa[0]=dataPacket._byte[1];
			temp_buffa[1]=dataPacket._byte[2];
			temp_buffa[2]=dataPacket._byte[3];
			temp_buffa[3]=dataPacket._byte[4];
			temp_buffa[4]=dataPacket._byte[5];
			//write to the Xbee interface
			putsuart(temp_buffa);
			
			//now send back an ok byte to let the server know write OK
			dataPacket._byte[1] = 0x01;
			counter=0x02;
			break;

		case bootmodecheck:
			dataPacket._byte[1]=0x05;
			counter=0x02;
			break;
            
		case READ_VERSION:
                //dataPacket._byte[1] is len
                dataPacket._byte[2] = MINOR_VERSION;
                dataPacket._byte[3] = MAJOR_VERSION;
                counter=0x04;
                break;
	

			case getrcv_buffa:

					// if we received data from terminal program then put it in send buffer
					if (dataPacket._byte[1]!=0x00)
					{
						write_sndbuffa(dataPacket._byte[1]);
					}

					for (n=1;n<21;n++)	
    				{
						 
						dataPacket._byte[n]=read_rcvbuffa(); 
							 
					}

				counter=0x15;
				break;



			default:
                break;

            case RESET:
                Reset();
                break;


		 }//end switch()
        	if(counter != 0)
        	{
            	if(!mUSBGenTxIsBusy())
				{
			 
 	             	  USBGenWrite((byte*)&dataPacket,counter);
						
				}
	    	}//end if
  	}//end if

}//end ServiceRequests



void TXbyte(byte data)
{
    while(TXSTAbits.TRMT==0);
    TXREG = data;
}//end TXbyte



void USBTasks2(void)
{
    /*
     * Servicing Hardware
     */
    USBCheckBusStatus();                    // Must use polling method
    if(UCFGbits.UTEYE!=1)
        USBDriverService();                 // Interrupt or polling method

}// end USBTasks


void startUsart(long baudrate)
{
	temp_buffa[4]=0x00;

	//blank out the receive buffa
	for (i=0;i<64;i++)
	{
		rcv_buffa[i]=0x00;
	}
		//blank out the send buffa
	for (i=0;i<32;i++)
	{
		snd_buffa[i]=0x00;
	}

	//small delay for things to cook up
	for (i=0;i<1026;i++)
			{
				Delay1KTCYx(10);
			}

   #define CLOCK_FREQ (48000000)      // Hz

   // configure USART
   RCSTA=0x91;
   RCSTAbits.SPEN = 1;
   TRISCbits.TRISC6 = 0;
   TRISCbits.TRISC7 = 1;
   PORTCbits.RC7=0;
   PORTCbits.RC6=1;
   TXSTA = 0x24;
   baudrateval = ( ((CLOCK_FREQ/baudrate)/4) - 1);
   SPBRG=(unsigned char)baudrateval;
   SPBRGH=(unsigned char)(baudrateval >>8);
   PIE1bits.RCIE = 0;
   PIR1bits.RCIF = 0;
   BAUDCON = 0x48;
xbee_sleep=0; //not asleep
xbee_rts=0;
datatemp=RCREG;
}


//tries to put xbee module into command mode at previously set baudrate
// returns 0 on success and 1 on failure
char testxbee(void)
{
temp_buffa[0]='A';
temp_buffa[1]='A';

	for (i=0;i<1060;i++)
	{
		Delay1KTCYx(11);
	}

putrsuart( "+++" );
	for (i=0;i<1060;i++)
	{
		Delay1KTCYx(11);
	}
getdelayuart(temp_buffa,3);

putrsuart("ATCN\r");
getdelayuart(temp_buffa,3);

//check to see module rcv_buffa, if its good then return a 0 for success
if ((temp_buffa[0]=='O')&(temp_buffa[1]=='K'))
{
	return(0);
}
// didnt get right rcv_buffa so return a 1 for failure
else 
{
return(1);
}
}

void changebaudrate(void)
{

//wait the guard time of 1 second before command mode entry
	for (i=0;i<1060;i++)
	{
		Delay1KTCYx(13);
	}

//put in command mode
putrsuart( "+++" );
getsuart(temp_buffa,3);

//set baud rate to 57600 command
putrsuart("ATBD30d40\r");
//get rcv_buffa also fulfills gaurd time for command mode 1 second after wait time
getsuart(temp_buffa,3);

putrsuart("ATWR\r");
getsuart(temp_buffa,3);

putrsuart("ATCN\r");
getsuart(temp_buffa,3);
}


//this is a delayed test getsuart, does not loop forever waiting to get a rcv_buffa
//use this one if you have an unknown working module state...(module in socket??)
void getdelayuart(char *buffer, unsigned char len)
{
  char i;    // Length counter
  unsigned char data;
  unsigned char good;
  good=0x66;

for(j=0;j<30000;j++)
	{
	Delay100TCYx(2);  //like 50 uS
    if(dataready())
	{
		j=30000;
		good=0x33;
	}
	}


if (good==0x33)
{
  for(i=0;i<len;i++)  // Only retrieve len characters
  {
	 while(!dataready());// Wait for data to be received	

    data = readusart();    // Get a character from the USART
                           // and save in the string
    *buffer = data;
    buffer++;              // Increment the string pointer
  }
}
}




void commo(void)
{
putrsuart("blah");
}

// this sends a string to USART WITHOUT SENDING THE NULL!!!!!
void putsuart( char *data)
{
  do
  {  // Transmit a byte
    while(fulluart());
//	if(*data!=0)
//	{
    	Writeuart(*data);
//	}
  } while( *data++ );
  PORTCbits.RC6=1;
}

//this sends a string to USART WITHOUT SENDING THE NULL!!
void putrsuart(const rom char *data)
{
  do
  {  // Transmit a byte
    while(BusyUSART());
	if(*data!=0)
	{
    	putcUSART(*data);
	}
  } while( *data++ );
  PORTCbits.RC6=1;
}


void Writeuart(char data)
{

  TXREG = data;      // Write the data byte to the USART
}




void Writeuart_1byte(char data)
{
	TXREG=data;
	while(BusyUSART());
	PORTCbits.RC6=1;
}

//check for a full UART
int fulluart(void)
{
  if(!TXSTAbits.TRMT)  // Is the transmit shift register empty
    return 1;          // no, return true (register full!!)

  return 0;            // Return false register empty
}


//see if we received some data
char dataready(void)
{
	if (PIR1bits.RCIF)
	{
    return 1;  // Data is available, return TRUE
	}
  	else
	{
    return 0;  // Data not available, return FALSE
	}
}

//just sit and wait for data to be received...
void getsuart(char *buffer, unsigned char len)
{
  char i;    // Length counter
  unsigned char data;

  for(i=0;i<len;i++)  // Only retrieve len characters
  {
    while(!dataready());// Wait for data to be received

    data = readusart();    // Get a character from the USART
                           // and save in the string
    *buffer = data;
    buffer++;              // Increment the string pointer
  }
}

//read the uart
char readusart(void)
{
  char data;   // Holds received data


  data = RCREG;                      // Read data

  return (data);                     // Return the received data
}



//
// reads an a2d input and returns an int
//
// pass the IO pin # 1 to 8 that you want to read
//
int a2dinputread(unsigned char iopin)
{
	switch (iopin)
	{
		case 1:

			ADCON0=0x00;
			break;
		
		case 2:

			ADCON0=0x04;
			break;
	
		case 3:

			ADCON0=0x08;
			break;

		case 4:

			ADCON0=0x0c;
			break;

		case 5:

			ADCON0=0x10;
			break;

		case 6:

			ADCON0=0x14;
			break;

		case 7:

			ADCON0=0x18;
			break;

		case 8:

			ADCON0=0x1c;
			break;

		default:

			ADCON0=0x00;
			break;
	}

ADCON0bits.ADON = 1;         // Enable the Adc
ConvertADC();
while(BusyADC());
resultant=ReadADC2();
ADCON0bits.ADON=0;
return (resultant);
}
//
// end adc
//

//
// interium read adc for above function
//
int ReadADC2(void)
{
  union ADCResult resultant; // A union is used to read the
                     // A/D result due to issues with
                     // handling long variables

  resultant.br[0] = ADRESL;  // Read ADRESL into the lower byte
  resultant.br[1] = ADRESH;  // Read ADRESH into the high byte

  return (resultant.lr);     // Return the long variable
}
//
// end interim adc read
//


//
// converts the resultant adc value to a 4 place string
//
// ie: 2.34 volts
//
// puts the string in the temp_buffa array
//
//this is executed right after and ADC read function if u want it to be a string
//
void a2d2string(int tempdata)
{

	templong1=tempdata * (unsigned long)3300;//
	templong1= templong1 / 1024;
	templong1= templong1 / 10;


	temptemp=(unsigned int)templong1;  //temp in celsius

      d=(temptemp/100);
      temp_buffa[0]=(char)(d+48);
	
	temp_buffa[1]='.';

      temptemp=temptemp%100;
       d=(temptemp/10);
       temp_buffa[2]=(char)(d+48);
 
   
   d=(temptemp % 10);
   temp_buffa[3]=(char)(d+48);
}
//
// end convert a2d to string
//



//write a byte to receive buffa
//this process assumes tail is always an empty spot
//tail should never equal head
//
void write_rcvbuffa(unsigned char blah123) 
{ 
	//check for a full buffer
	if ((buffer_full) | ((tail+1)==head) | ((tail==length) & (head==0)))
	{
//		xbee_rts=1; //inhibit unit from sending more data
		buffer_full=1;
	}
	//if buffer not full
	else
	{
		//write the byte and inc to next empty slot
		rcv_buffa[tail] = blah123; 
		if (tail >= length) 
		{	
			tail = 0; 
		}
		else 
		{
			tail++; 
		}
		
		//update flags
		buffer_empty=0;

		if (((tail+1)==head) | ((tail==length) & (head==0)))
		{
			buffer_full=1;
//			xbee_rts=1; //inhibit more stuff from comming until buffer is not empty	
		}
	}
} 
//end write a byte to receive buffa



// read a byte from receive buffer
unsigned char read_rcvbuffa(void) 
{ 


//return a null if buffer is empty, otherwise read 1 from head and increment
// then returns the value at head.
// then examines if buffer is empty and sets flag as appropriate for next read (or write)
	if ((buffer_empty) | (head==tail))
	{
		Data1=0x00; //null value
//		xbee_rts=0; //good to go
		buffer_empty=1;
	}
	else
	{
		//read 1 from head and inc head 
		Data1 = rcv_buffa[head]; 

		if (head >= length) 
		{
			head = 0; 
		}
		else 
		{
			head++; 
		}
		
		//update flags
		buffer_full=0;
//		xbee_rts=0;

		if (head==tail)
		{
			buffer_empty=1;
//			xbee_rts=0; //good to go
		}
	}
	return Data1; 
} 
// end read byte from receive buffa




//write a byte to send buffa
//this process assumes tail is always an empty spot
//tail should never equal head
//
void write_sndbuffa(unsigned char blah123) 
{ 
	//check for a full buffer
	if ((Sbuffer_full) | ((Stail+1)==Shead) | ((Stail==Slength) & (Shead==0)))
	{
		Sbuffer_full=1;
	}
	//if buffer not full
	else
	{
		//write the byte and inc to next empty slot
		snd_buffa[Stail] = blah123; 
		if (Stail >= Slength) 
		{	
			Stail = 0; 
		}
		else 
		{
			Stail++; 
		}
		
		//update flags
		Sbuffer_empty=0;

		if (((Stail+1)==Shead) | ((Stail==Slength) & (Shead==0)))
		{
			Sbuffer_full=1;
		}
	}
} 
//end write a byte to receive buffa



// read a byte from receive buffer
unsigned char read_sndbuffa(void) 
{ 


//return a null if buffer is empty, otherwise read 1 from head and increment
// then returns the value at head.
// then examines if buffer is empty and sets flag as appropriate for next read (or write)
	if ((Sbuffer_empty) | (Shead==Stail))
	{
		Data1=0x00; //null value
		Sbuffer_empty=1;
	}
	else
	{
		//read 1 from head and inc head 
		Data1 = snd_buffa[Shead]; 

		if (Shead >= Slength) 
		{
			Shead = 0; 
		}
		else 
		{
			Shead++; 
		}
		
		//update flags
		Sbuffer_full=0;

		if (Shead==Stail)
		{
			Sbuffer_empty=1;
		}
	}
	return Data1; 
} 
// end read byte from receive buffa



unsigned char port_to_ascii(unsigned char portvalue)
{
	if (portvalue==0)
	{
		return (0x30);
	}
	if  (portvalue==1)
	{
		return(0x31);
	}
}




void interrupthingie(void)
{

	if (PIR1bits.RCIF==1)
	{

	//	xbee_sleep=0;
	//	while(!xbee_cts);
	    datatemp=RCREG;

		if (!buffer_full)
		{
			write_rcvbuffa(datatemp);
		}

	//	xbee_sleep=1;
	}

	if (PIR1bits.TMR1IF == 1)    
	{ 	
        WriteTimer1(28100);
		inctime();
 
			if (!Sbuffer_empty)
		{
		for (m=0;m<5;m++)
		{
			datatemp2=read_sndbuffa();
			if (datatemp2!=0x00)
			{
				Writeuart_1byte(datatemp2);
			}
			else
			{
				m=5;
			}
		}
		}

	//clear timer 1 flag
		PIR1bits.TMR1IF = 0;                // yes, clear int flag	
	}
}


void starttimer(void)
{
	timemins=0x00;
	timesecs=0x00;
	timetenths=0x00;
	timeqats=0x00;
	timehours=0x00;
	timedays=0x00;

	CCP1CON=0x00;  
	CCP2CON=0x00; 

	PIE1bits.TMR1IE=1;

	RCONbits.IPEN=0;
	INTCONbits.PEIE=1; 
  	INTCONbits.GIE=1;                     
	OpenTimer1(TIMER_INT_ON&T1_16BIT_RW&T1_SOURCE_INT&T1_SYNC_EXT_OFF&T1_PS_1_8&T3_SOURCE_CCP);
	WriteTimer1(28100);
}



//
//
// LOAD CONFIGURATION FUNCTION
//
// edit this to change the board configuration
//
// edit the loadconfigfriendly one for a more
// user friendly version
//
void loadconfig(void)
{
	// these are your configuration registers
	//
	// only edit these if you know what you are doing
	// you can find them in the microchip datasheet for this MCU
	//
	// but generally speaking, dont edit these!! change the pin equates below them as specified
	//

		// CCP pheripheral configuration, these are unconnected in this design
		CCP1CON=0x00;
		CCP2CON=0x00;

		// A2D settings. default values are optimized for this design
		ADCON0=0x00;
		ADCON1=0x0F;
		ADCON2=0xFD;
		CMCON=0x07;

		// these specify pins as either input or ouput
		// do not change these directly
		// to change the IO pins change the ones listed below here after
       		// the register setup
		// ie: change the ones listed a IO1=input.
		TRISA=0x10;
		TRISB=0xFF;	    
		TRISC=0xBF;
		TRISD=0xB9;
		TRISE=0x00;	

		//default latch values for IO pins in these registers
		//this is what is on the pin when you start the controller
		//ie: a high or low
		LATA=0x00;
		LATB=0x00;
		LATC=0X00;
		LATD=0x04;
		LATE=0x00;

	//
	// END register configuration
	//
}


//increment time variables for tens, seconds and minutes accordingly based on calling this function. (every cal is 1/10th of a sec)
void inctime(void)
{

if (timeqats>=0x03) // runs from 0 to 3 (4 25 ms periods)  (0 to 9 os 10 ms) and (0 to 19 is 5 ms)
{
	timeqats=0x00;
	function25mS();

	if (timetenths>=0x09)                //tens runs from 0 to 9, if >=9 then reset to zero and test next unit
	{
		timetenths=0x00;
		function100mS();
	
		if (timesecs>=0x3b)     //secs run from 0 to 59 then reset and inc min
		{
			timesecs=0x00;
			function1Sec();

			if (timemins>=0x3b)
			{
				timemins=0x00;
				function1min();
					
				if(timehours>=0x17)
				{
					function1hour();

					if (timedays>=254)
					{
						function1day();
						timedays=0x00;
						timemins=0x00;  //from 0 to 59 mins then reset to 0 mins
						timesecs=0x00;
						timeqats=0x00;
						timetenths=0x00;
						timehours=0x00;
					}
					else
					{
						timedays++;
						function1day();
					}
				}
				else
				{
					timehours++;
					function1hour();
				}
			}
			else
			{
				timemins++;
				function1min();
			}
		}
		else
		{
			timesecs++;
			function1Sec();
		}
	}
	else
	{
		timetenths++;
		function100mS();
	}
}
else
{
	timeqats++;
	function25mS();
}

}


////
////
////
//// START OF QUICK NETWORK USER CODE MODIFICATIONS HERE
////
//// everything below here you can edit for a very quick network
//// everything above here can be edited for advanced users
////
////
////
////


//
//INSERT YOUR PERIODIC FUNCTIONS IN HERE inside the correspond time function that you want
//
// functions are executed according to the time in the function name
//

void function25mS(void)
{

//	}

	;	

}

void function100mS(void)
{
	;
}

void function1Sec(void)
{
;
}

void function1min(void)
{
	IO7=1;
		xbee_sleep=0;
		temp_buffa[0]=0x01;
		temp_buffa[1]=0x4C;
		temp_buffa[2]=0x04;
		temp_buffa[3]=0x4C;
		temp_buffa[4]=0x04;
		putsuart(temp_buffa);

		xbee_sleep=0;
		temp_buffa[0]=0x01;
		temp_buffa[1]=0xDC;
		temp_buffa[2]=0x05;
		temp_buffa[3]=0xDC;
		temp_buffa[4]=0x05;
		putsuart(temp_buffa);

	IO8=1;

	;
}

void function1hour(void)
{
	;
}

void function1day(void)
{
	;
}

//
// END OF PERIODIC FUNCTIONS
//




//
// init xbee function
//
// refer to maxstream module for AT commands to configure the xbee module everytime the controller starts up
//
// the AT commands are follow by a "\r" which is a carriage return
// follow the format below
//
//  DONT PUT SLEEP INSTRUCTIONS IN HERE!!!!
//  PUT THEM IN THE XBEE_SLEEP_CONFIG FUNCTION BELOW
//

void initxbee(void)
{
	//this is a guard time delay that is required for command mode entry
	// do not remove
	for (i=0;i<1060;i++)
	{
		Delay1KTCYx(30);
	}

//this is command mode entry command do not remove
putrsuart("+++");
getsuart(temp_buffa,3);

// set channel to C
putrsuart("ATSC0C\r");
getsuart(temp_buffa,3);

//panid
putrsuart("ATID33\r");
getsuart(temp_buffa,3);

//change node joining to always
putrsuart("ATNJFF\r");
getsuart(temp_buffa,3);

//node identifier for this node
putrsuart("ATNIGUN1\r");
getsuart(temp_buffa,3);

// turn off sleep mode
putrsuart("ATSM0\r");
getsuart(temp_buffa,3);

//save changes
//putrsuart("ATWR\r");
//getsuart(temp_buffa,3);

putrsuart("ATDH0\r");
getsuart(temp_buffa,3);

putrsuart("ATDL0\r");
getsuart(temp_buffa,3);

//exit command mode
putrsuart("ATCN\r");
getsuart(temp_buffa,3);


}

//
// END xbee init function
//



//
// xbee sleep configuration
// doesnt save to non volatile memory
// so when power is reset, easier to enter command mode again
//
void sleep_config_xbee(void)
{
	//this is a guard time delay that is required for command mode entry
	// do not remove
//sleep mode sucks

	for (i=0;i<1060;i++)
	{
		Delay1KTCYx(30);
	}

//this is command mode entry command do not remove
putrsuart( "+++" );
getsuart(temp_buffa,3);

// turn off sleep mode
putrsuart("ATSM0\r");
getsuart(temp_buffa,3);

// time before sleep x 1 mS curently 50 mS
//putrsuart("ATST0004\r");
//getsuart(temp_buffa,3);

// sleep period x 10 ms  //10 second wakeup (10 sec = 0x003e8) (1 sec=0x64)
//putrsuart("ATSP0002\r");
//getsuart(temp_buffa,3);

// initialize with changes
//putrsuart("ATAC\r");
//getsuart(temp_buffa,3);

//putrsuart("ATWR\r");
//getsuart(temp_buffa,3);

//exit command mode
putrsuart("ATCN\r");
getsuart(temp_buffa,3);


}

//
// END xbee sleep config function
//




//
//  LOADCONFIGFRIENDLY function
//
// 	PIN EQUATES AND PIN BEHAVIOR SETUP IN HERE
//	FOR PINS ON SCREW TERMINALS IN THIS DESIGN
//
//  change from input to output to analog
//
//  and default values on startup (high or low)
//
//	these will only work 
//  if you haven't changed the register values in loadconfig()
//
void loadconfigfriendly( void )
{

	//
	// set # of digital and analog pins here
	// 
	// if you set something as analog, you must make it an ANALOG_PIN below also!!!
	//
	// __________________________________
	//   0x0F = ALL io pins digital
	//   0x0E = IO1 analog , All else digital
	//   0x0D = IO1,IO2 analog, All else digital
	//   0x0C = IO1,IO2,IO3 analog, All else digital
	//   0x0B = IO1,IO2,IO3,IO4 analog, All else digital
	//   0x0A = IO1,IO2,IO3,IO4,IO5 analog, All else digital
    	//   0x09 = IO1,IO2,IO3,IO4,IO5,IO6 analog, All else digital
   	//   0x08 = IO1,IO2,IO3,IO4,IO5,IO6,IO7 analog, All else digital
   	//   0x07 = IO1,IO2,IO3,IO4,IO5,IO6,IO7,IO8 ALL analog lines.
	//
 	//

		ADCON1=0x0E;


	//
	// TRIS register equates
	//
	// change these values to the following for each pin below
	//
	// INPUT_PIN   = makes the pin an input 	
	// OUTPUT_PIN  = makes the pin an output
	// ANALOG_PIN  = makes the pin an analog pin
	//

		IO1_TRIS = INPUT_PIN;
		IO2_TRIS = OUTPUT_PIN;
		IO3_TRIS = OUTPUT_PIN;
		IO4_TRIS = OUTPUT_PIN;
		IO5_TRIS = OUTPUT_PIN;
		IO6_TRIS = OUTPUT_PIN;
		IO7_TRIS = OUTPUT_PIN;
		IO8_TRIS = OUTPUT_PIN;

	//
	// default pin values on bootup
	//
	//	settings are as follows:
	//
	//	1: output is a high (+5v)
	//  0: output is a low (ground)
	//
	//  put a 0 for INPUT_PIN and ANALOG_PIN
	//

		IO1 = 0;
		IO2	= 0;
		IO3 = 0;
		IO4	= 0;
		IO5 = 0;
		IO6	= 0;
		IO7 = 0;
		IO8	= 0;


	//
	// I2C bus speed
	//
	// default is 1 Mhz.
	//
	// please goto i2cstuff.c to change this setting.
	//


}

//
// END loadconfigfriendly function
//


/** EOF user.c ***************************************************************/

	