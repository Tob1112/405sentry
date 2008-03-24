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
    //usb_interface usbHub;

    byte[] usbRcvBuff;

    char num;

    private Servos servos;
    private const int REDDOT_OFFSET_X = 3;
    private const int REDDOT_OFFSET_Y = 3;
    private int positionIncrement = 25;


    public TransmitPosition()
    {
      InitializeComponent();
      //usbHub = new usb_interface();
      redDot.Location = new Point(gridBox.Width / 2 + gridBox.Left, gridBox.Height / 2 + gridBox.Top);
      servos = new Servos(1600, 1477);
      XTextBox.Text = servos.ConvertPositionProgramToMath().X.ToString();
      YTextBox.Text = servos.ConvertPositionProgramToMath().Y.ToString();
      redDot.Location = servos.getPorportionalPosition(gridBox.Bounds);
      redDot.Location = new Point(redDot.Location.X - REDDOT_OFFSET_X, redDot.Location.Y - REDDOT_OFFSET_Y);
      label1.Text = "(-" + servos.ShootingRange.Width / 2 + ",-" + servos.ShootingRange.Height / 2 + ")";
      PosIncTextBox.Text = positionIncrement.ToString();
      Packet packet = new Packet(servos.PositionToServosController);
      packet.setFireOn();
      this.sendData(packet);
    }

    private void XTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      KeyDownUpProcess(e);
    }
    private void XTextBox_Leave(object sender, EventArgs e)
    {
      int x, y;
      if (int.TryParse(XTextBox.Text, out x) && int.TryParse(YTextBox.Text, out y)) transmitPosition();
    }

    private void transmitPosition()
    {
      int x = int.Parse(XTextBox.Text);
      int y = int.Parse(YTextBox.Text);
      servos.Position = servos.ConvertPositionMathToProgram(new Point(x, y));
      XTextBox.Text = servos.ConvertPositionProgramToMath().X.ToString();
      YTextBox.Text = servos.ConvertPositionProgramToMath().Y.ToString();
      redDot.Location = servos.getPorportionalPosition(gridBox.Bounds);
      redDot.Location = new Point(redDot.Location.X - REDDOT_OFFSET_X, redDot.Location.Y - REDDOT_OFFSET_Y);
      Packet packet = new Packet(servos.PositionToServosController);
      packet.setFireOn();
      this.sendData(packet);
    }

    private void YTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      KeyDownUpProcess(e);


    }

    private void KeyDownUpProcess(KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          {
            int x, y;
            if (int.TryParse(XTextBox.Text, out x) && int.TryParse(YTextBox.Text, out y)) transmitPosition(); break;
          }
        case Keys.Up:
          {
            YTextBox.Text = (Convert.ToInt32(YTextBox.Text) + positionIncrement).ToString();
            transmitPosition(); break;
          }
        case Keys.Down:
          {
            YTextBox.Text = (Convert.ToInt32(YTextBox.Text) - positionIncrement).ToString();
            transmitPosition(); break;
          }
        case Keys.Left:
          {
            XTextBox.Text = (Convert.ToInt32(XTextBox.Text) - positionIncrement).ToString();
            transmitPosition(); break;
          }
        case Keys.Right:
          {
            XTextBox.Text = (Convert.ToInt32(XTextBox.Text) + positionIncrement).ToString();
            transmitPosition(); break;
          }

      }
    }
    private void YTextBox_Leave(object sender, EventArgs e)
    {
      int x, y;
      if (int.TryParse(XTextBox.Text, out x) && int.TryParse(YTextBox.Text, out y)) transmitPosition();
    }
    private void YTextBox_Update(object sender, EventArgs e)
    {
      transmitPosition();
    }

    private void sendData(Packet packet)
    {
        usbRcvBuff = AutonomousSentryGun.Program.usbHub.getdata(packet.Data);
    }


    private void centerButton_Click(object sender, EventArgs e)
    {
      int x = 0;
      int y = 0;
      servos.Position = servos.ConvertPositionMathToProgram(new Point(x, y));
      XTextBox.Text = servos.ConvertPositionProgramToMath().X.ToString();
      YTextBox.Text = servos.ConvertPositionProgramToMath().Y.ToString();
      redDot.Location = servos.getPorportionalPosition(gridBox.Bounds);
      redDot.Location = new Point(redDot.Location.X - REDDOT_OFFSET_X, redDot.Location.Y - REDDOT_OFFSET_Y);
      Packet packet = new Packet(servos.PositionToServosController);
      packet.setFireOn();
      this.sendData(packet);
    }

    private void TransmitPosition_KeyDown(object sender, KeyEventArgs e)
    {
      KeyDownUpProcess(e);

    }

    private void PosIncTextBox_TextChanged(object sender, EventArgs e)
    {
      int x;
      if (int.TryParse(PosIncTextBox.Text, out x))
        positionIncrement = Convert.ToInt32(PosIncTextBox.Text);
    }

    private void PosIncTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      KeyDownUpProcess(e);

    }

  }
}
