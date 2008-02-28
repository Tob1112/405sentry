using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutonomousSentryGun.Forms.Test
{
  public partial class TransmitPosition : Form
  {
    private readonly static int CENTER_GRID_X = 139;
    private readonly static int CENTER_GRID_Y = 137;

    public TransmitPosition()
    {
      InitializeComponent();
      init();
    }
    void init()
    {
      redDot.Location = new Point(TransmitPosition.CENTER_GRID_X, TransmitPosition.CENTER_GRID_Y);
    //  XTextBox.Text = 
    }
  }
}
