using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AutonomousSentryGun.Functions
{
  class Servos
  {
      //170,230
    private readonly Size MAX_SHOOTING_RANGE_SIZE = new Size(170,230);
    private readonly Point CENTER_POSITION = new Point(1630,1477);
    private Rectangle shootingRange;
    private Point position;
    public Rectangle ShootingRange
    {
      get
      {
        return shootingRange;
      }
    }

    public Point CenterServosPosition
    {
      get
      {
        return CENTER_POSITION;
      }
    }
    public Point ServosPosition
    {
      get
      {
        int invertedX = CENTER_POSITION.X - (position.X - CENTER_POSITION.X);
        return new Point(invertedX, position.Y);
      }
      set
      {
        position.X = GetValidXCoordinate(value.X);
        position.Y = GetValidYCoordinate(value.Y);

      }
    }

    public Servos()
    {
      shootingRange = new Rectangle(GetUpperLeftPosition(MAX_SHOOTING_RANGE_SIZE, CENTER_POSITION), MAX_SHOOTING_RANGE_SIZE);
      ServosPosition = new Point(CENTER_POSITION.X, CENTER_POSITION.Y);
    }

    public Servos(int xCenterPosition, int yCenterPosition)
    {
      CENTER_POSITION = new Point(xCenterPosition, yCenterPosition);
      shootingRange = new Rectangle(GetUpperLeftPosition(MAX_SHOOTING_RANGE_SIZE, CENTER_POSITION), MAX_SHOOTING_RANGE_SIZE);
      ServosPosition = new Point(CENTER_POSITION.X, CENTER_POSITION.Y);
      
    }
   
    public Servos(int xCenterPosition, int yCenterPosition,int xLengthFromCenter, int yLengthFromCenter)
    {
      CENTER_POSITION = new Point(xCenterPosition, yCenterPosition);
      MAX_SHOOTING_RANGE_SIZE = new Size(xLengthFromCenter, yLengthFromCenter);
      shootingRange = new Rectangle(GetUpperLeftPosition(MAX_SHOOTING_RANGE_SIZE, CENTER_POSITION), MAX_SHOOTING_RANGE_SIZE);
      ServosPosition = new Point(CENTER_POSITION.X, CENTER_POSITION.Y);
    }

    private int GetValidXCoordinate(int x)
    {
      if (x > shootingRange.Right)
        return shootingRange.Right;
      if (x < shootingRange.Left)
        return shootingRange.Left;
      return x;
    }
    private int GetValidYCoordinate(int y)
    {
      if (y < shootingRange.Top)
        return shootingRange.Top;
      if (y > shootingRange.Bottom)
        return shootingRange.Bottom;
      return y;
    }
    public void SetPorportionalMathPosition(Rectangle grid, Point position)
    {
      this.ServosPosition = new Point(Convert.ToInt32(Math.Round((double)(position.X - grid.Left) / grid.Width * shootingRange.Width + shootingRange.Left, 0)),Convert.ToInt32(Math.Round((double)(position.Y - grid.Top) / grid.Height * shootingRange.Height + shootingRange.Top, 0)));
    }
    public Point GetPorportionalMathPosition(Rectangle grid)
    {
      Double pointX = (double)(position.X - shootingRange.Left) / shootingRange.Width * grid.Width + grid.Left;
      Double pointY = (double)(position.Y - shootingRange.Top) / shootingRange.Height * grid.Height + grid.Top;
      return new Point(Convert.ToInt32(Math.Round(pointX,0)),Convert.ToInt32(Math.Round(pointY,0)));
    }
    private Point GetUpperLeftPosition(Size size, Point centerPosition)
    {
      return new Point(centerPosition.X-size.Width/2,centerPosition.Y-size.Height/2);
    }
    public Point ConvertPositionMathToServos(Point p)
    {
      return new Point(CENTER_POSITION.X+p.X,CENTER_POSITION.Y-p.Y);
    }
    public Point GetMathPosition()
    {
      return new Point(position.X - CENTER_POSITION.X, CENTER_POSITION.Y-position.Y);
    }
    public void SetShootingRangeSize(int width, int height)
    {
      Size newSize = new Size();
      newSize.Width = width < MAX_SHOOTING_RANGE_SIZE.Width ?
        width : MAX_SHOOTING_RANGE_SIZE.Width;
      newSize.Height = height < MAX_SHOOTING_RANGE_SIZE.Height ?
        height : MAX_SHOOTING_RANGE_SIZE.Height;
      shootingRange = new Rectangle(GetUpperLeftPosition(newSize, CENTER_POSITION), newSize);
      position.X = GetValidXCoordinate(position.X);
      position.Y = GetValidYCoordinate(position.Y);
    }
  }
}
