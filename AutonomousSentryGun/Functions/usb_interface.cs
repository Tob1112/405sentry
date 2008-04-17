using System;
using System.Collections;
using System.Runtime.InteropServices;    // for PInvoke
using Microsoft.Win32;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;

using PVOID = System.IntPtr;
using DWORD = System.UInt32;


namespace usb_api
{
	unsafe public class usb_interface
	{
		#region  String Definitions of Pipes and VID_PID
		//string vid_pid_boot= "vid_04d8&pid_000b";    // Bootloader vid_pid ID
		public string vid_pid_norm= "vid_04d8&pid_feaf";

		string out_pipe= "\\MCHP_EP1"; // Define End Points
		string in_pipe= "\\MCHP_EP1";
		#endregion

		#region Imported DLL functions from mpusbapi.dll
		[DllImport("mpusbapi.dll")]
		private static extern DWORD _MPUSBGetDLLVersion();
		[DllImport("mpusbapi.dll")]
		private static extern DWORD _MPUSBGetDeviceCount(string pVID_PID);
		[DllImport("mpusbapi.dll")]
		private static extern void* _MPUSBOpen(DWORD instance,string pVID_PID,string pEP,DWORD dwDir,DWORD dwReserved);
		[DllImport("mpusbapi.dll")]
		private static extern DWORD _MPUSBRead(void* handle,void* pData,DWORD dwLen,DWORD* pLength,DWORD dwMilliseconds);
		[DllImport("mpusbapi.dll")]
		private static extern DWORD _MPUSBWrite(void* handle,void* pData,DWORD dwLen,DWORD* pLength,DWORD dwMilliseconds);
		[DllImport("mpusbapi.dll")]
		private static extern DWORD _MPUSBReadInt(void* handle,DWORD dwLen,DWORD* pLength,DWORD dwMilliseconds);
		[DllImport("mpusbapi.dll")]
		private static extern bool _MPUSBClose(void* handle);
		#endregion

		void* myOutPipe;
		void* myInPipe;

		private void OpenPipes()
		{
			DWORD selection=0; // Selects the device to connect to, in this example it is assumed you will only have one device per vid_pid connected.

			myOutPipe = _MPUSBOpen(selection,vid_pid_norm,out_pipe,0,0);
			myInPipe = _MPUSBOpen(selection,vid_pid_norm,in_pipe,1,0);
		}
		public void ClosePipes()
		{
			_MPUSBClose(myOutPipe);
			_MPUSBClose(myInPipe);
		}

		private DWORD SendReceivePacket(byte* SendData, DWORD SendLength, byte* ReceiveData, DWORD *ReceiveLength)
		{
			uint SendDelay=1000;
			uint ReceiveDelay=1000;

			DWORD SentDataLength;
			DWORD ExpectedReceiveLength = *ReceiveLength;

			OpenPipes();

				if(_MPUSBWrite(myOutPipe,(void*)SendData,SendLength,&SentDataLength,SendDelay)==1)
				{

					if(_MPUSBRead(myInPipe,(void*)ReceiveData, ExpectedReceiveLength,ReceiveLength,ReceiveDelay)==1)
					{
						if(*ReceiveLength == ExpectedReceiveLength)
						{
							ClosePipes();
							return 1;   // Success!
						}
						else if(*ReceiveLength < ExpectedReceiveLength)
						{
							ClosePipes();
							return 2;   // Partially failed, incorrect receive length
						}
					}
				}
			ClosePipes();
			return 0;  // Operation Failed
		}

		public DWORD GetDLLVersion()
		{
			return _MPUSBGetDLLVersion();
		}
		public DWORD GetDeviceCount(string Vid_Pid)
		{
			return _MPUSBGetDeviceCount(Vid_Pid);
		}

        public uint bootmodecheck(uint x)
        {

            byte* send_buf = stackalloc byte[64];
            byte* receive_buf = stackalloc byte[64];


            uint Temp6 = 2;

            DWORD RecvLength = 2;
            send_buf[0] = 0x44;			//Command for bootmodecheck
            if (SendReceivePacket(send_buf, 1, receive_buf, &RecvLength) == 1)
            {
                Temp6 = receive_buf[1];
            }

            return Temp6;
        }


public bool gunDisabled(byte headerByte)
{
	byte* send_buf = stackalloc byte[64];
	byte* receive_buf = stackalloc byte[64];
	byte[] rcv_buff = new byte[0];	
	DWORD RecvLength = 2;
	
	bool result = false;
		
	//header byte to tell the Zigbee that the USB packet checks for the gun status
	send_buf[0] = 0xEE;
	//padding byte so that the buffer is changed in even increments
	send_buf[1] = 0X00;

	if (SendReceivePacket(send_buf, 2, receive_buf, &RecvLength) == 1)
        {
		rcv_buff[0] = receive_buf[1];
        	if(rcv_buff[0] == 0x01)
			result = true;
        }

        return result;	
}

        public bool getdata(byte[] sendByte)
        {
            bool result;
            byte* send_buf = stackalloc byte[64];
            byte* receive_buf = stackalloc byte[64];
            //byte[] rcvbuff = new byte[6];

		//first byte for Zigbee to recognize the USB write
            	send_buf[0] = 0xAA;
		//copy all of the sendByte bits to the send_buffer
	    	send_buf[1] = sendByte[0];
send_buf[2] = sendByte[1];
send_buf[3] = sendByte[2];
send_buf[4] = sendByte[3];
send_buf[5] = sendByte[4];
send_buf[6] = sendByte[5];	

            DWORD RecvLength = 7;
            if (SendReceivePacket(send_buf, 7, receive_buf, &RecvLength) == 1)
            {
                result = true;
            }
            else
            {
                MessageBox.Show("USB Error!");
                result = false;
            }

            return result;
        }
        
        
        public void reset33(uint x)
        {

            byte* send_buf = stackalloc byte[64];
            byte* receive_buf = stackalloc byte[64];


            uint Temp6 = 2;

            DWORD RecvLength = 0;
            send_buf[0] = 0xFF;			//Command for reset
            if (SendReceivePacket(send_buf, 1, receive_buf, &RecvLength) == 1)
            {
                Temp6= receive_buf[1];
            }

            return;
        }
     
	}

}
