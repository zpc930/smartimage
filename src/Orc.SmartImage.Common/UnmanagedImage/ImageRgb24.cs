using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace Orc.SmartImage
{
    [StructLayout(LayoutKind.Explicit)]
    public partial struct Rgb24
    {
        public static Rgb24 WHITE = new Rgb24 { Red = 255, Green = 255, Blue = 255 };
        public static Rgb24 BLACK = new Rgb24 ();
        public static Rgb24 RED = new Rgb24 { Red = 255 };
        public static Rgb24 BLUE = new Rgb24 { Blue = 255 };
        public static Rgb24 GREEN = new Rgb24 { Green = 255 };

        public Rgb24(int red, int green, int blue)
        {
            Red = (byte)red;
            Green = (byte)green;
            Blue = (byte)blue;
        }

        public Rgb24(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        [FieldOffset(0)]
        public Byte Blue;
        [FieldOffset(1)]
        public Byte Green;
        [FieldOffset(2)]
        public Byte Red;

        public override string ToString()
        {
            return "Rgb24 [R=" + Red.ToString() + ", G=" + Green.ToString() + ", B=" + Blue.ToString() + "]";
        }

        public void AdjustSaturation()
        {
            if (this.Red == this.Green && this.Red == this.Blue) return;

            int max = Math.Max(Math.Max(this.Red, this.Blue), this.Green);
            int min = Math.Min(Math.Min(this.Red, this.Blue), this.Green);
            float coeff = (float)max / (max - min);
            int red = (int)((this.Red - min) * coeff);
            int green = (int)((this.Green - min) * coeff);
            int blue = (int)((this.Blue - min) * coeff);
            if (red > 255) red = 255;
            if (green > 255) green = 255;
            if (blue > 255) blue = 255;
            this.Red = (byte)red;
            this.Green = (byte)green;
            this.Blue = (byte)blue;
        }

        public Byte ToGray()
        {
            return (Byte)(0.299 * Red + 0.587 * Green + 0.114 * Blue);
        }

        public CieXyz ToCieXyz()
        {
            double rLinear = Red / 255.0;
            double gLinear = Green / 255.0;
            double bLinear = Blue / 255.0;

            double r = (rLinear > 0.04045) ? Math.Pow((rLinear + 0.055) / (1 + 0.055), 2.2) : (rLinear / 12.92);
            double g = (gLinear > 0.04045) ? Math.Pow((gLinear + 0.055) / (1 + 0.055), 2.2) : (gLinear / 12.92);
            double b = (bLinear > 0.04045) ? Math.Pow((bLinear + 0.055) / (1 + 0.055), 2.2) : (bLinear / 12.92);

            return new CieXyz { X = r * 0.4124 + g * 0.3576 + b * 0.1805, Y = r * 0.2126 + g * 0.7152 + b * 0.0722, Z = r * 0.0193 + g * 0.1192 + b * 0.9505 };
        }

        public CieLab ToCieLab()
        {
            return ToCieXyz().ToCieLab();
        }

        public double GetCirLabDistance(Rgb24 other)
        {
            return ToCieLab().GetDistance(other.ToCieLab());
        }

        public double GetCirLabDistanceSquare(Rgb24 other)
        {
            return ToCieLab().GetDistanceSquare(other.ToCieLab());
        }

        public double GetDistance(Rgb24 other)
        {
            int deltaR = this.Red - other.Red;
            int deltaG = this.Green - other.Green;
            int deltaB = this.Blue - other.Blue;
            int distance = deltaR * deltaR + deltaG * deltaG + deltaB * deltaB;
            return Math.Sqrt(distance);
        }

        public int GetDistanceSquare(Rgb24 other)
        {
            int deltaR = this.Red - other.Red;
            int deltaG = this.Green - other.Green;
            int deltaB = this.Blue - other.Blue;
            return deltaR * deltaR + deltaG * deltaG + deltaB * deltaB;
        }

        public int GetVisualDistanceSquare(Rgb24 other, int redCoeff = 1, int greenCoeff = 2, int blueCoeff = 1)
        {
            int deltaR = this.Red - other.Red;
            int deltaG = this.Green - other.Green;
            int deltaB = this.Blue - other.Blue;
            return redCoeff * deltaR * deltaR + greenCoeff * deltaG * deltaG + blueCoeff * deltaB * deltaB;
        }

        public override int GetHashCode()
        {
            return Red * 65536 + Green * 256 + Blue;
        }
    }

    public struct Rgb24Converter : IByteConverter<Rgb24>
    {
        public unsafe void Copy(byte* from, Argb32* to)
        {
            to->Blue = from[0];
            to->Green = from[1];
            to->Red = from[2];
            to->Alpha = 255;
        }

        public unsafe void Copy(Argb32* from, byte* to)
        {
            to[0] = from->Blue;
            to[1] = from->Green;
            to[2] = from->Red;
        }

        public unsafe void Copy(Rgb24* from, byte* to)
        {
            to[0] = from->Blue;
            to[1] = from->Green;
            to[2] = from->Red;
        }


        public unsafe void Copy(byte* from, ref Rgb24 to)
        {
            to.Blue = from[0];
            to.Green = from[1];
            to.Blue = from[2];
        }

        public unsafe void Copy(ref Rgb24 from, byte* to)
        {
            to[0] = from.Blue;
            to[1] = from.Green;
            to[2] = from.Red;
        }
    }

    public partial class ImageRgb24 : UnmanagedImage<Rgb24>
    {
        public unsafe ImageRgb24(Int32 width, Int32 height)
            : base(width, height)
        {
        }

        public ImageRgb24(Bitmap map)
            : base(map)
        {
        }

        protected override IByteConverter<Rgb24> CreateByteConverter()
        {
            return new Rgb24Converter();
        }

        public ImageU8 ToGrayscaleImage()
        {
            return ToGrayscaleImage(0.299, 0.587, 0.114);
        }

        public ImageInt32 ToGrayscaleImageInt32()
        {
            return ToGrayscaleImageInt32(0.299, 0.587, 0.114);
        }

        public unsafe ImageU8 ToGrayscaleImage(double rCoeff, double gCoeff, double bCoeff)
        {
            ImageU8 img = new ImageU8(this.Width, this.Height);
            Rgb24* p = Start;
            Byte* to = img.Start;
            Rgb24* end = p + Length;

            if (Length < 1024)
            {
                while (p != end)
                {
                    *to = (Byte)(p->Red * rCoeff + p->Green * gCoeff + p->Blue * bCoeff);
                    p++;
                    to++;
                }
            }
            else
            {
                int* bCache = stackalloc int[256];
                int* gCache = stackalloc int[256];
                int* rCache = stackalloc int[256];

                const int shift = 1<<10;
                int rShift = (int)(rCoeff * shift);
                int gShift = (int)(gCoeff * shift);
                int bShift = shift - rShift - gShift;

                int r = 0, g = 0, b = 0;
                for (int i = 0; i < 256; i++)
                {
                    bCache[i] = b;
                    gCache[i] = g;
                    rCache[i] = r;
                    b += bShift;
                    g += gShift;
                    r += rShift;
                }

                while (p != end)
                {
                    *to = (Byte)(( bCache[p->Red] + gCache[p->Green] + rCache[p->Red] ) >> 10);
                    p++;
                    to++;
                }
            }
            return img;
        }

        public unsafe ImageInt32 ToGrayscaleImageInt32(double rCoeff, double gCoeff, double bCoeff)
        {
            ImageInt32 img = new ImageInt32(this.Width, this.Height);
            Rgb24* p = Start;
            Int32* to = img.Start;
            Rgb24* end = p + Length;

            if (Length < 1024)
            {
                while (p != end)
                {
                    *to = (Byte)(p->Red * rCoeff + p->Green * gCoeff + p->Blue * bCoeff);
                    p++;
                    to++;
                }
            }
            else
            {
                int* bCache = stackalloc int[256];
                int* gCache = stackalloc int[256];
                int* rCache = stackalloc int[256];

                const int shift = 1 << 10;
                int rShift = (int)(rCoeff * shift);
                int gShift = (int)(gCoeff * shift);
                int bShift = shift - rShift - gShift;

                int r = 0, g = 0, b = 0;
                for (int i = 0; i < 256; i++)
                {
                    bCache[i] = b;
                    gCache[i] = g;
                    rCache[i] = r;
                    b += bShift;
                    g += gShift;
                    r += rShift;
                }

                while (p != end)
                {
                    *to = (Byte)((bCache[p->Red] + gCache[p->Green] + rCache[p->Red]) >> 10);
                    p++;
                    to++;
                }
            }
            return img;
        }

        public void DrawLine(PointF start, PointF end, Rgb24 color, int radius)
        {
            float deltaX = end.X - start.X;
            float deltaY = end.Y - start.Y;
            int ww = this.Width-1;
            int hh = this.Height-1;

            if (deltaX == 0)
            {
                if (deltaY == 0) return;

                int yStart = (int)(start.Y);
                int yEnd = (int)(end.Y);
                int x = (int)(start.X);

                if (yEnd < yStart)
                {
                    int tmp = yEnd;
                    yEnd = yStart;
                    yStart = tmp;
                }

                yStart = Math.Max(0, yStart);
                yEnd = Math.Min(ww, yEnd);

                for (int y = yStart; y <= yEnd; y++)
                {
                    SetColor(x, y, color, radius, ww, hh);
                }
            }
            else
            {
                int xStart = (int)start.X;
                int xEnd = (int)end.X;
                if (xEnd < xStart)
                {
                    int tmp = xEnd;
                    xEnd = xStart;
                    xStart = tmp;
                }

                for (int x = xStart; x <= xEnd; x++)
                {
                    float deltaXX = start.X - x;
                    float deltaYY = deltaY * (deltaXX / deltaX);
                    int y = (int)(Math.Round(start.Y - deltaYY));

                    SetColor(x, y, color, radius, ww, hh);
                }
            }
        }

        private void SetColor(int x, int y, Rgb24 color, int radius, int ww, int hh)
        {
            int xStart = x - radius;
            int xEnd = x + radius;
            int yStart = y - radius;
            int yEnd = y + radius;

            for (int yy = yStart; yy < yEnd; yy++)
            {
                for (int xx = xStart; xx < xEnd; xx++)
                {
                    if (xx < 0 || yy < 0 || xx > ww || yy > hh) break;
                    this[yy, xx] = color;
                }
            }
        }

        public override IImage Clone()
        {
            ImageRgb24 img = new ImageRgb24(this.Width, this.Height);
            img.CloneFrom(this);
            return img;
        }
    }
}
