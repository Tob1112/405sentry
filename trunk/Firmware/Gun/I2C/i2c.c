#include <p18f4550.h>
#include <i2c.h>
#include <delays.h>
#include "AN991.h"

/************************************************************************
*     Function Name:    LDByteWriteI2C                                  *
*     Parameters:       EE memory ControlByte, address and data         *
*     Description:      Writes data one byte at a time to I2C EE        *
*                       device. This routine can be used for any I2C    *
*                       EE memory device, which only uses 1 byte of     *
*                       address data as in the 24LC01B/02B/04B/08B/16B. *
*                                                                       *
************************************************************************/
unsigned char blah22;


unsigned int EERandomReadm( unsigned char control, unsigned char addressh, unsigned char addressl )
{
  //blah22=SSPBUF;   //dummy read to demonstrate not clearing sspbuf on 2nd time calling subroutine
  PORTD=SSPSTAT;   //shows me value of sspstat before the program gets stuck in the writei2c command.
  IdleI2C();                      // ensure module is idle
  StartI2C();                     // initiate START condition
  while ( SSPCON2bits.SEN );      // wait until start condition is over 
  if ( PIR2bits.BCLIF )           // test for bus collision
  {
    return ( -1 );                // return with Bus Collision error 
  }
  else
  {
    if ( WriteI2C( control ) )    // write 1 byte
    {
     	return ( -3 );              // return with write collision error  
  	}
   	IdleI2C();                    // ensure module is idle
	if ( !SSPCON2bits.ACKSTAT )   // test for ACK condition, if received
    {	 
		if ( WriteI2C( addressh ) )    // write 1 byte
    	{
     		return ( -3 );              // return with write collision error
   		}
    	IdleI2C();                    // ensure module is idle
   		if ( !SSPCON2bits.ACKSTAT )   // test for ACK condition, if received
    	{
      		if ( WriteI2C( addressl ) )  // WRITE word address for EEPROM
      		{
        		return ( -3 );            // return with write collision error
      		}
      		IdleI2C();                  // ensure module is idle
      		if ( !SSPCON2bits.ACKSTAT ) // test for ACK condition, if received
      		{
        		RestartI2C();             // generate I2C bus restart condition
        		while ( SSPCON2bits.RSEN );// wait until re-start condition is over 
        		if ( PIR2bits.BCLIF )     // test for bus collision
        		{
          			return ( -1 );          // return with Bus Collision error 
        		}                
        		if ( WriteI2C( control+1 ) )// write 1 byte - with r/w bit set to read 1
        		{
          			return ( -3 );          // return with write collision error
        		}
        		IdleI2C();                // ensure module is idle
        		if ( !SSPCON2bits.ACKSTAT )// test for ACK condition, if received
        		{
          			SSPCON2bits.RCEN = 1;       // enable master for 1 byte reception
          			while ( SSPCON2bits.RCEN ); // check that receive sequence is over
					PIR1bits.SSPIF=0;
                    blah22=SSPBUF; 
          			NotAckI2C();              // send ACK condition
          			while ( SSPCON2bits.ACKEN ); // wait until ACK sequence is over 
          			StopI2C();              // send STOP condition
          			while ( SSPCON2bits.PEN ); // wait until stop condition is over 
          			if ( PIR2bits.BCLIF )   // test for bus collision
          			{
           				return ( -1 );         // return with Bus Collision error 
          			}
        		}
        		else
        		{
          			return ( -2 );          // return with Not Ack error
        		}
      		}
      		else
      		{
        		return ( -2 );            // return with Not Ack error
      		}
    	}
    	else
    		{
      			return ( -2 );              // return with Not Ack error
    		}
  	}
	else
    {
      	return ( -2 );              // return with Not Ack error
    }
	}
  return ( (unsigned int) blah22 );     // return with data
}


unsigned char LDByteWriteI2C( unsigned char ControlByte, unsigned char LowAdd, unsigned char data )
{
  IdleI2C();                          // ensure module is idle
  StartI2C();                         // initiate START condition
  while ( SSPCON2bits.SEN );          // wait until start condition is over 
  WriteI2C( ControlByte );            // write 1 byte - R/W bit should be 0
  IdleI2C();                          // ensure module is idle
  WriteI2C( LowAdd );                 // write address byte to EEPROM
  IdleI2C();                          // ensure module is idle
  WriteI2C ( data );                  // Write data byte to EEPROM
  IdleI2C();                          // ensure module is idle
  StopI2C();                          // send STOP condition
  while ( SSPCON2bits.PEN );          // wait until stop condition is over 
  while (EEAckPolling(ControlByte));  //Wait for write cycle to complete
  return ( 0 );                       // return with no error
}

