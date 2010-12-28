using System;
using System.Collections.Generic;
using System.Text;

namespace Orc.SmartImage
{
    using Orc.Util;

    public class ColorCollection<TPixel> : List<TPixel> where TPixel : struct, IMetriable<TPixel>
    {
        public Nullable<TPixel> FindNearest(TPixel color)
        {
            int index = FindNearestIndex(color);
            if (index >= 0)
                return this[index];
            else
                return null;
        }

        public int FindNearestIndex(TPixel color)
        {
            if (Count == 0) return -1;
            int index = 0;
            double min = this[0].GetDistanceSquare(color);
            for (int i = 1; i < this.Count; i++)
            {
                TPixel p = this[i];

                double distanceSquare = p.GetDistanceSquare(color);
                if (distanceSquare < min)
                {
                    index = i;
                    min = distanceSquare;
                }
            }
            return index;
        }
    }

    public class Rgb24Collection : ColorCollection<Rgb24>
    {
        public Nullable<Rgb24> FindVisualNearest(Rgb24 color)
        {
            int index = FindVisualNearestIndex(color);
            if (index >= 0)
                return this[index];
            else
                return null;
        }

        public int FindVisualNearestIndex(Rgb24 color)
        {
            if (Count == 0) return -1;
            int index = 0;
            double min = this[0].GetVisualDistanceSquare(color);
            for (int i = 1; i < this.Count; i++)
            {
                double distanceSquare = this[i].GetVisualDistanceSquare(color);
                if (distanceSquare < min)
                {
                    index = i;
                    min = distanceSquare;
                }
            }
            return index;
        }
    }
}
