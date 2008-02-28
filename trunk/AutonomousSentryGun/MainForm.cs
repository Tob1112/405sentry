using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AutonomousSentryGun.Forms.Test;

namespace AutonomousSentryGun
{
  public partial class MainForm : Form
  {
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
  }
}
