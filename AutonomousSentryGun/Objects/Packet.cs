using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Runtime.InteropServices;    // for PInvoke
using Microsoft.Win32;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AutonomousSentryGun.Functions;
using System.Drawing;

namespace AutonomousSentryGun.Objects
{
    class Packet
    {
        private byte[] data;
        private const int PACKET_BYTE_LENGTH = 6;
        //FIRE:1
        //XPOSITION:16
        //YPOSITION:16
        private const int FIRE_IDX = 0;
        private const int XPOSITION_IDX = 1;
        private const int YPOSITION_IDX = 3;
        private const int CHECKSUM = 5;

        public Packet()
        {
            data = new byte[PACKET_BYTE_LENGTH];
        }
        public Packet(Point point)
            : this()
        {
            this.setPosition(point);
        }
        public byte[] Data
        {
            get
            {
                return data;
            }
        }
        public void setFireOn()
        {
            data[FIRE_IDX] = 0x01;
        }
        public void setFireOff()
        {
            data[FIRE_IDX] = 0x00;
        }
        public void setPosition(Point point)
        {
            this.setXPosition(point.X);
            this.setYPosition(point.Y);
            UpdateCheckSum();
        }
        private void setXPosition(int position)
        {
            byte[] positionBytes = IntToTwoBytes(position);

            for (int i = 0; i < positionBytes.Length; i++)
                data[XPOSITION_IDX + i] = positionBytes[i];


        }
        private void setYPosition(int position)
        {
            byte[] positionBytes = IntToTwoBytes(position);

            for (int i = 0; i < positionBytes.Length; i++)
                data[YPOSITION_IDX + i] = positionBytes[i];
        }
        private byte[] IntToTwoBytes(int n)
        {
            byte[] twoByte = new byte[2];
            string nStr = Convert.ToString(n, 2);
            twoByte[0] = Convert.ToByte(nStr.Substring(nStr.Length - 8, 8), 2);
            twoByte[1] = Convert.ToByte(nStr.Substring(0, nStr.Length - 8), 2);
            //if ((twoByte[0] > 0x7F) || (twoByte[1] > 0x7F))
              //  MessageBox.Show("out of range data?");
            return twoByte;
        }
        private void UpdateCheckSum()
        {
            data[Packet.CHECKSUM] = 0x00;
            /*
            data[Packet.CHECKSUM] = (byte)((((data[Packet.YPOSITION_IDX] & 0x0f) > 7) ? 0x01 : 0x00) |
            (((((((data[Packet.YPOSITION_IDX] & 0xf0) >> 4) & 0x0f) > 7) ? 0x01 : 0x00) << 1) |
            ((((data[Packet.YPOSITION_IDX + 1] & 0x0f) > 7) ? 0x01 : 0x00) << 2) |
            ((((((data[Packet.YPOSITION_IDX + 1] & 0xf0) >> 4) & 0x0f) > 7) ? 0x01 : 0x00) << 3) |
            ((((data[Packet.XPOSITION_IDX] & 0x0f) > 7) ? 0x01 : 0x00) << 4) |
            ((((((data[Packet.XPOSITION_IDX] & 0xf0) >> 4) & 0x0f) > 7) ? 0x01 : 0x00) << 5) |
            ((((data[Packet.XPOSITION_IDX + 1] & 0x0f) > 7) ? 0x01 : 0x00) << 6) |
            (((((data[Packet.XPOSITION_IDX + 1] & 0xf0) >> 4) & 0x0f) > 7) ? 0x01 : 0x00) << 7));
             */
        }
    }
}
