using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Vision.Motion;

namespace AutonomousSentryGun
{
    public partial class MotionSettings : Form
    {
        private Camera activecamera;

        public MotionSettings()
        {
            InitializeComponent();
        }

        public void setActiveCamera(Camera c)
        {
            activecamera = c;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (activecamera != null && activecamera.MotionDetector != null)
            {
                HBox.Text = ((CountingMotionDetector)activecamera.MotionDetector).MinObjectsHeight.ToString();
                HBar.Value = ((CountingMotionDetector)activecamera.MotionDetector).MinObjectsHeight;
                HBar.Maximum = ((CountingMotionDetector)activecamera.MotionDetector).MaxObjectsHeight/2;
                WBox.Text = ((CountingMotionDetector)activecamera.MotionDetector).MinObjectsWidth.ToString();
                WBar.Value = ((CountingMotionDetector)activecamera.MotionDetector).MinObjectsWidth;
                WBar.Maximum = ((CountingMotionDetector)activecamera.MotionDetector).MaxObjectsWidth/2;
                areaVisual.Height = HBar.Value; 
                areaVisual.Width = WBar.Value;
                areaVisual.Refresh();

                HBoxMax.Text = ((CountingMotionDetector)activecamera.MotionDetector).MaxObjectsHeight.ToString();
                HBarMax.Value = ((CountingMotionDetector)activecamera.MotionDetector).MaxObjectsHeight;
                HBarMax.Maximum = ((CountingMotionDetector)activecamera.MotionDetector).MaxObjectsHeight;
                WBoxMax.Text = ((CountingMotionDetector)activecamera.MotionDetector).MaxObjectsWidth.ToString();
                WBarMax.Value = ((CountingMotionDetector)activecamera.MotionDetector).MaxObjectsWidth;
                WBarMax.Maximum = ((CountingMotionDetector)activecamera.MotionDetector).MaxObjectsWidth+75;
                areaVisualMax.Height = HBarMax.Value;
                areaVisualMax.Width = WBarMax.Value;
                areaVisualMax.Refresh();
            
                ALBox.Text = activecamera.getAlarmLevel().ToString();
                ALBar.Value = (int)(activecamera.getAlarmLevel() * 100);
            }

        }

        private void ALBar_Scroll(object sender, EventArgs e)
        {
            ALBox.Text = ((double)ALBar.Value/100).ToString();
            if (activecamera != null && activecamera.MotionDetector != null)
            {                
                activecamera.setAlarmLevel(((double)ALBar.Value / 100));
            }
        }

        private void HBar_Scroll(object sender, EventArgs e)
        {
            areaVisual.Height = HBar.Value;
            areaVisual.Refresh();
            HBox.Text = HBar.Value.ToString();

            if (activecamera != null && activecamera.MotionDetector!=null)
            {
                ((CountingMotionDetector)activecamera.MotionDetector).MinObjectsHeight = HBar.Value;
            }
        }

        private void WBar_Scroll(object sender, EventArgs e)
        {
            areaVisual.Width = WBar.Value;
            areaVisual.Refresh();
            WBox.Text = WBar.Value.ToString();

            if (activecamera != null && activecamera.MotionDetector != null)
            {
                ((CountingMotionDetector)activecamera.MotionDetector).MinObjectsWidth = WBar.Value;
            }
        }

        private void HBarMax_Scroll(object sender, EventArgs e)
        {
            areaVisualMax.Height = HBarMax.Value;
            areaVisualMax.Refresh();
            HBoxMax.Text = HBarMax.Value.ToString();

            if (activecamera != null && activecamera.MotionDetector != null)
            {
                ((CountingMotionDetector)activecamera.MotionDetector).MaxObjectsHeight = HBarMax.Value;
            }

        }

        private void WBarMax_Scroll(object sender, EventArgs e)
        {
            areaVisualMax.Width = WBarMax.Value;
            areaVisualMax.Refresh();
            WBoxMax.Text = WBarMax.Value.ToString();

            if (activecamera != null && activecamera.MotionDetector != null)
            {
                ((CountingMotionDetector)activecamera.MotionDetector).MaxObjectsWidth = WBarMax.Value;
            }
        }
        
       
    }
}
