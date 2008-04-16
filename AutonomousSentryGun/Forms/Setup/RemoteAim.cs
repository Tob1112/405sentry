using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using AForge.Video;
using AForge.Video.VFW;
using AForge.Video.DirectShow;
using usb_api;

using AutonomousSentryGun.Functions;
using AutonomousSentryGun.Objects;

namespace AutonomousSentryGun.Forms.Test
{

  public partial class RemoteAim : Form
  {
    //need to add ability to change servos range in real time

    byte[] usbRcvBuff;

    char num;
    bool fireOK = false;

    private Servos servos;
    private const int REDDOT_OFFSET_X = 4;
    private const int REDDOT_OFFSET_Y = 4;
    private int positionIncrement = 25;

    private bool isDragging = false;
    private int currentX, currentY;

    public RemoteAim(String vcd)
    {
      InitializeComponent();

      VideoCaptureDevice videoSource = new VideoCaptureDevice(vcd, new Size(320, 240), false);
      OpenVideoSource(videoSource);

      redDot.Location = new Point(gridBox.Width / 2 + gridBox.Left, gridBox.Height / 2 + gridBox.Top);
      servos = new Servos(1600, 1477);
      XTextBox.Text = servos.GetMathPosition().X.ToString();
      YTextBox.Text = servos.GetMathPosition().Y.ToString();
      redDot.Location = servos.GetPorportionalMathPosition(gridBox.Bounds);
      redDot.Location = new Point(redDot.Location.X - REDDOT_OFFSET_X, redDot.Location.Y - REDDOT_OFFSET_Y);
      label1.Text = "(-" + servos.CenterServosPosition.X + "," + servos.CenterServosPosition.Y + ")";
      PosIncTextBox.Text = positionIncrement.ToString();
      textBoxXServo.Text = servos.ShootingRange.Width.ToString();
      textBoxYServo.Text = servos.ShootingRange.Height.ToString();
      textBoxXCoord.Text = servos.ServosPosition.X.ToString();
      textBoxYCoord.Text = servos.ServosPosition.Y.ToString();

      Packet packet = new Packet(servos.CenterServosPosition);
      packet.setFireOff();
      this.sendData(packet);
    }

    private void XTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      KeyDownUpProcess(e);
    }
    private void XTextBox_Leave(object sender, EventArgs e)
    {
      int x, y;
      if (int.TryParse(XTextBox.Text, out x) && int.TryParse(YTextBox.Text, out y)) remoteAim();
    }

    private void remoteAim()
    {
      int x = int.Parse(XTextBox.Text);
      int y = int.Parse(YTextBox.Text);
      servos.ServosPosition = servos.ConvertPositionMathToServos(new Point(x, y));
      XTextBox.Text = servos.GetMathPosition().X.ToString();
      YTextBox.Text = servos.GetMathPosition().Y.ToString();
      redDot.Location = servos.GetPorportionalMathPosition(gridBox.Bounds);
      redDot.Location = new Point(redDot.Location.X - REDDOT_OFFSET_X, redDot.Location.Y - REDDOT_OFFSET_Y);
      Packet packet = new Packet(servos.ServosPosition);
      if (fireOK == true)
          packet.setFireOn();
      else
          packet.setFireOff();
      this.sendData(packet);
    }

