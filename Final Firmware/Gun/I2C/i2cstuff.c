#include <p18f4550.h>
#include <i2c.h>
#include <timers.h>
#include <delays.h>
#include "i2cstuff.h"



unsigned char blah22;
int g;
unsigned char error;
unsigned int i2cerror;

//
// initi2c
//
// inits the MSSP for i2c master mode communications
//
void initi2c(void)
{
	SSPCON1=0x00;
	SSPSTAT=0x80;				// turn off slew rate control for 100k and 1 mhz, turn on for 400k//
 	//*******************
	//
	//I2C speed setting here
	//
	//***48 mhz settings***0x1D for 400k...0x0B for 1Mhz...0x77 for 100khz.........///
	//
 	SSPADD=0x77; 				//see above speed guide for 48mhz//
	//******************
    SSPCON1=0x28;				//sspen and ssmp: 1 0 0 0 for master i2c mode//
    SSPCON2=0x00; 				//init values//
    TRISBbits.TRISB0=1;			//make sure port is input//
    TRISBbits.TRISB1=1;	 		//make sure port is input//
}

//
//	EErombyteread 
//
//	for high density serial eeprom devices which have high and low addy bytes
//	tested on 24FC128
//
//  always call this with a control byte r/w bit set to 0 (for a write) it will change it to read as necessary
//
unsigned int EErombyteread ( unsigned char control, unsigned char addressh, unsigned char addressl ) 
{ 
INTCONbits.PEIE=0;
INTCONbits.GIE=0; 

i2cerror=0x00;
IdleI2C();                      // ensure module is idle 
StartI2C();                     // initiate START condition 
while ( SSPCON2bits.SEN );      // wait until start condition is over 
if ( PIR2bits.BCLIF )           // test for bus collision 
{ 
   SSPCON1=0x00;
   SSPCON1=0x28;
   return ( 1 );                // return with Bus Collision error 
} 
else 
{ 
   if ( WriteI2Cme( control ) )    // write 1 byte 
   { 
	i2cerror=0x01;
	 SSPCON1=0x00;
	 SSPCON1=0x28;
     return ( 1 );              // return with write collision error 
 } 
   IdleI2C();                    // ensure module is idle 
if ( !SSPCON2bits.ACKSTAT )   // test for ACK condition, if received 
   {  
 if ( WriteI2Cme( addressh ) )    // write 1 byte 
    { 
		i2cerror=0x01;
      SSPCON1=0x00;
	  SSPCON1=0x28;
      return ( 1 );              // return with write collision error 
    } 
    IdleI2C();                    // ensure module is idle 
    if ( !SSPCON2bits.ACKSTAT )   // test for ACK condition, if received 
    { 
       if ( WriteI2Cme( addressl ) )  // WRITE word address for EEPROM 
       { 
			i2cerror=0x01;
         SSPCON1=0x00;
	     SSPCON1=0x28;
         return ( 1 );            // return with write collision error 
       } 
       IdleI2C();                  // ensure module is idle 
       if ( !SSPCON2bits.ACKSTAT ) // test for ACK condition, if received 
       { 
         RestartI2C();             // generate I2C bus restart condition 
         while ( SSPCON2bits.RSEN );// wait until re-start condition is over 
         if ( PIR2bits.BCLIF )     // test for bus collision 
         { 
			i2cerror=0x01;	
			SSPCON1=0x00;
			SSPCON1=0x28;
            return ( 1 );          // return with Bus Collision error 
         }                
         if ( WriteI2Cme( control+1 ) )// write 1 byte - with r/w bit set to read 1 
         { 
			i2cerror=0x01;
            SSPCON1=0x00;
			SSPCON1=0x28;
            return ( 1 );          // return with write collision error 
         } 
         IdleI2C();                // ensure module is idle 
         if ( !SSPCON2bits.ACKSTAT )// test for ACK condition, if received 
         { 
            SSPCON2bits.RCEN = 1;       // enable master for 1 byte reception 
//            while ( SSPCON2bits.RCEN); // check that receive sequence is over 
			error=checkrcv1();
			if (error==0x01)
			{
				i2cerror=0x01;
				SSPCON1=0x00;
				SSPCON1=0x28;
            	return ( 1 );          // return with write collision error 	
			}

            NotAckI2C();              // send ACK condition 
            while ( SSPCON2bits.ACKEN ); // wait until ACK sequence is over 
            StopI2C();              // send STOP condition 
            while ( SSPCON2bits.PEN ); // wait until stop condition is over 

	
            if ( PIR2bits.BCLIF )   // test for bus collision 
            { 
				i2cerror=0x01;
              SSPCON1=0x00;
			  SSPCON1=0x28;
              return ( 1 );         // return with Bus Collision error 
            } 
         } 
         else 
         { 
			i2cerror=0x01;
            SSPCON1=0x00;
			SSPCON1=0x28;
            return ( 1 );          // return with Not Ack error 
         } 
       } 
       else 
       { 
			i2cerror=0x01;
            SSPCON1=0x00;
			SSPCON1=0x28;
         return ( 1 );            // return with Not Ack error 
       } 
    } 
    else 
     { 
		i2cerror=0x01;
        SSPCON1=0x00;
		SSPCON1=0x28;
        return ( 1 );              // return with Not Ack error 
     } 
 } 
else 
   { 
		i2cerror=0x01;
      SSPCON1=0x00;
	  SSPCON1=0x28;
      return ( 1 );              // return with Not Ack error 
   } 
} 
INTCONbits.PEIE=1;
INTCONbits.GIE=1; 

		SSPCON1=0x00;
	    SSPCON1=0x28;

return ( (unsigned int) SSPBUF );     // return with data 
} 

