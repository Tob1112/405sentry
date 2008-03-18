using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using AutonomousSentryGun.Objects;

namespace AutonomousSentryGun
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        test();
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }
    static void test()
    {
      Packet packet = new Packet();
      packet.setXPosition(0xAAAA);
    }
  }
}
