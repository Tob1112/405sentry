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
using AutonomousSentryGun.Objects;

namespace AutonomousSentryGun.Forms.Test
{

  public partial class TransmitPosition : Form
  {
    //create the USB interface
    usb_interface usbHub;
    
    byte[] usbRcvBuff;
    
    char num;

    private Servos servos;
    public TransmitPosition()
    {
      InitializeComponent();

      redDot.Location = new Point(gridBox.Width / 2 + gridBox.Left, gridBox.Height / 2 + gridBox.Top);
      servos = new Servos(1600, 1477);
      XTextBox.Text = servos.ConvertPositionProgramToMath().X.ToString();
      YTextBox.Text = servos.ConvertPositionProgramToMath().Y.ToString();
      redDot.Location = servos.getPorportionalPosition(gridBox.ClientRectangle);
      label1.Text = "(-" + servos.ShootingRange.Width / 2 + ",-" + servos.ShootingRange.Height / 2 + ")";
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
      transmitPosition();
      
    }

    private void transmitPosition()
    {
      int x = int.Parse(XTextBox.Text);
      int y = int.Parse(YTextBox.Text);
      servos.Position = servos.ConvertPositionMathToProgram(new Point(x, y));
      XTextBox.Text = servos.ConvertPositionProgramToMath().X.ToString();
      YTextBox.Text = servos.ConvertPositionProgramToMath().Y.ToString();
      redDot.Location = servos.getPorportionalPosition(gridBox.ClientRectangle);
      Packet packet = new Packet(servos.PositionToServosController);
      packet.setFireOn();
      this.sendData(packet);
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
      transmitPosition();
    }
    
    private void upButton_Click(object sender, EventArgs e)
    {
        
        
    }

    private void sendData(Packet packet)
    {
      //usbRcvBuff = usbHub.getdata(packet.Data);
    }

    private void downButton_Click(object sender, EventArgs e)
    {
        
    }

    private void leftButton_Click(object sender, EventArgs e)
    {
        
    }

    private void rightButton_Click(object sender, EventArgs e)
    {
        
    }

    private void centerButton_Click(object sender, EventArgs e)
    {
   
    }

  }
}
