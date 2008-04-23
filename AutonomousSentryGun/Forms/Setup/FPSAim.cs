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
using AutonomousSentryGun;

namespace AutonomousSentryGun.Forms.Test
{

  public partial class FPSAim : Form
  {
    //need to increase servos range for this form

    bool usbRcvBuff;
    
    bool fireOK = false;
    bool cursorhidden = false;

    private Servos servos;
    private const int REDDOT_OFFSET_X = 2;
    private const int REDDOT_OFFSET_Y = 2;

    int sensitivity = 5;
    
    private int currentX, currentY, lastX, lastY;

    public FPSAim(String vcd)
    {
      InitializeComponent();

      VideoCaptureDevice videoSource = new VideoCaptureDevice(vcd, new Size(320, 240), false);
      OpenVideoSource(videoSource);

      redDot.Location = new Point(gridBox.Width / 2 + gridBox.Left, gridBox.Height / 2 + gridBox.Top);
      servos = new Servos(1600, 1477);      
      redDot.Location = servos.GetPorportionalMathPosition(gridBox.Bounds);
      redDot.Location = new Point(redDot.Location.X - REDDOT_OFFSET_X, redDot.Location.Y - REDDOT_OFFSET_Y);
      label1.Text = "(-" + servos.CenterServosPosition.X + "," + servos.CenterServosPosition.Y + ")";      
      textBoxXServo.Text = servos.ShootingRange.Width.ToString();
      textBoxYServo.Text = servos.ShootingRange.Height.ToString();
      textBoxXCoord.Text = servos.ServosPosition.X.ToString();
      textBoxYCoord.Text = servos.ServosPosition.Y.ToString();

      Packet packet = new Packet(servos.CenterServosPosition);
      packet.setFireOff();
      this.sendData(packet);
      Cursor.Hide();
      cursorhidden = true;
      CoordinateTimer.Start();      

      //Point camwindowcenter = new Point(gridBox.PointToScreen(new Point(0, 0)).X , gridBox.PointToScreen(new Point(0, 0)).Y );
      //Cursor.Position = camwindowcenter;
      currentX = Cursor.Position.X;
      currentY = Cursor.Position.Y;
      lastX = 0;
      lastY = 0;
    }

    private void KeyDownUpProcess(KeyEventArgs e)
    {
      switch (e.KeyCode)
      {        
        case Keys.Space:
          {
              //isDragging = !isDragging;
              if (CoordinateTimer.Enabled)
              {
                  CoordinateTimer.Stop();
              }
              else
              {
                  CoordinateTimer.Start();
              }
              if (cursorhidden)
              {
                  Cursor.Show();
                  cursorhidden = false;
              }
              else
              {
                  Cursor.Hide();
                  cursorhidden = true;
              }
              fireOK = false;
              redDot.BackColor = Color.Yellow;
              break;
          }

      }
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
      lastX = 0;
      lastY = 0;
      servos.ServosPosition = servos.ConvertPositionMathToServos(new Point(x, y));      
      redDot.Location = servos.GetPorportionalMathPosition(gridBox.Bounds);
      redDot.Location = new Point(redDot.Location.X - REDDOT_OFFSET_X, redDot.Location.Y - REDDOT_OFFSET_Y);
      textBoxXCoord.Text = servos.CenterServosPosition.X.ToString();
      textBoxYCoord.Text = servos.CenterServosPosition.Y.ToString();
      Packet packet = new Packet(servos.CenterServosPosition);
      packet.setFireOff();
      this.sendData(packet);
    }

    private void FPSAim_KeyDown(object sender, KeyEventArgs e)
    {
      KeyDownUpProcess(e);
    }

    private void gridBox_KeyDown(object sender, KeyEventArgs e)
    {
      KeyDownUpProcess(e);
    }

    private void FPSAim_FormClosing(object sender, FormClosingEventArgs e)
    {
        CoordinateTimer.Stop();
        CloseVideoSource();
        Cursor.Show();

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

    private void CoordinateTimer_Tick(object sender, EventArgs e)
    {
        int deltaX = Cursor.Position.X - currentX;
        int deltaY = Cursor.Position.Y - currentY;
        lastX += deltaX/sensitivity;
        lastY -= deltaY/sensitivity;
        
        if (lastX > servos.ShootingRange.Width / 2)
        {
            lastX = servos.ShootingRange.Width / 2;
        }
        else if (lastX < -servos.ShootingRange.Width / 2)
        {
            lastX = -servos.ShootingRange.Width / 2;
        }
        if (lastY > servos.ShootingRange.Height / 2)
        {
            lastY = servos.ShootingRange.Height / 2;
        }
        else if (lastY < -servos.ShootingRange.Height / 2)
        {
            lastY = -servos.ShootingRange.Height / 2;
        }
        //MessageBox.Show(lastX.ToString() + " " + lastY.ToString());
        servos.ServosPosition = servos.ConvertPositionMathToServos(new Point(lastX, lastY));
        //servos.SetPorportionalMathPosition(gridBox.Bounds, new Point(lastX, lastY));        
        Packet packet = new Packet(servos.ServosPosition);
        textBoxXCoord.Text = servos.ServosPosition.X.ToString();
        textBoxYCoord.Text = servos.ServosPosition.Y.ToString();
        if (fireOK == true)
            packet.setFireOn();
        else
            packet.setFireOff();
        this.sendData(packet);
        Cursor.Position = new Point(currentX, currentY);
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

    private void FPSAim_MouseDown(object sender, MouseEventArgs e)
    {            
        fireOK = true;
        redDot.BackColor = Color.Red; 
    }

    private void FPSAim_MouseUp(object sender, MouseEventArgs e)
    {         
        fireOK = false;
        redDot.BackColor = Color.Yellow;
    }

    private void gridBox_MouseDown(object sender, MouseEventArgs e)
    {        
        fireOK = true;
        redDot.BackColor = Color.Red; 
    }

    private void gridBox_MouseUp(object sender, MouseEventArgs e)
    {        
        fireOK = false;
        redDot.BackColor = Color.Yellow;
    }

    private void sensitivitytrackBar_Scroll(object sender, EventArgs e)
    {
        sensitivity = sensitivitytrackBar.Value;
    }
  }
}
