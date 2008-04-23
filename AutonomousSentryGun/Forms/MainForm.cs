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
using System.Threading;
using AForge.Video;
using AForge.Video.VFW;
using AForge.Video.DirectShow;
using AForge.Vision.Motion;
using usb_api;

using AutonomousSentryGun.Functions;
using AutonomousSentryGun.Objects;
using AutonomousSentryGun.Forms.Test;
using AutonomousSentryGun.Forms.Setup;

//code to play sound taken from
//http://www.codeproject.com/KB/audio-video/PlaySounds1.aspx

/* Blake: Stuff To Do
 *  
 * need to test and optimize aiming code for leading targets 
 * 
 * play with different settings for frame size, frame rate, with and without erosion
 * real life testing
 *       
 * clean up interface/comment code 
*/
namespace AutonomousSentryGun
{
  public partial class MainForm : Form
  {
    // motion detector
    private IMotionDetector detector = null;

    // statistics length
    private const int statLength = 15;
    // current statistics index
    private int statIndex = 0;
    // ready statistics values
    private int statReady = 0;
    // statistics array
    private int[] statCount = new int[statLength];
    // current working directory
    string pwd = Environment.CurrentDirectory;

    private bool soundOn = false;
    private bool firingOn = false;

    // servo coordinates object
    private Servos servos = new Servos(1600, 1477);
    //create the USB interface
    //private usb_interface usbHub = new usb_interface();
    //usb buffer
    private bool usbRcvBuff;

    [DllImport("winmm.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
    static extern bool PlaySound(string pszSound, IntPtr hMod, SoundFlags sf);

    // Flags for playing sounds.  For this example, we are reading 
    // the sound from a filename, so we need only specify 
    // SND_FILENAME | SND_ASYNC
    [Flags]
    public enum SoundFlags : int
    {
      SND_SYNC = 0x0000,  // play synchronously (default) 
      SND_ASYNC = 0x0001,  // play asynchronously 
      SND_NODEFAULT = 0x0002,  // silence (!default) if sound not found 
      SND_MEMORY = 0x0004,  // pszSound points to a memory file
      SND_LOOP = 0x0008,  // loop the sound until next sndPlaySound 
      SND_NOSTOP = 0x0010,  // don't stop any currently playing sound 
      SND_NOWAIT = 0x00002000, // don't wait if the driver is busy 
      SND_ALIAS = 0x00010000, // name is a registry alias 
      SND_ALIAS_ID = 0x00110000, // alias is a predefined ID
      SND_FILENAME = 0x00020000, // name is file name 
      SND_RESOURCE = 0x00040004  // name is resource name or atom 
    }

    public MainForm()
    {
      InitializeComponent();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Packet packet = new Packet(servos.CenterServosPosition);
      packet.setFireOff();
      sendData(packet);
      Application.Exit();
    }

    private void resetToDefaultToolStripMenuItem_Click(object sender, EventArgs e)
    {
      MessageBox.Show("Settings have been reset to the default.");
    }

    private void transmitPositionToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AllOffButton_Click(sender, e);
      TransmitPosition form = new TransmitPosition();
      form.Show();
    }

    private void dataTransmissionToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DataTransmission dtForm = new DataTransmission(TrackingTimer);
      dtForm.Show();
    }

    private void loadSetupFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
      openFileDialog1.ShowDialog();
    }