/********************************************************************
*     Function Name:    LDByteReadI2C                               *
*     Parameters:       EE memory ControlByte, address, pointer and *
*                       length bytes.                               *
*     Description:      Reads data string from I2C EE memory        *
*                       device. This routine can be used for any I2C*
*                       EE memory device, which only uses 1 byte of *
*                       address data as in the 24LC01B/02B/04B/08B. *
*                                                                   *  
********************************************************************/

unsigned char LDByteReadI2C( unsigned char ControlByte, unsigned char address, unsigned char *data, unsigned char length )
{
  IdleI2C();                      // ensure module is idle
  StartI2C();                     // initiate START condition
  while ( SSPCON2bits.SEN );      // wait until start condition is over 
  WriteI2C( ControlByte );        // write 1 byte 
  IdleI2C();                      // ensure module is idle
  WriteI2C( address );            // WRITE word address to EEPROM
  IdleI2C();                      // ensure module is idle
  RestartI2C();                   // generate I2C bus restart condition
  while ( SSPCON2bits.RSEN );     // wait until re-start condition is over 
  WriteI2C( ControlByte | 0x01 ); // WRITE 1 byte - R/W bit should be 1 for read
  IdleI2C();                      // ensure module is idle
  getsI2C( data, length );       // read in multiple bytes
  NotAckI2C();                    // send not ACK condition
  while ( SSPCON2bits.ACKEN );    // wait until ACK sequence is over 
  StopI2C();                      // send STOP condition
  while ( SSPCON2bits.PEN );      // wait until stop condition is over 
  return ( 0 );                   // return with no error

}

/********************************************************************
*     Function Name:    LDPageWriteI2C                              *
*     Parameters:       EE memory ControlByte, address and pointer  *
*     Description:      Writes data string to I2C EE memory         *
*                       device. This routine can be used for any I2C*
*                       EE memory device, which uses 2 bytes of     *
*                       address data as in the 24LC32A/64/128/256.  *
*                                                                   *  
********************************************************************/

unsigned char LDPageWriteI2C( unsigned char ControlByte, unsigned char LowAdd, unsigned char *wrptr )
{
  IdleI2C();                      // ensure module is idle
  StartI2C();                     // initiate START condition
  while ( SSPCON2bits.SEN );      // wait until start condition is over 
  WriteI2C( ControlByte );        // write 1 byte - R/W bit should be 0
  IdleI2C();                      // ensure module is idle
  WriteI2C( LowAdd );             // write LowAdd byte to EEPROM
  IdleI2C();                      // ensure module is idle
  putstringI2C ( wrptr );         // pointer to data for page write
  IdleI2C();                      // ensure module is idle
  StopI2C();                      // send STOP condition
  while ( SSPCON2bits.PEN );      // wait until stop condition is over 
  return ( 0 );                   // return with no error
}

/********************************************************************
*     Function Name:    LDSequentialReadI2C                         *
*     Parameters:       EE memory ControlByte, address, pointer and *
*                       length bytes.                               *
*     Description:      Reads data string from I2C EE memory        *
*                       device. This routine can be used for any I2C*
*                       EE memory device, which only uses 1 byte of *
*                       address data as in the 24LC01B/02B/04B/08B. *
*                                                                   *  
********************************************************************/

unsigned char LDSequentialReadI2C( unsigned char ControlByte, unsigned char address, unsigned char *rdptr, unsigned char length )
{
  IdleI2C();                      // ensure module is idle
  StartI2C();                     // initiate START condition
  while ( SSPCON2bits.SEN );      // wait until start condition is over 
  WriteI2C( ControlByte );        // write 1 byte 
  IdleI2C();                      // ensure module is idle
  WriteI2C( address );            // WRITE word address to EEPROM
  IdleI2C();                      // ensure module is idle
  RestartI2C();                   // generate I2C bus restart condition
  while ( SSPCON2bits.RSEN );     // wait until re-start condition is over 
  WriteI2C( ControlByte | 0x01 ); // WRITE 1 byte - R/W bit should be 1 for read
  IdleI2C();                      // ensure module is idle
  getsI2C( rdptr, length );       // read in multiple bytes
  NotAckI2C();                    // send not ACK condition
  while ( SSPCON2bits.ACKEN );    // wait until ACK sequence is over 
  StopI2C();                      // send STOP condition
  while ( SSPCON2bits.PEN );      // wait until stop condition is over 
  return ( 0 );                   // return with no error
}

