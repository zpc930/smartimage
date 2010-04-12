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

        public Byte ToGray()
        {
            return (Byte)(0.299 * Red + 0.587 * Green + 0.114 * Blue);
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
        public unsafe Rgb24* Start { get { return (Rgb24*)this.StartIntPtr; } }

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

        public override IImage Clone()
        {
            ImageRgb24 img = new ImageRgb24(this.Width, this.Height);
            img.CloneFrom(this);
            return img;
        }
    }
}
