using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutonomousSentryGun.Functions
{
  class ServoController
  {
    public const int MAX_INTEGER_INPUT = 255;
    public const double MAX_ANGLE = 90;
    public static double getFactor() {
      return ((double)(ServoController.MAX_ANGLE))/(ServoController.MAX_INTEGER_INPUT);
    }
    public static void sendPosition(Position position)
    {
    }
    
  }
}