/************************************************************************
*     Function Name:    HDByteWriteI2C                                  *   
*     Parameters:       EE memory ControlByte, address and data         *
*     Description:      Writes data one byte at a time to I2C EE        *
*                       device. This routine can be used for any I2C    *
*                       EE memory device, which only uses 1 byte of     *
*                       address data as in the 24LC01B/02B/04B/08B/16B. *
*                                                                       *     
************************************************************************/

unsigned char HDByteWriteI2C( unsigned char ControlByte, unsigned char HighAdd, unsigned char LowAdd, unsigned char data )
{
  IdleI2C();                      // ensure module is idle
  StartI2C();                     // initiate START condition
  while ( SSPCON2bits.SEN );      // wait until start condition is over 
  WriteI2C( ControlByte );        // write 1 byte - R/W bit should be 0
  IdleI2C();                      // ensure module is idle
  !SSPCON2bits.ACKSTAT; 
  WriteI2C( HighAdd );            // write address byte to EEPROM
  IdleI2C();                      // ensure module is idle
  !SSPCON2bits.ACKSTAT; 
  WriteI2C( LowAdd );             // write address byte to EEPROM
  IdleI2C();                      // ensure module is idle
  !SSPCON2bits.ACKSTAT; 
  WriteI2C ( data );              // Write data byte to EEPROM
  IdleI2C();                      // ensure module is idle
  StopI2C();                      // send STOP condition
  while ( SSPCON2bits.PEN );      // wait until stop condition is over 
  while (EEAckPolling(ControlByte));  //Wait for write cycle to complete
  
  return ( 0 );                   // return with no error
}

/********************************************************************
*     Function Name:    HDByteReadI2C                               *
*     Parameters:       EE memory ControlByte, address, pointer and *
*                       length bytes.                               *
*     Description:      Reads data string from I2C EE memory        *
*                       device. This routine can be used for any I2C*
*                       EE memory device, which only uses 1 byte of *
*                       address data as in the 24LC01B/02B/04B/08B. *
*                                                                   *  
********************************************************************/

unsigned char HDByteReadI2C( unsigned char ControlByte, unsigned char HighAdd, unsigned char LowAdd, unsigned char *data, unsigned char length )
{
  unsigned char datatemp1;
  IdleI2C();                      // ensure module is idle
  StartI2C();                     // initiate START condition
  while ( SSPCON2bits.SEN );      // wait until start condition is over 	
  WriteI2C( ControlByte ); 	          // write control byte for a write command
  IdleI2C();   	       					  // ensure module is idle
  !SSPCON2bits.ACKSTAT; 
  WriteI2C( HighAdd );            // WRITE word address to EEPROM
  IdleI2C();                      // ensure module is idle
  !SSPCON2bits.ACKSTAT; 
  WriteI2C( LowAdd );             // WRITE word address to EEPROM
  IdleI2C();                      // ensure module is idle
  !SSPCON2bits.ACKSTAT;  
  RestartI2C();                   // generate I2C bus restart condition
  while ( SSPCON2bits.RSEN );     // wait until re-start condition is over 
  WriteI2C( ControlByte | 0x01 ); 			  // WRITE 1 byte - R/W bit should be 1 for read
  !SSPCON2bits.ACKSTAT;                       // ensure module is idle
  datatemp1=ReadI2C();            // read in bytes
  IdleI2C();
  NotAckI2C();                    // send not ACK condition
  while ( SSPCON2bits.ACKEN );    // wait until ACK sequence is over 
  StopI2C();                      // send STOP condition
  while ( SSPCON2bits.PEN );      // wait until stop condition is over 
  SSPCON1=0x00;
  SSPCON1=0x28;
  return ( datatemp1 );                   // return with no error
 
}

/********************************************************************
*     Function Name:    HDPageWriteI2C                              *
*     Parameters:       EE memory ControlByte, address and pointer  *
*     Description:      Writes data string to I2C EE memory         *
*                       device. This routine can be used for any I2C*
*                       EE memory device, which uses 2 bytes of     *
*                       address data as in the 24LC32A/64/128/256.  *
*                                                                   *  
********************************************************************/

