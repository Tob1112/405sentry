using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using usb_api;

using AutonomousSentryGun.Objects;

namespace AutonomousSentryGun
{
  static class Program
  {
      public static usb_interface usbHub = new usb_interface();
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
       // test();
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }    
  }
}
