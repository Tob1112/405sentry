using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AutonomousSentryGun.Functions;

namespace AutonomousSentryGun.Forms.Test
{
  public partial class TransmitPosition : Form
  {
    private readonly static int CENTER_GRID_X = 139;
    private readonly static int CENTER_GRID_Y = 137;
    private readonly int X_CENTER;
    private readonly int Y_CENTER;
    private Position dotPosition;
    public TransmitPosition()
    {
      InitializeComponent();

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
        ServoController.sendPosition(dotPosition);
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
        ServoController.sendPosition(dotPosition);
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
        ServoController.sendPosition(dotPosition);
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
        ServoController.sendPosition(dotPosition);
      }
    }
  }
}
