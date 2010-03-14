using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Drawing.Imaging;

namespace Orc.SmartImage
{
    public struct Argb32
    {
        public Byte Alpha;
        public Byte Red;
        public Byte Green;
        public Byte Blue;
    }

    public struct Argb32Converter : IColorConverter
    {
        #region IColorConvert Members

        public unsafe void Copy(byte* from, Argb32* to)
        {
            *to = *((Argb32*)from);
        }

        public unsafe void Copy(Argb32* from, byte* to)
        {
            *((Argb32*)to) = *from;
        }

        public unsafe void Copy(Rgb24* from, byte* to)
        {
            Argb32* c = (Argb32*)to;
            c->Alpha = 255;
            c->Red = from->Red;
            c->Green = from->Green;
            c->Blue = from->Blue;
        }

        #endregion
    }

    public class Argb32Image : UnmanagedImage<Argb32>
    {
        public unsafe Argb32* Start { get { return (Argb32*)this.StartIntPtr; } }

        public unsafe Argb32Image(Int32 width, Int32 height)
            : base(width, height)
        {
        }

        public Argb32Image(Bitmap map):base(map)
        {
        }

        public unsafe Argb32 this[int index]
        {
            get { return *(Start + index); }
            set { *(Start + index) = value; }
        }

        public unsafe Argb32 this[int row, int col]
        {
            get { return *(Start + row * Width + col); }
            set { *(Start + row * Width + col) = value; }
        }

        protected override IColorConverter GetColorConvert()
        {
            return new Argb32Converter();
        }


        public GrayscaleImage ToGrayscaleImage()
        {
            return ToGrayscaleImage(0.299, 0.587, 0.114);
        }

        public unsafe GrayscaleImage ToGrayscaleImage(double rCoeff, double gCoeff, double bCoeff)
        {
            GrayscaleImage img = new GrayscaleImage(this.Width, this.Height);
            Argb32* p = Start;
            Byte* to = img.Start;
            Argb32* end = p + Length;

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

                const int shift = 1 << 10;
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
