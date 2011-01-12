using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Orc.SmartImage
{
    public static class ClassHelper
    {
        public static int Area(this Rectangle rec)
        {
            return rec.Width * rec.Height;
        }

        public static Boolean IsContain(this Rectangle rec, Rectangle other)
        {
            return rec.Top <= other.Top
                && rec.Bottom >= other.Bottom
                && rec.Left <= other.Left
                && rec.Right >= other.Right;
        }

        public static Boolean IsContain(this Rectangle rec, Point point)
        {
            return rec.Top <= point.Y
                && rec.Bottom > point.Y
                && rec.Left <= point.X
                && rec.Right > point.X;
        }

        public static Point Right(this Point p)
        {
            return new Point(p.X+1, p.Y);
        }

        public static Point Left(this Point p)
        {
            return new Point(p.X - 1, p.Y);
        }

        public static Point Upper(this Point p)
        {
            return new Point(p.X, p.Y-1);
        }

        public static Point Lower(this Point p)
        {
            return new Point(p.X, p.Y+1);
        }

        public static Point UpperRight(this Point p)
        {
            return new Point(p.X + 1, p.Y-1);
        }

        public static Point UpperLeft(this Point p)
        {
            return new Point(p.X - 1, p.Y-1);
        }

        public static Point LowerRight(this Point p)
        {
            return new Point(p.X + 1, p.Y+1);
        }

        public static Point LowerLeft(this Point p)
        {
            return new Point(p.X - 1, p.Y+1);
        }

        public static Point Move(this Point p, Point shift)
        {
            return new Point(p.X + shift.X, p.Y + shift.Y);
        }

        public static Point Move(this Point p, int xShift, int yShift)
        {
            return new Point(p.X + xShift, p.Y + yShift);
        }

        public static int GetHashCode32(this Point p)
        {
            return p.Y * Int16.MaxValue + p.X;
        }

        public static PolarPointD ToPolarPointD(this Point p)
        {
            double angle = Math.Atan2(p.Y, p.X) * (180 / Math.PI);
            if (angle < 0) angle = 360 + angle;
            double radius = Math.Sqrt(p.X * p.X + p.Y * p.Y);
            return new PolarPointD(radius, angle);
        }

        public static PolarPoint ToPolarPoint(this Point p)
        {
            double angle = Math.Atan2(p.Y, p.X) * (180 / Math.PI);
            if (angle < 0) angle = 360 + angle;
            double radius = Math.Sqrt(p.X * p.X + p.Y * p.Y);
            return new PolarPoint((int)radius, (int)angle);
        }
    }
}
