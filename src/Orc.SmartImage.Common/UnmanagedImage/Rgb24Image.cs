using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Orc.SmartImage
{
    public struct Rgb24
    {
        public Byte Blue;
        public Byte Green;
        public Byte Red;

        public override string ToString()
        {
            return "Rgb24 [R=" + Red.ToString() + ", G=" + Green.ToString() + ", B=" + Blue.ToString() + "]";
        }
    }

    public struct Rgb24Converter : IColorConverter
    {
        #region IColorConvert Members

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

        #endregion
    }

    public class Rgb24Image : UnmanagedImage<Rgb24>
    {
        public unsafe Rgb24* Start { get { return (Rgb24*)this.StartIntPtr; } }

        public unsafe Rgb24Image(Int32 width, Int32 height)
            : base(width, height)
        {
        }

        public Rgb24Image(Bitmap map)
            : base(map)
        {
        }

        public unsafe Rgb24 this[int index]
        {
            get { return *(Start + index); }
            set { *(Start + index) = value; }
        }

        public unsafe Rgb24 this[int row, int col]
        {
            get { return *(Start + row * Width + col); }
            set { *(Start + row * Width + col) = value; }
        }

        protected override IColorConverter GetColorConvert()
        {
            return new Rgb24Converter();
        }

        public GrayscaleImage ToGrayscaleImage()
        {
            return ToGrayscaleImage(0.299, 0.587, 0.114);
        }

        public unsafe GrayscaleImage ToGrayscaleImage(double rCoeff, double gCoeff, double bCoeff)
        {
            GrayscaleImage img = new GrayscaleImage(this.Width, this.Height);
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
                int* rCache = stackalloc int[256];
                int* gCache = stackalloc int[256];
                int* bCache = stackalloc int[256];

                const int shift = 1<<10;
                int rShift = (int)(rCoeff * shift);
                int gShift = (int)(gCoeff * shift);
                int bShift = shift - rShift - gShift;

                int r = 0, g = 0, b = 0;
                for (int i = 0; i < 256; i++)
                {
                    rCache[i] = r;
                    gCache[i] = g;
                    bCache[i] = b;
                    r += rShift;
                    g += gShift;
                    b += bShift;
                }

                while (p != end)
                {
                    *to = (Byte)((rCache[p->Red] + gCache[p->Green] + bCache[p->Red]) >> 10);
                    p++;
                    to++;
                }
            }
            return img;
        }
    }
}
