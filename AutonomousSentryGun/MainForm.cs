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
using AForge.Vision.Motion;

using AutonomousSentryGun.Forms.Test;


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

    public MainForm()
    {
      InitializeComponent();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void resetToDefaultToolStripMenuItem_Click(object sender, EventArgs e)
    {
      MessageBox.Show("Settings have been reset to the default.");
    }

    private void transmitPositionToolStripMenuItem_Click(object sender, EventArgs e)
    {
     TransmitPosition form = new TransmitPosition();
     form.Show();
    }

    #region Motion Functions/Events

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {        
        CloseVideoSource();
    }

    private void onOffCameraToolStripMenuItem_Click(object sender, EventArgs e)
    {
        
        if (!onOffCameraToolStripMenuItem.Checked)
        {
            VideoCaptureDeviceForm form = new VideoCaptureDeviceForm();

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                // create video source
                VideoCaptureDevice videoSource = new VideoCaptureDevice(form.VideoDevice);

                // open it
                OpenVideoSource(videoSource);
            }                        
            onOffCameraToolStripMenuItem.Checked = true;
        }
        else
        {
            if (onOffToolStripMenuItem.Checked)
            {
                SetMotionDetector(null);
                onOffToolStripMenuItem.Checked = false;
            }
            CloseVideoSource();
            onOffCameraToolStripMenuItem.Checked = false;
        }
    }

    /*private void cameraFeedToolStripMenuItem_Click(object sender, EventArgs e)
    {
        VideoCaptureDeviceForm form = new VideoCaptureDeviceForm();

        if (form.ShowDialog(this) == DialogResult.OK)
        {
            // create video source
            VideoCaptureDevice videoSource = new VideoCaptureDevice(form.VideoDevice);

            // open it
            OpenVideoSource(videoSource);
        }
    }*/

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

    private void onOffToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (cameraWindow1.Camera != null)
        {
            if (!onOffToolStripMenuItem.Checked)
            {
                CountingMotionDetector cmd = new CountingMotionDetector(true);
                cmd.MinObjectsWidth = 80;
                cmd.MinObjectsHeight = 80;
                cmd.MaxObjectsWidth = cameraWindow1.Width;
                cmd.MaxObjectsHeight = cameraWindow1.Height;
                SetMotionDetector(cmd);
                onOffToolStripMenuItem.Checked = true;
                //MessageBox.Show(String.Concat(cameraWindow1.Height.ToString(), cameraWindow1.Width.ToString()));
            }
            else
            {
                SetMotionDetector(null);
                onOffToolStripMenuItem.Checked = false;
            }
        }
        else
        {
            MessageBox.Show("No Active Camera!");
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

#endregion   

    
          
  }
}
