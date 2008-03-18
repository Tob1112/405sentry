using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutonomousSentryGun.Objects
{
    class Packet
    {
        private byte[] packet;
        private const int PACKET_BYTE_LENGTH = 5;
        private const byte HEADER_BYTE = 0xAA; //10101010
        //HEADER:7
        //FIRE:1
        //XPOSITION:16
        //YPOSITION:16
        private const int HEADER_IDX = 4;
        private const int FIRE_IDX = 4;
        private const int XPOSITION_IDX = 2;
        private const int YPOSITION_IDX = 0;

        public Packet() {
            packet = new byte[PACKET_BYTE_LENGTH];
        }
        //public string toString() { }
        public byte[] getPacket()
        {
            return packet;
        }
        public void validatePacket()
        {
            invalidatePacket();
            packet[HEADER_IDX] |= HEADER_BYTE;
        }
        public void invalidatePacket()
        {
            packet[HEADER_IDX] &= 1;
        }
        public void setFireOn()
        {
            packet[FIRE_IDX] |= 1;
        }
        public void setFireOff()
        {
            packet[FIRE_IDX] &= Convert.ToByte("11111110", 2);
        }
        public void setXPosition(int position)
        {
            byte[] positionBytes = IntToTwoBytes(position);
           
            for(int i=0; i<positionBytes.Length; i++) 
                packet[XPOSITION_IDX+i] = positionBytes[i];
            
        }
        public void setYPosition(int position)
        {
            byte[] positionBytes = IntToTwoBytes(position);

            for (int i = 0; i < positionBytes.Length; i++)
                packet[YPOSITION_IDX + i] = positionBytes[i];
        }
        private byte[] IntToTwoBytes(int n)
        {
            byte[] twoByte = new byte[2];
            twoByte[0] = Convert.ToByte(Convert.ToString(n, 2).Substring(8,8),2);
            twoByte[1] = Convert.ToByte(Convert.ToString(n, 2).Substring(0,8), 2);
            return twoByte;
        }

    }
}
