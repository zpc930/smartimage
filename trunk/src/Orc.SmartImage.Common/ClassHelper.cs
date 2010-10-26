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
                && rec.Bottom >= point.Y
                && rec.Left <= point.X
                && rec.Right >= point.X;
        }
    }
}
