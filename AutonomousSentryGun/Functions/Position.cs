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

    public int X
    {
      get
      {
        return x;
      }
      set
      {
        x = value;
        hAngle = CoordinateToAngle(value);
      }
    }

    public int Y
    {
      get
      {
        return y;
      }
      set
      {
        y = value;
        vAngle = CoordinateToAngle(value);
      }
    }

    public double HAngle
    {
      get
      {
        return hAngle;
      }
      set
      {
        hAngle = value;
        x = AngleToCoordinate(value);
      }
    }
    
    public double VAngle
    {
      get
      {
        return vAngle;
      }
      set
      {
        vAngle = value;
        y = AngleToCoordinate(value);
      }
    }
    
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
      return (int)Math.Round((double)angle / ServoController.getFactor());
    }
    private double CoordinateToAngle(int coordinate)
    {
      double factor = ServoController.getFactor();
      return Math.Round(coordinate * ServoController.getFactor());
    }
  }
}
