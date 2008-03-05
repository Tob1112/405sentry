﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutonomousSentryGun.Functions
{
  class ServoController
  {
    public readonly static int MAX_INTEGER_INPUT = 255;
    public readonly static double MAX_ANGLE = 90;
    public static double getFactor() {
      return ((double)(ServoController.MAX_ANGLE))/(ServoController.MAX_INTEGER_INPUT);
    }
  }
}