// 
// EErombytewrite 
// 
// for high density serial eeprom devices which have high and low addy bytes 
// tested on 24FC128 
// 
//  always call this with a control byte r/w bit set to 0 (for a write) 
// 
unsigned char EErombytewrite( unsigned char control, unsigned char addressh, unsigned char addressl, unsigned char data ) 
{ 
INTCONbits.PEIE=0;
INTCONbits.GIE=0;  

i2cerror=0x00;
IdleI2C();                      // ensure module is idle 
StartI2C();                     // initiate START condition 
while ( SSPCON2bits.SEN );      // wait until start condition is over 
if ( PIR2bits.BCLIF )           // test for bus collision 
{ 
	i2cerror=0x01;
   return ( -1 );                // return with Bus Collision error 
} 
else                            // start condition successful 
{ 
   if ( WriteI2Cme( control ) )    // write byte - R/W bit should be 0 
   { 
			i2cerror=0x01;
			SSPCON1=0x00;
			SSPCON1=0x28;
     return ( -3 );              // set error for write collision 
   } 
   IdleI2C();                    // ensure module is idle 
   if ( !SSPCON2bits.ACKSTAT )   // test for ACK condition received 
   { 
     if ( WriteI2Cme( addressh ) )  // write word address for EEPROM 
     { 
			i2cerror=0x01;
			SSPCON1=0x00;
			SSPCON1=0x28;
       return ( -3 );            // set error for write collision 
     } 
   IdleI2C();                    // ensure module is idle 
     if ( !SSPCON2bits.ACKSTAT )   // test for ACK condition received 
     { 
      if ( WriteI2Cme( addressl ) )  // write word address for EEPROM 
      { 
			i2cerror=0x01;
        	SSPCON1=0x00;
			SSPCON1=0x28;
        return ( -3 );            // set error for write collision 
      } 
       IdleI2C();                  // ensure module is idle 
       if ( !SSPCON2bits.ACKSTAT ) // test for ACK condition received 
       { 
        if ( WriteI2Cme( data ) )   // data byte for EEPROM 
        { 
			i2cerror=0x01;
			SSPCON1=0x00;
			SSPCON1=0x28;
            return ( -3 );          // set error for write collision 
        } 
       } 
  else 
 { 
		i2cerror=0x01;
       SSPCON1=0x00;
	   SSPCON1=0x28;
       return ( -2 );              // return with Not Ack error condition   
       } 
      } 
      else 
      { 
		i2cerror=0x01;
        SSPCON1=0x00;
        SSPCON1=0x28;
        return ( -2 );            // return with Not Ack error condition   
      } 
    }  
    else 
    { 
		i2cerror=0x01;
       SSPCON1=0x00;
	   SSPCON1=0x28;
       return ( -2 );              // return with Not Ack error condition   
    } 
} 
IdleI2C();                      // ensure module is idle  
StopI2C();                      // send STOP condition 
while ( SSPCON2bits.PEN );      // wait until stop condition is over 

Delay10KTCYx(5); 


if ( PIR2bits.BCLIF )           // test for bus collision 
{ 
	i2cerror=0x01;
	SSPCON1=0x00;
	SSPCON1=0x28;
    return ( -1 );                // return with Bus Collision error 
} 

INTCONbits.PEIE=1;
INTCONbits.GIE=1; 

SSPCON1=0x00;
SSPCON1=0x28;

return ( 0 );                   // return with no error 
} 

