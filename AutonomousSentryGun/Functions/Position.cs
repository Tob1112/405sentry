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
        hAngle = CoordinateToHAngle(value);
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
        vAngle = CoordinateToVAngle(value);
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
        x = HAngleToCoordinate(value);
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
        y = VAngleToCoordinate(value);
      }
    }
    
    public Position()
    {
      this.x = (ServoController.MAX_INTEGER_INPUT - ServoController.MIN_INTEGER_INPUT) / 2 + ServoController.MIN_INTEGER_INPUT;
      this.y = (ServoController.MAX_INTEGER_INPUT - ServoController.MIN_INTEGER_INPUT) / 2 + ServoController.MIN_INTEGER_INPUT;
      this.hAngle = this.CoordinateToHAngle(this.x);
      this.vAngle = this.CoordinateToVAngle(this.y);
    }
    public Position(int x, int y)
    {
      this.x = x;
      this.y = y;
      this.hAngle = this.CoordinateToHAngle(this.x);
      this.vAngle = this.CoordinateToVAngle(this.y);
    }

    public Position(double hAngle, double vAngle)
    {
      this.hAngle = hAngle;
      this.vAngle = vAngle;
      this.x = this.HAngleToCoordinate(this.hAngle);
      this.y = this.VAngleToCoordinate(this.vAngle);
    }
    private int HAngleToCoordinate(double angle)
    {
      return (int)Math.Round((double)angle / ServoController.getXFactor()) + ServoController.MIN_INTEGER_INPUT;
    }
    private double CoordinateToHAngle(int coordinate)
    {
      double factor = ServoController.getXFactor();
      return Math.Round((coordinate - ServoController.MIN_INTEGER_INPUT) * ServoController.getXFactor()) + ServoController.MIN_H_ANGLE;
    }
    private int VAngleToCoordinate(double angle)
    {
      return (int)Math.Round((double)angle / ServoController.getYFactor()) + ServoController.MIN_INTEGER_INPUT;
    }
    private double CoordinateToVAngle(int coordinate)
    {
      double factor = ServoController.getYFactor();
      return Math.Round((coordinate-ServoController.MIN_INTEGER_INPUT) * ServoController.getYFactor())+ServoController.MIN_V_ANGLE;
    }
  }
}