    private void saveSetupFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
      saveFileDialog1.ShowDialog();
    }

    #region Motion Functions/Events

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      AllOffButton_Click(sender, e);
    }

    private void remoteAimToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AllOffButton_Click(sender, e);

      VideoCaptureDeviceForm form = new VideoCaptureDeviceForm();

      if (form.ShowDialog(this) == DialogResult.OK)
      {
        RemoteAim ra = new RemoteAim(form.VideoDevice);
        ra.Show();
      }
    }

    private void fPSToolStripMenuItem_Click(object sender, EventArgs e)
    {
        AllOffButton_Click(sender, e);

        VideoCaptureDeviceForm form = new VideoCaptureDeviceForm();

        if (form.ShowDialog(this) == DialogResult.OK)
        {
            FPSAim fps = new FPSAim(form.VideoDevice);
            fps.Show();
        }
    }

    // Open video source
    private void OpenVideoSource(AForge.Video.IVideoSource source)
    {
      // set busy cursor
      this.Cursor = Cursors.WaitCursor;

      // close previous video source
      CloseVideoSource();

      // create camera
      Camera camera = new Camera(source, detector);
      camera.NewFrame += new EventHandler(camera_NewFrame);
      // start camera
      camera.Start();

      // attach camera to camera window
      cameraWindow1.Camera = camera;

      // reset statistics
      statIndex = statReady = 0;

      // start timer        
      timer.Start();

      this.Cursor = Cursors.Default;
    }

    // Close current video source
    private void CloseVideoSource()
    {
      Camera camera = cameraWindow1.Camera;

      if (camera != null)
      {
        // stop timer
        timer.Stop();

        // detach camera from camera window
        cameraWindow1.Camera = null;
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

        // reset motion detector
        if (detector != null)
          detector.Reset();
      }
    }

    // On timer event - gather statistics
    private void timer_Tick(object sender, EventArgs e)
    {
      Camera camera = cameraWindow1.Camera;

      if (camera != null)
      {
        // get number of frames for the last second
        statCount[statIndex] = camera.FramesReceived;

        // increment indexes
        if (++statIndex >= statLength)
          statIndex = 0;
        if (statReady < statLength)
          statReady++;

        float fps = 0;

        // calculate average value
        for (int i = 0; i < statReady; i++)
        {
          fps += statCount[i];
        }
        fps /= statReady;

        statCount[statIndex] = 0;

        fpsLabel.Text = fps.ToString("F2") + " fps";
      }
    }

    private Point lastCenterPosition = new Point();
    private Point lastLeadingPosition = new Point();
    private bool firingsoundplayed = false;
    private bool ceasefiringsoundplayed = true;
    private int soundTracker = 0;
    //Timer that tracks refresh of position tracking
    private void TrackingTimer_Tick(object sender, EventArgs e) //125ms refresh rate as of right now
    {
      if (cameraWindow1.Camera != null && cameraWindow1.Camera.MotionDetector != null)
      {
        CountingMotionDetector cmd = (CountingMotionDetector)cameraWindow1.Camera.MotionDetector;
        Rectangle[] rectmotion = cmd.ObjectRectangles;

        if (rectmotion.Length != 0)
        {
          if (!firingsoundplayed && soundOn)
          {
            switch (soundTracker % 4)
            {
              case 0:
                PlaySound(pwd + "\\Sounds\\Sentry Sounds\\Target Found\\firing.wav", IntPtr.Zero, SoundFlags.SND_FILENAME | SoundFlags.SND_ASYNC);
                firingsoundplayed = true;
                break;
              case 1:
                PlaySound(pwd + "\\Sounds\\Sentry Sounds\\Target Found\\i_see_you.wav", IntPtr.Zero, SoundFlags.SND_FILENAME | SoundFlags.SND_ASYNC);
                firingsoundplayed = true;
                break;
              case 2:
                PlaySound(pwd + "\\Sounds\\Sentry Sounds\\Target Found\\target_acquired.wav", IntPtr.Zero, SoundFlags.SND_FILENAME | SoundFlags.SND_ASYNC);
                firingsoundplayed = true;
                break;
              case 3:
                PlaySound(pwd + "\\Sounds\\Sentry Sounds\\Target Found\\there_you_are.wav", IntPtr.Zero, SoundFlags.SND_FILENAME | SoundFlags.SND_ASYNC);
                firingsoundplayed = true;
                break;
            }
          }
          ceasefiringsoundplayed = false;
          aimDot.BackColor = Color.Red;
          //aimDot.Visible = true;
          //calculate largest detected motion area
          Rectangle largest = rectmotion[0];
          for (int i = 1; i < rectmotion.Length; i++)
          {
            if (rectmotion[i].Width * rectmotion[i].Height > largest.Width * largest.Height)
            {
              largest = rectmotion[i];
            }
          }

          //get center of largest motion area
          int x = largest.X + largest.Width / 2 - 2 + cameraWindow1.Location.X;
          int y = largest.Y + largest.Height / 2 - 2 + cameraWindow1.Location.Y;

          //calculate difference from last center point and adjust target lead depending on speed
          if (lastCenterPosition.IsEmpty)
          {
            lastCenterPosition = new Point(x + 2, y + 2);
            lastLeadingPosition = new Point(lastCenterPosition.X, lastCenterPosition.Y);
          }
          else if (!lastCenterPosition.IsEmpty)
          {
            int xdiff = x - lastCenterPosition.X;
            int ydiff = y - lastCenterPosition.Y;
            lastLeadingPosition = new Point(x + xdiff, y + ydiff);
            lastCenterPosition = new Point(x + 2, y + 2);
          }

          //set aimdot location
          aimDot.Location = new Point(lastLeadingPosition.X - 2, lastLeadingPosition.Y - 2);

          //create packet, set servo position, and fire
          servos.SetPorportionalMathPosition(cameraWindow1.Bounds, lastLeadingPosition);
          Packet packet = new Packet(servos.ServosPosition);
          //Console.WriteLine("(" + servos.Position.X + "," + servos.Position.Y + ")");
          //MessageBox.Show("(" + servos.Position.X + "," + servos.Position.Y + ")");
          packet.setFireOff();
          if (firingOn)
          {
            packet.setFireOn();
          }
          sendData(packet);
        }
        else if (rectmotion.Length == 0)
        {
          if (!ceasefiringsoundplayed && soundOn)
          {
            switch (soundTracker % 4)
            {
              case 0:
                PlaySound(pwd + "\\Sounds\\Sentry Sounds\\Target lost\\searching.wav", IntPtr.Zero, SoundFlags.SND_FILENAME | SoundFlags.SND_ASYNC);

                ceasefiringsoundplayed = true;
                break;
              case 1:
                PlaySound(pwd + "\\Sounds\\Sentry Sounds\\Target lost\\is_anyone_there.wav", IntPtr.Zero, SoundFlags.SND_FILENAME | SoundFlags.SND_ASYNC);
                ceasefiringsoundplayed = true;
                break;
              case 2:
                PlaySound(pwd + "\\Sounds\\Sentry Sounds\\Target lost\\target_lost.wav", IntPtr.Zero, SoundFlags.SND_FILENAME | SoundFlags.SND_ASYNC);
                ceasefiringsoundplayed = true;
                break;
              case 3:
                PlaySound(pwd + "\\Sounds\\Sentry Sounds\\Target Lost\\are_you_still_there.wav", IntPtr.Zero, SoundFlags.SND_FILENAME | SoundFlags.SND_ASYNC);
                ceasefiringsoundplayed = true;
                break;
            }
            soundTracker++;
          }
          firingsoundplayed = false;
          //when no motion, center aimDot and return servos to center position
          aimDot.Location = new Point(cameraWindow1.Location.X + cameraWindow1.Width / 2 - 2, cameraWindow1.Location.Y + cameraWindow1.Height / 2 - 2);
          aimDot.BackColor = Color.Yellow;
          //aimDot.Visible = false;
          lastCenterPosition = new Point();
          lastLeadingPosition = new Point();
          Packet packet = new Packet(servos.CenterServosPosition);
          packet.setFireOff();
          sendData(packet);
        }
      }
      else
      {
        MessageBox.Show("No Active Camera w/ Active Motion Detector!");
        TrackingTimer.Stop();
      }
    }

    // Main window resized
    private void MainForm_SizeChanged(object sender, EventArgs e)
    {
      cameraWindow1.UpdatePosition();
    }

    // On new frame
    private void camera_NewFrame(object sender, System.EventArgs e)
    {
      if (detector is ICountingMotionDetector)
      {
        ICountingMotionDetector countingDetector = (ICountingMotionDetector)detector;
        objectsCountLabel.Text = "Objects: " + countingDetector.ObjectsCount.ToString();
      }
      else
      {
        objectsCountLabel.Text = "";
      }
    }

    // Set motion detector
    private void SetMotionDetector(IMotionDetector detector)
    {
      this.detector = detector;

      // set motion detector to camera
      Camera camera = cameraWindow1.Camera;

      if (camera != null)
      {
        camera.Lock();
        camera.MotionDetector = detector;
        // reset statistics
        statIndex = statReady = 0;
        camera.Unlock();
      }
    }



    private void motionDetectionSettingsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      MotionSettings MSdialog = new MotionSettings();
      if (cameraWindow1.Camera != null && cameraWindow1.Camera.MotionDetector != null)
      {
        MSdialog.setActiveCamera(cameraWindow1.Camera);
        MSdialog.Show();
      }
      else
      {
        MessageBox.Show("No Active Camera w/ Active Motion Detector!");
      }
    }



    private void onOffSoundMenuItem_Click(object sender, EventArgs e)
    {
      soundOn = !soundOn;
      onOffSoundMenuItem.Checked = !onOffSoundMenuItem.Checked;
    }

    private void sendData(Packet packet)
    {
      //try
      //{
        usbRcvBuff = AutonomousSentryGun.Program.usbHub.getdata(packet.Data);
      //}
      //catch (Exception e)
      //{
       // objectsCountLabel.Text = objectsCountLabel.Text + " - " + "The zigbee module is not plugged into the usb.";
      //}
    }

    #endregion
    #region Button Triggers
    private void CameraFeedButton_Click(object sender, EventArgs e)
    {
      if (CameraFeedButton.Text.Equals("OFF"))
        CameraFeedOn();
      else
        CameraFeedOff();
    }

    private void MotionDetectionButton_Click(object sender, EventArgs e)
    {
      if (MotionDetectionButton.Text.Equals("OFF"))
        MotionDetectionOn();
      else
        MotionDetectionOff();
    }

    private void GunTrackButton_Click(object sender, EventArgs e)
    {
      if (GunTrackButton.Text.Equals("OFF"))
        GunTrackOn();
      else
        GunTrackOff();
    }

    private void GunFireButton_Click(object sender, EventArgs e)
    {
      if (GunFireButton.Text.Equals("OFF"))
        GunFireOn();
      else
        GunFireOff();

    }

    private void AllOffButton_Click(object sender, EventArgs e)
    {
      GunFireOff();
      GunTrackOff();
      MotionDetectionOff();
      CameraFeedOff();
    }
    #endregion

    #region Camera Feed Functions
    private void CameraFeedOn()
    {

      VideoCaptureDeviceForm form = new VideoCaptureDeviceForm();

      if (form.ShowDialog(this) == DialogResult.OK)
      {
        // create video source
        //VideoCaptureDevice videoSource = new VideoCaptureDevice(form.VideoDevice, false);
        VideoCaptureDevice videoSource = new VideoCaptureDevice(form.VideoDevice, new Size(320, 240), false);

        // open it
        OpenVideoSource(videoSource);
        CameraFeedButton.Text = "ON";
        CameraFeedButton.BackColor = Color.Green;
        MotionDetectionButton.Enabled = true;
      }
    }


    private void CameraFeedOff()
    {
      CameraFeedButton.Text = "OFF";
      CameraFeedButton.BackColor = Color.Red;
      MotionDetectionButton.Enabled = false;

      CloseVideoSource();
    }
    #endregion
    #region Motion Detection Functions
    private void MotionDetectionOn()
    {
      if (cameraWindow1.Camera != null)
      {
        MotionDetectionButton.Text = "ON";
        MotionDetectionButton.BackColor = Color.Green;
        GunTrackButton.Enabled = true;
        CameraFeedButton.Enabled = false;
        CountingMotionDetector cmd = new CountingMotionDetector(true);
        cmd.MinObjectsWidth = 20;
        cmd.MinObjectsHeight = 20;
        cmd.MaxObjectsWidth = cameraWindow1.Width - 75;
        cmd.MaxObjectsHeight = cameraWindow1.Height;
        SetMotionDetector(cmd);
      }
      else
      {
        MessageBox.Show("No Active Camera!");
      }
    }

    private void MotionDetectionOff()
    {
      MotionDetectionButton.Text = "OFF";
      MotionDetectionButton.BackColor = Color.Red;
      GunTrackButton.Enabled = false;
      CameraFeedButton.Enabled = true;

      SetMotionDetector(null);
    }
    #endregion
    #region Gun Track Functions
    private void GunTrackOn()
    {
      if (cameraWindow1.Camera != null && cameraWindow1.Camera.MotionDetector != null)
      {

        //sentry activated sound    
        if (soundOn)
        {
          PlaySound(pwd + "\\Sounds\\Sentry Sounds\\Bootup\\sentry_mode_activated.wav", IntPtr.Zero, SoundFlags.SND_FILENAME | SoundFlags.SND_ASYNC);
        }

        TrackingTimer.Start();
        aimDot.Visible = true;
        //servos = new Servos(1600, 1477, cameraWindow1.Width / 2, cameraWindow1.Height / 2);
        //servos = new Servos(1600, 1477);

        GunTrackButton.Text = "ON";
        GunTrackButton.BackColor = Color.Green;
        GunFireButton.Enabled = true;
        MotionDetectionButton.Enabled = false;
      }
      else
      {
        MessageBox.Show("No Active Camera w/ Active Motion Detector!");
      }
    }

    private void GunTrackOff()
    {
      GunTrackButton.Text = "OFF";
      GunTrackButton.BackColor = Color.Red;
      GunFireButton.Enabled = false;
      MotionDetectionButton.Enabled = true;

      TrackingTimer.Stop();
      aimDot.Visible = false;

      //Packet packet = new Packet(servos.CenterServosPosition);
      //packet.setFireOff();
      //sendData(packet);

    }
    #endregion
    #region Gun Fire Functions
    private void GunFireOn()
    {
      GunFireButton.Text = "ON";
      GunFireButton.BackColor = Color.Green;
      GunTrackButton.Enabled = false;
    
      firingOn = true;
    }
    
    private void GunFireOff()
    {
      GunFireButton.Text = "OFF";
      GunFireButton.BackColor = Color.Red;
      GunTrackButton.Enabled = true;
      
      firingOn = false;
    }
    #endregion

  }
}
