using System;
using System.Collections.Generic;
using System.Text;

namespace Orc.SmartImage
{
    public partial struct CieXyz
    {
        public static readonly CieXyz D65 = new CieXyz { X = 0.9505, Y = 1.0, Z = 1.0890 };

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public CieLab ToCieLab()
        {
            double l = 116.0 * Transform(Y / D65.Y) - 16;
            double a = 500.0 * (Transform(X / D65.X) - Transform(Y / D65.Y));
            double b = 200.0 * (Transform(Y / D65.Y) - Transform(Z / D65.Z));
            return new CieLab { L = l, A = a, B = b };
        }

        private static double Transform(double t)
        {
            return ((t > 0.008856) ? Math.Pow(t, (1.0 / 3.0)) : (7.787 * t + 16.0 / 116.0));
        }
    }
}
