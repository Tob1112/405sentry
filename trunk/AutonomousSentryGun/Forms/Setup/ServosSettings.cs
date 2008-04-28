using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutonomousSentryGun.Functions;

namespace AutonomousSentryGun.Forms.Setup
{
  public partial class ServosSettings : Form
  {
    private MainForm mainForm;
    public ServosSettings(MainForm mainForm)
    {
      this.mainForm = mainForm;      
      InitializeComponent();
      SetTextBoxValues();
    }

    private void SetTextBoxValues()
    {
      Servos mainServos = mainForm.MainServos;
      XCenterTextBox.Text = mainServos.CenterServosPosition.X.ToString();
      YCenterTextBox.Text = mainServos.CenterServosPosition.Y.ToString();
      XRangeTextBox.Text = mainServos.ShootingRange.Width.ToString();
      YRangeTextBox.Text = mainServos.ShootingRange.Height.ToString();
    }
    private void UpdateSettingsButton_Click(object sender, EventArgs e)
    {
      mainForm.MainServos = new Servos(Int32.Parse(XCenterTextBox.Text),Int32.Parse(YCenterTextBox.Text),Int32.Parse(XRangeTextBox.Text)/2,Int32.Parse(YRangeTextBox.Text)/2);
      this.Close();
    }
  }
}
