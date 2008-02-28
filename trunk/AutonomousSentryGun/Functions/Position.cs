using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutonomousSentryGun.Functions
{
  class Position
  {
    private int x, y;
    private double hAngle, vAngle;

    public Position()
    {
      this.x = ServoController.MAX_INTEGER_INPUT / 2;
      this.y = ServoController.MAX_INTEGER_INPUT / 2;
      this.hAngle = this.CoordinateToAngle(this.x);
      this.vAngle = this.CoordinateToAngle(this.y);
    }
    public Position(int x, int y)
    {
      this.x = x;
      this.y = y;
      this.hAngle = this.CoordinateToAngle(this.x);
      this.vAngle = this.CoordinateToAngle(this.y);
    }

    public Position(double hAngle, double vAngle)
    {
      this.hAngle = hAngle;
      this.vAngle = vAngle;
      this.x = this.AngleToCoordinate(this.hAngle);
      this.y = this.AngleToCoordinate(this.vAngle);
    }
    private int AngleToCoordinate(double angle)
    {
      return (int)Math.Round((double)angle/ServoController.getFactor());
    }
    private double CoordinateToAngle(int coordinate)
    {
      return Math.Round(coordinate * ServoController.getFactor());
    }
  }
}
