using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutonomousSentryGun.Forms.Setup
{
  public partial class DataTransmission : Form
  {
    Timer trackingTimer;
    public DataTransmission(Timer ttu)
    {        
        InitializeComponent();
        trackingTimer = ttu;
        textBox1.Text = trackingTimer.Interval.ToString();
    }

    private void SetIntervalButton_Click(object sender, EventArgs e)
    {
        trackingTimer.Interval = Convert.ToInt32(textBox1.Text);
        this.Close();
    }
  }
}
