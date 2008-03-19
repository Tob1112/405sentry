using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutonomousSentryGun.Functions;

namespace AutonomousSentryGun.Objects
{
    class Packet
    {
        private byte[] data;
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
            data = new byte[PACKET_BYTE_LENGTH];
        }
        
        public byte[] Data
        {
          get
          {
            return data;
          }
        }
        public void validatePacket()
        {
            invalidatePacket();
            data[HEADER_IDX] |= HEADER_BYTE;
        }
        public void invalidatePacket()
        {
            data[HEADER_IDX] &= 1;
        }
        public void setFireOn()
        {
            data[FIRE_IDX] |= 1;
        }
        public void setFireOff()
        {
            data[FIRE_IDX] &= Convert.ToByte("11111110", 2);
        }
        public void setPosition(Position position)
        {
          this.setXPosition(position.X);
          this.setYPosition(position.Y);
        }
        public void setXPosition(int position)
        {
            byte[] positionBytes = IntToTwoBytes(position);
           
            for(int i=0; i<positionBytes.Length; i++) 
                data[XPOSITION_IDX+i] = positionBytes[i];
            
        }
        public void setYPosition(int position)
        {
            byte[] positionBytes = IntToTwoBytes(position);

            for (int i = 0; i < positionBytes.Length; i++)
                data[YPOSITION_IDX + i] = positionBytes[i];
        }
        private byte[] IntToTwoBytes(int n)
        {
            byte[] twoByte = new byte[2];
            string nStr = Convert.ToString(n, 2);
            twoByte[0] = Convert.ToByte((nStr.Length >= 8) ? nStr.Substring(0, 8) : nStr.Substring(0), 2);
            twoByte[1] = Convert.ToByte((nStr.Length<=8)?"0":nStr.Substring(8),2);            
            return twoByte;
        }

    }
}
