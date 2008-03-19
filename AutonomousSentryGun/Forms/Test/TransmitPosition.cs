using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using usb_api;

using AutonomousSentryGun.Functions;

namespace AutonomousSentryGun.Forms.Test
{

  public partial class TransmitPosition : Form
  {
    //create the USB interface
    usb_interface usbHub;
    //buffer to store the fire, x, y data
    byte[] usbSendBuff;
    byte[] usbRcvBuff;
    //temp position to hold the aiming data...can be deleted later
    uint currentX;
    byte currentXL;
    byte currentXH;
    uint currentY;
    byte currentYL;
    byte currentYH;
    char num;

    Position gunPosition;

    private readonly static int CENTER_GRID_X = 139;
    private readonly static int CENTER_GRID_Y = 137;
    private readonly int X_CENTER;
    private readonly int Y_CENTER;
    private Position dotPosition;
    public TransmitPosition()
    {
      InitializeComponent();

      //init components for moving gun
      usbHub= new usb_interface();
      usbSendBuff = new byte[5];
      usbRcvBuff = new byte[6];
      currentX = 1500;
      currentXL = 0xDC;
      currentXH = 0x05;
      currentY = 1500;
      currentYL = 0xDC;
      currentYH = 0x05;

      redDot.Location = new Point(TransmitPosition.CENTER_GRID_X, TransmitPosition.CENTER_GRID_Y);
      dotPosition = new Position();
      XTextBox.Text = (dotPosition.X).ToString();
      YTextBox.Text = (dotPosition.Y).ToString();
      X_CENTER = dotPosition.X;
      Y_CENTER = dotPosition.Y;
      HTextBox.Text = (dotPosition.HAngle).ToString();
      VTextBox.Text = (dotPosition.VAngle).ToString();
      label1.Text = "(" + ServoController.MIN_INTEGER_INPUT + "," + ServoController.MIN_INTEGER_INPUT + ")" + "\n" +
                      "(" + ServoController.MIN_H_ANGLE + "°," + ServoController.MIN_V_ANGLE + "°)";
    }