    private void YTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      KeyDownUpProcess(e);
      textBoxXCoord.Text = servos.ServosPosition.X.ToString();
      textBoxYCoord.Text = servos.ServosPosition.Y.ToString();
    }

    private void KeyDownUpProcess(KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          {
            int x, y;
            if (int.TryParse(XTextBox.Text, out x) && int.TryParse(YTextBox.Text, out y)) remoteAim(); break;
          }
        case Keys.Up:
          {
            YTextBox.Text = (Convert.ToInt32(YTextBox.Text) + positionIncrement).ToString();
            remoteAim(); break;
          }
        case Keys.Down:
          {
            YTextBox.Text = (Convert.ToInt32(YTextBox.Text) - positionIncrement).ToString();
            remoteAim(); break;
          }
        case Keys.Left:
          {
            XTextBox.Text = (Convert.ToInt32(XTextBox.Text) - positionIncrement).ToString();
            remoteAim(); break;
          }
        case Keys.Right:
          {
            XTextBox.Text = (Convert.ToInt32(XTextBox.Text) + positionIncrement).ToString();
            remoteAim(); break;
          }
        case Keys.Space:
          {
              fireBox.Checked = !fireBox.Checked;
              remoteAim(); break;
          }

      }
    }
    private void YTextBox_Leave(object sender, EventArgs e)
    {
      int x, y;
      if (int.TryParse(XTextBox.Text, out x) && int.TryParse(YTextBox.Text, out y)) remoteAim();
    }
    private void YTextBox_Update(object sender, EventArgs e)
    {
      remoteAim();
    }

    private void sendData(Packet packet)
    {
      try
      {
        usbRcvBuff = AutonomousSentryGun.Program.usbHub.getdata(packet.Data);
      }
      catch (Exception e)
      {
        //MessageBox.Show("The zigbee module is not plugged into the usb.");
      }
    }

    private void centerButton_Click(object sender, EventArgs e)
    {
      int x = 0;
      int y = 0;
      servos.ServosPosition = servos.ConvertPositionMathToServos(new Point(x, y));
      XTextBox.Text = servos.GetMathPosition().X.ToString();
      YTextBox.Text = servos.GetMathPosition().Y.ToString();
      redDot.Location = servos.GetPorportionalMathPosition(gridBox.Bounds);
      redDot.Location = new Point(redDot.Location.X - REDDOT_OFFSET_X, redDot.Location.Y - REDDOT_OFFSET_Y);
      textBoxXCoord.Text = servos.CenterServosPosition.X.ToString();
      textBoxYCoord.Text = servos.CenterServosPosition.Y.ToString();
      Packet packet = new Packet(servos.CenterServosPosition);
      packet.setFireOff();
      this.sendData(packet);
    }

    private void RemoteAim_KeyDown(object sender, KeyEventArgs e)
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

    private void fireBox_CheckedChanged(object sender, EventArgs e)
    {  
        //now fire
        if (fireOK == false)
        {
            fireOK = true;
            redDot.BackColor = Color.Red;
        }
        //firing before so dont fire
        else
        {
            fireOK = false;
            redDot.BackColor = Color.Yellow;
        }      
    }

    private void RemoteAim_FormClosing(object sender, FormClosingEventArgs e)
    {
        CloseVideoSource();

        Packet packet = new Packet(servos.CenterServosPosition);
        packet.setFireOff();
        sendData(packet);
    }

    // Open video source
    private void OpenVideoSource(AForge.Video.IVideoSource source)
    {
        // set busy cursor
        this.Cursor = Cursors.WaitCursor;

        // close previous video source
        CloseVideoSource();

        // create camera
        Camera camera = new Camera(source, null);
        //camera.NewFrame += new EventHandler(camera_NewFrame);
        // start camera
        camera.Start();

        // attach camera to camera window
        gridBox.Camera = camera;             

        this.Cursor = Cursors.Default;
    }

    // Close current video source
    private void CloseVideoSource()
    {
        Camera camera = gridBox.Camera;

        if (camera != null)
        {
            // detach camera from camera window
            gridBox.Camera = null;
            Application.DoEvents();

            // signal camera to stop
            camera.SignalToStop();
            // wait 2 seconds until camera stops
            for (int i = 0; (i < 20) && (camera.IsRunning); i++)
            {
                Thread.Sleep(100);
            }
            if (camera.IsRunning)
                camera.Stop();
            camera = null;            
        }
    }

    private void redDot_MouseDown(object sender, MouseEventArgs e)
    {
        isDragging = true;
        CoordinateTimer.Start();

        currentX = e.X;
        currentY = e.Y;
    }

    private void redDot_MouseMove(object sender, MouseEventArgs e)
    {
        if (isDragging)
        {
            redDot.Top = redDot.Top + (e.Y - currentY);
            redDot.Left = redDot.Left + (e.X - currentX);
        }
    }

    private void redDot_MouseUp(object sender, MouseEventArgs e)
    {
        CoordinateTimer.Stop();
        isDragging = false;

        if (redDot.Top < gridBox.Top)
            redDot.Top = gridBox.Top;
        else if (redDot.Top > gridBox.Top + gridBox.Height)
            redDot.Top = gridBox.Top + gridBox.Height - redDot.Height;

        if (redDot.Left < gridBox.Left)
            redDot.Left = gridBox.Left;
        else if (redDot.Left > gridBox.Left + gridBox.Width)
            redDot.Left = gridBox.Left + gridBox.Width - redDot.Width;
    }

    private void CoordinateTimer_Tick(object sender, EventArgs e)
    {
        servos.SetPorportionalMathPosition(gridBox.Bounds, new Point(redDot.Location.X + REDDOT_OFFSET_X, redDot.Location.Y + REDDOT_OFFSET_Y));
        XTextBox.Text = servos.GetMathPosition().X.ToString();
        YTextBox.Text = servos.GetMathPosition().Y.ToString();                
        Packet packet = new Packet(servos.ServosPosition);
        textBoxXCoord.Text = servos.ServosPosition.X.ToString();
        textBoxYCoord.Text = servos.ServosPosition.Y.ToString();
        if (fireOK == true)
            packet.setFireOn();
        else
            packet.setFireOff();
        this.sendData(packet);
    }

    private void textBoxYServo_MouseLeave(object sender, EventArgs e)
    {
      servos.SetShootingRangeSize(Convert.ToInt32(textBoxXServo.Text), Convert.ToInt32(textBoxYServo.Text));
      textBoxXServo.Text = servos.ShootingRange.Width.ToString();
      textBoxYServo.Text = servos.ShootingRange.Height.ToString();
    }

    private void textBoxXServo_MouseLeave(object sender, EventArgs e)
    {
      servos.SetShootingRangeSize(Convert.ToInt32(textBoxXServo.Text), Convert.ToInt32(textBoxYServo.Text));
      textBoxXServo.Text = servos.ShootingRange.Width.ToString();
      textBoxYServo.Text = servos.ShootingRange.Height.ToString();
    }


  }
}
