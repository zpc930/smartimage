using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Drawing.Imaging;

namespace Orc.SmartImage
{
    public struct Argb32
    {
        public Byte Blue;
        public Byte Green;
        public Byte Red;
        public Byte Alpha;

        public override string ToString()
        {
            return "Argb32 [A="+ Alpha +", R=" + Red.ToString() + ", G=" + Green.ToString() + ", B=" + Blue.ToString() + "]";
        }
    }

    public struct Argb32Converter : IByteConverter<Argb32>
    {
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
            c->Blue = from->Blue;
            c->Green = from->Green;
            c->Red = from->Red;
            c->Alpha = 255;
        }

        public unsafe void Copy(byte* from, ref Argb32 to)
        {
            to.Blue = from[0];
            to.Green = from[1];
            to.Blue = from[2];
            to.Alpha = from[3];
        }

        public unsafe void Copy(ref Argb32 from, byte* to)
        {
            to[0] = from.Blue;
            to[1] = from.Green;
            to[2] = from.Red;
            to[3] = from.Alpha;
        }
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

        protected override IByteConverter<Argb32> CreateByteConverter()
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
    }
}
