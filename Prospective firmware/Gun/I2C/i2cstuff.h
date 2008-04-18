//Global variables 


//Function Prototypes
void initi2c(void);
unsigned int  EErombyteread( unsigned char, unsigned char, unsigned char );
unsigned char EErombytewrite( unsigned char, unsigned char, unsigned char, unsigned char );
unsigned char putstringI2C( unsigned char *);
unsigned char HDPageWriteI2C( unsigned char, unsigned char, unsigned char, unsigned char *);
unsigned char HDSequentialReadI2C(unsigned char, unsigned char, unsigned char, unsigned char *, unsigned char );

unsigned char WriteI2Cme( unsigned char);
unsigned char checkrcv1 (void);
unsigned char EErombytewrite2( unsigned char control, unsigned char addressh, unsigned char data );