unsigned char HDPageWriteI2C( unsigned char ControlByte, unsigned char HighAdd, unsigned char LowAdd, unsigned char *wrptr )
{
  IdleI2C();                      // ensure module is idle
  StartI2C();                     // initiate START condition
  while ( SSPCON2bits.SEN );      // wait until start condition is over 
  WriteI2C( ControlByte );        // write 1 byte - R/W bit should be 0
  IdleI2C();                      // ensure module is idle
  !SSPCON2bits.ACKSTAT;  
  WriteI2C( HighAdd );            // write HighAdd byte to EEPROM 
  IdleI2C();                      // ensure module is idle
  !SSPCON2bits.ACKSTAT;  
  WriteI2C( LowAdd );             // write LowAdd byte to EEPROM
  IdleI2C();                      // ensure module is idle
  !SSPCON2bits.ACKSTAT;  
  putstringI2C ( wrptr );         // pointer to data for page write
  IdleI2C();                      // ensure module is idle
  StopI2C();                      // send STOP condition
  while ( SSPCON2bits.PEN );      // wait until stop condition is over 
  SSPCON1=0x00;
  SSPCON1=0x28;
  return ( 0 );                   // return with no error
}

/********************************************************************
*     Function Name:    HDSequentialReadI2C                         *
*     Parameters:       EE memory ControlByte, address, pointer and *
*                       length bytes.                               *
*     Description:      Reads data string from I2C EE memory        *
*                       device. This routine can be used for any I2C*
*                       EE memory device, which only uses 2 bytes of*
*                       address data as in the 24xx32 - 24xx512.    *
*                                                                   *  
********************************************************************/

unsigned char HDSequentialReadI2C( unsigned char ControlByte, unsigned char HighAdd, unsigned char LowAdd, unsigned char *rdptr, unsigned char length )
{
  IdleI2C();                      // ensure module is idle
  StartI2C();                     // initiate START condition
  while ( SSPCON2bits.SEN );      // wait until start condition is over 
  WriteI2C( ControlByte );        // write 1 byte 
  IdleI2C();                      // ensure module is idle
  WriteI2C( HighAdd );            // WRITE word address to EEPROM
  IdleI2C();                      // ensure module is idle
  WriteI2C( LowAdd );             // write HighAdd byte to EEPROM
  IdleI2C();                      // ensure module is idle
  RestartI2C();                   // generate I2C bus restart condition
  while ( SSPCON2bits.RSEN );     // wait until re-start condition is over 
  WriteI2C( ControlByte | 0x01 ); // WRITE 1 byte - R/W bit should be 1 for read
  IdleI2C();                      // ensure module is idle
  getsI2C( rdptr, length );       // read in multiple bytes
  NotAckI2C();                    // send not ACK condition
  while ( SSPCON2bits.ACKEN );    // wait until ACK sequence is over 
  StopI2C();                      // send STOP condition
  while ( SSPCON2bits.PEN );      // wait until stop condition is over 
  return ( 0 );                   // return with no error
}

/********************************************************************
*     Function Name:    putstringI2C                                *
*     Return Value:     error condition status                      *
*     Parameters:       address of write string storage location    *
*     Description:      This routine writes a string to the I2C bus,*
*                       until a null character is reached. If Master*
*                       function putcI2C is called. When trans-     *
*                       mission is complete then test for ack-      *
*                       nowledge bit. If Slave transmitter wait for *
*                       null character or not ACK received from bus *
*                       device.                                     *
********************************************************************/

unsigned char putstringI2C( unsigned char *wrptr )
{

unsigned char x;
  for (x = 0; x < PageSize; x++ ) // transmit data until PageSize  
  {
    if ( SSPCON1bits.SSPM3 )      // if Master transmitter then execute the following
    {
      if ( putcI2C ( *wrptr ) )   // write 1 byte
      {
        return ( -3 );            // return with write collision error
      }
      !SSPCON2bits.ACKSTAT;  
	  //IdleI2C();                  // test for idle condition 
      //if ( SSPCON2bits.ACKSTAT )  // test received ack bit state
      //{
      //  return ( -2 );            // bus device responded with  NOT ACK
       //}                        // terminateputstringI2C() function
    }
    else                          // else Slave transmitter
    {
      PIR1bits.SSPIF = 0;         // reset SSPIF bit
      SSPBUF = *wrptr;            // load SSPBUF with new data
      SSPCON1bits.CKP = 1;        // release clock line 
      while ( !PIR1bits.SSPIF );  // wait until ninth clock pulse received

      if ( ( !SSPSTATbits.R_W ) && ( !SSPSTATbits.BF ) )// if R/W=0 and BF=0, NOT ACK was received
      {
        return ( -2 );            // terminateputstringI2C() function
      }
    }
  wrptr ++;                       // increment pointer
  }                               // continue data writes until null character
  wrptr --;
  wrptr --;
  PORTD=*wrptr;
  return ( 0 );
}