    private void XTextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar.Equals('\r'))
        XTextBox_Update(sender, e);
    }
    private void XTextBox_Leave(object sender, EventArgs e)
    {
      int x;
      if (int.TryParse(XTextBox.Text, out x))
        XTextBox_Update(sender, e);
    }
    private void XTextBox_Update(object sender, EventArgs e)
    {
      int x = int.Parse(XTextBox.Text);
      if (x != dotPosition.X)
      {
        if (x > ServoController.MAX_INTEGER_INPUT)
        {
          x = ServoController.MAX_INTEGER_INPUT;
          XTextBox.Text = x.ToString();
        }
        else if (x < ServoController.MIN_INTEGER_INPUT)
        {
          x = ServoController.MIN_INTEGER_INPUT;
          XTextBox.Text = x.ToString();
        }

        dotPosition.X = x;
        redDot.Location = new Point((dotPosition.X - X_CENTER) + CENTER_GRID_X, redDot.Location.Y);
        HTextBox.Text = (dotPosition.HAngle).ToString();
        //ServoController.sendPosition(dotPosition);
      }
    }

    private void YTextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar.Equals('\r'))
        YTextBox_Update(sender, e);
    }
    private void YTextBox_Leave(object sender, EventArgs e)
    {
      int y;
      if (int.TryParse(YTextBox.Text, out y))
        YTextBox_Update(sender, e);
    }
    private void YTextBox_Update(object sender, EventArgs e)
    {
      int y = int.Parse(YTextBox.Text);
      
      if (y != dotPosition.Y)
      {
        if (y > ServoController.MAX_INTEGER_INPUT)
        {
          y = ServoController.MAX_INTEGER_INPUT;
          YTextBox.Text = y.ToString();
        }
        else if (y < ServoController.MIN_INTEGER_INPUT)
        {
          y = ServoController.MIN_INTEGER_INPUT;
          YTextBox.Text = y.ToString();
        }

        dotPosition.Y = y;
        redDot.Location = new Point(redDot.Location.X, CENTER_GRID_Y - (dotPosition.Y - Y_CENTER));
        VTextBox.Text = (dotPosition.VAngle).ToString();
        //ServoController.sendPosition(dotPosition);
      }
    }

    private void HTextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar.Equals('\r'))
        HTextBox_Update(sender, e);
    }
    private void HTextBox_Leave(object sender, EventArgs e)
    {
      int x;
      if (int.TryParse(HTextBox.Text, out x))
        HTextBox_Update(sender, e);
    }
    private void HTextBox_Update(object sender, EventArgs e)
    {
      int hAngle = int.Parse(HTextBox.Text);
      if (hAngle != dotPosition.HAngle)
      {
        if (hAngle > ServoController.MAX_H_ANGLE)
        {
          hAngle = (int)ServoController.MAX_H_ANGLE;
          HTextBox.Text = hAngle.ToString();
        }
        else if (hAngle < ServoController.MIN_H_ANGLE)
        {
          hAngle = (int)ServoController.MIN_H_ANGLE;
          HTextBox.Text = hAngle.ToString();
        }

        dotPosition.HAngle = hAngle;
        redDot.Location = new Point((dotPosition.X - X_CENTER) + CENTER_GRID_X, redDot.Location.Y);
        XTextBox.Text = (dotPosition.X).ToString();
        //ServoController.sendPosition(dotPosition);
      }
    }

    private void VTextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar.Equals('\r'))
        VTextBox_Update(sender, e);
    }
    private void VTextBox_Leave(object sender, EventArgs e)
    {
      int y;
      if (int.TryParse(VTextBox.Text, out y))
        VTextBox_Update(sender, e);
    }
    private void VTextBox_Update(object sender, EventArgs e)
    {
      int vAngle = int.Parse(VTextBox.Text);
      if (vAngle != dotPosition.VAngle)
      {
        if (vAngle > ServoController.MAX_V_ANGLE)
        {
          vAngle = (int)ServoController.MAX_V_ANGLE;
          VTextBox.Text = vAngle.ToString();
        }
        else if (vAngle < ServoController.MIN_V_ANGLE)
        {
          vAngle = (int)ServoController.MIN_V_ANGLE;
          VTextBox.Text = vAngle.ToString();
        }
        
        dotPosition.VAngle = vAngle;
        redDot.Location = new Point(redDot.Location.X, CENTER_GRID_Y - (dotPosition.Y - Y_CENTER));
        YTextBox.Text = (dotPosition.Y).ToString();
        //ServoController.sendPosition(dotPosition);
      }
    }

    private void upButton_Click(object sender, EventArgs e)
    {
        //increment the Y value stored in the sd21 registers
        if (currentY + 50 < 2000)
        {
            currentY = currentY + 50;

            //num = (char)currentY;
            usbSendBuff[0] = 0x01;
            usbSendBuff[1] = currentXL;
            usbSendBuff[2] = currentXH;
            usbSendBuff[3] = 0x4C;
            usbSendBuff[4] = 0x04;
            usbRcvBuff = usbHub.getdata(usbSendBuff);
        }
        else
            return;
    }

    private void downButton_Click(object sender, EventArgs e)
    {
        //decrement the Y value stored in the sd21 registers
        if (currentY - 50 < 1000)
        {
            currentY = currentY - 50;
            usbSendBuff[0] = 0x01;
            usbSendBuff[1] = currentXL;
            usbSendBuff[2] = currentXH;
            usbSendBuff[3] = 0x01;
            usbSendBuff[4] = 0x01;
            usbRcvBuff = usbHub.getdata(usbSendBuff);
        }
        else
            return;
    }

    private void leftButton_Click(object sender, EventArgs e)
    {
        //increment the X value stored in the sd21 registers
        if (currentX + 50 < 2000)
        {
            currentX = currentX + 50;
            usbSendBuff[0] = 0x01;
            usbSendBuff[1] = 0x01;
            usbSendBuff[2] = 0x01;
            usbSendBuff[3] = currentYL;
            usbSendBuff[4] = currentYH;
            usbRcvBuff = usbHub.getdata(usbSendBuff);
        }
        else
            return;
    }

    private void rightButton_Click(object sender, EventArgs e)
    {
        //decrement the X value stored in the sd21 registers
        if (currentX - 50 > 2000)
        {
            currentX = currentX - 50;
            usbSendBuff[0] = 0x01;
            usbSendBuff[1] = 0x01;
            usbSendBuff[2] = 0x01;
            usbSendBuff[3] = currentYL;
            usbSendBuff[4] = currentYH;
            usbRcvBuff = usbHub.getdata(usbSendBuff);
        }
        else
            return;
    }

    private void centerButton_Click(object sender, EventArgs e)
    {
        currentX = 1500;
        currentY = 1500;
        //center the servo position
        usbSendBuff[0] = 0x01;
        usbSendBuff[1] = 0xDC;
        usbSendBuff[2] = 0x05;
        usbSendBuff[3] = 0xDC;
        usbSendBuff[4] = 0x05;
        usbRcvBuff = usbHub.getdata(usbSendBuff);
    }


  }
}
