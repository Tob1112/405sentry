using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AutonomousSentryGun.Functions
{
  class Servos
  {
    private readonly Size SHOOTING_RANGE_SIZE = new Size(250,250);
    private readonly Point CENTER_POSITION = new Point(1600,1477);
    private readonly Rectangle SHOOTING_RANGE;
    private Point position;
    public Rectangle ShootingRange
    {
      get
      {
        return SHOOTING_RANGE;
      }
    }
    public Point Position
    {
      get
      {
        return position;
      }
      set
      {
        position.X = getValidXCoordinate(value.X);
        position.Y = getValidYCoordinate(value.Y);

      }
    }
    public Point PositionToServosController
    {
      get
      {
        int invertedX = CENTER_POSITION.X - (position.X - CENTER_POSITION.X);
        return new Point(invertedX, position.Y);
      }
    }

    public Servos()
    {
      SHOOTING_RANGE = new Rectangle(getUpperLeftPosition(SHOOTING_RANGE_SIZE, CENTER_POSITION), SHOOTING_RANGE_SIZE);
      Position = new Point(CENTER_POSITION.X, CENTER_POSITION.Y);
    }

    public Servos(int xCenterPosition, int yCenterPosition)
    {
      CENTER_POSITION = new Point(xCenterPosition, yCenterPosition);
      SHOOTING_RANGE = new Rectangle(getUpperLeftPosition(SHOOTING_RANGE_SIZE, CENTER_POSITION), SHOOTING_RANGE_SIZE);
      Position = new Point(CENTER_POSITION.X, CENTER_POSITION.Y);
      
    }
   
    public Servos(int xCenterPosition, int yCenterPosition,int xLengthFromCenter, int yLengthFromCenter)
    {
      CENTER_POSITION = new Point(xCenterPosition, yCenterPosition);
      SHOOTING_RANGE_SIZE = new Size(xLengthFromCenter, yLengthFromCenter);
      SHOOTING_RANGE = new Rectangle(getUpperLeftPosition(SHOOTING_RANGE_SIZE, CENTER_POSITION), SHOOTING_RANGE_SIZE);
      Position = new Point(CENTER_POSITION.X, CENTER_POSITION.Y);
    }

    private int getValidXCoordinate(int x)
    {
      if (x > SHOOTING_RANGE.Right)
        return SHOOTING_RANGE.Right;
      if (x < SHOOTING_RANGE.Left)
        return SHOOTING_RANGE.Left;
      return x;
    }
    private int getValidYCoordinate(int y)
    {
      if (y < SHOOTING_RANGE.Top)
        return SHOOTING_RANGE.Top;
      if (y > SHOOTING_RANGE.Bottom)
        return SHOOTING_RANGE.Bottom;
      return y;
    }
    public void setPorportionalPosition(Rectangle grid, Point position)
    {
      this.Position = new Point(Convert.ToInt32(Math.Round((double)(position.X - grid.Left) / grid.Width * SHOOTING_RANGE.Width + SHOOTING_RANGE.Left, 0)),Convert.ToInt32(Math.Round((double)(position.Y - grid.Top) / grid.Height * SHOOTING_RANGE.Height + SHOOTING_RANGE.Top, 0)));
    }
    public Point getPorportionalPosition(Rectangle grid)
    {
      Double pointX = (double)(position.X - SHOOTING_RANGE.Left) / SHOOTING_RANGE.Width * grid.Width + grid.Left;
      Double pointY = (double)(position.Y - SHOOTING_RANGE.Top) / SHOOTING_RANGE.Height * grid.Height + grid.Top;
      return new Point(Convert.ToInt32(Math.Round(pointX,0)),Convert.ToInt32(Math.Round(pointY,0)));
    }
    public Point getUpperLeftPosition(Size size, Point centerPosition)
    {
      return new Point(centerPosition.X-size.Width/2,centerPosition.Y-size.Height/2);
    }
    public Point ConvertPositionMathToProgram(Point p)
    {
      return new Point(CENTER_POSITION.X+p.X,CENTER_POSITION.Y-p.Y);
    }
    public Point ConvertPositionProgramToMath()
    {
      return new Point(position.X - CENTER_POSITION.X, CENTER_POSITION.Y-position.Y);
    }
    public Point getCenterPosition()
    {
        return CENTER_POSITION;
    }
  }
}