unsigned char WriteI2Cme( unsigned char data_out )
{
  SSPBUF = data_out;           // write single byte to SSPBUF
  if ( SSPCON1bits.WCOL )      // test if write collision occurred
   return ( -1 );              // if WCOL bit is set return negative #
  else
  {
	for(g = 0; g < 500; g++)   //poll for bf flag without locking up if it fails...
 	{
		if (g==499)
		{
		i2cerror=0x01;
			return (1);
		}
		else
		{
 			if (SSPSTATbits.BF==0)
			{
				return (0);
			}
			else Delay10TCYx(1);
		}							
    }
//    while( SSPSTATbits.BF );   // wait until write cycle is complete oldschool method will lock up in loop      
    return ( 0 );              // if WCOL bit is not set return non-negative #
  }
}

unsigned char checkrcv1 (void)
{		
	for(g = 0; g < 500; g++)   //poll for RCEN done flag without locking up if it fails...
 	{
		if (g==499)
		{
				i2cerror=0x01;
				return (1);
		}
		else
		{
 			if (SSPCON2bits.RCEN==0)
			{
				return (0);
			}
			else Delay10TCYx(1);							
  		} 
	}
	return (0);
}  



unsigned char EErombytewrite2( unsigned char control, unsigned char addressh, unsigned char data ) 
{ 
INTCONbits.PEIE=0;
INTCONbits.GIE=0;  

i2cerror=0x00;
IdleI2C();                      // ensure module is idle 
StartI2C();                     // initiate START condition 
while ( SSPCON2bits.SEN );      // wait until start condition is over 
if ( PIR2bits.BCLIF )           // test for bus collision 
{ 
	i2cerror=0x01;
   return ( -1 );                // return with Bus Collision error 
} 
else                             // start condition successful 
{ 
   if ( WriteI2Cme( control ) )    // write byte - R/W bit should be 0 
   { 
			i2cerror=0x01;
			SSPCON1=0x00;
			SSPCON1=0x28;
     return ( -3 );              // set error for write collision 
   } 
   IdleI2C();                    // ensure module is idle 
   if ( !SSPCON2bits.ACKSTAT )   // test for ACK condition received 
   { 
     if ( WriteI2Cme( addressh ) )  // write word address for EEPROM 
     { 
			i2cerror=0x01;
			SSPCON1=0x00;
			SSPCON1=0x28;
       return ( -3 );            // set error for write collision 
     } 
   IdleI2C();                    // ensure module is idle 
     if ( !SSPCON2bits.ACKSTAT )   // test for ACK condition received 
     { 
      if ( WriteI2Cme( data ) )  // write word address for EEPROM 
      { 
			i2cerror=0x01;
        	SSPCON1=0x00;
			SSPCON1=0x28;
        return ( -3 );            // set error for write collision 
      } 
       
      } 
      else 
      { 
		i2cerror=0x01;
        SSPCON1=0x00;
        SSPCON1=0x28;
        return ( -2 );            // return with Not Ack error condition   
      } 
    }  
    else 
    { 
		i2cerror=0x01;
       SSPCON1=0x00;
	   SSPCON1=0x28;
       return ( -2 );              // return with Not Ack error condition   
    } 
} 
IdleI2C();                      // ensure module is idle  
StopI2C();                      // send STOP condition 
while ( SSPCON2bits.PEN );      // wait until stop condition is over 

Delay10KTCYx(5); 


if ( PIR2bits.BCLIF )           // test for bus collision 
{ 
	i2cerror=0x01;
	SSPCON1=0x00;
	SSPCON1=0x28;
    return ( -1 );                // return with Bus Collision error 
} 

INTCONbits.PEIE=1;
INTCONbits.GIE=1; 

SSPCON1=0x00;
SSPCON1=0x28;

return ( 0 );                   // return with no error 
} 

