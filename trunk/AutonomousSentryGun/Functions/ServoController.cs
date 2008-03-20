using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutonomousSentryGun.Functions
{
  class ServoController
  {
    public const int MIN_X_INTEGER_INPUT = 1000;
    public const int MAX_X_INTEGER_INPUT = 2000;
    public const int MIN_Y_INTEGER_INPUT = 1200;
    public const int MAX_Y_INTEGER_INPUT = 1600;
    public const double MAX_H_ANGLE = 90;
    public const double MIN_H_ANGLE = -90;
    public const double MAX_V_ANGLE = 45;
    public const double MIN_V_ANGLE = -45;
    public static double getXFactor()
    {
      return ((double)(ServoController.MAX_H_ANGLE-ServoController.MIN_H_ANGLE)) / (ServoController.MAX_INTEGER_INPUT-ServoController.MIN_INTEGER_INPUT);
    }
    public static double getYFactor()
    {
      return ((double)(ServoController.MAX_V_ANGLE-ServoController.MIN_V_ANGLE)) / (ServoController.MAX_INTEGER_INPUT-ServoController.MIN_INTEGER_INPUT);
    }    
  }
}
