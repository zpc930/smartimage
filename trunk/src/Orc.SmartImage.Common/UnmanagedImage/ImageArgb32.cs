using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing.Imaging;

namespace Orc.SmartImage
{
    [StructLayout(LayoutKind.Explicit)]
    public partial struct Argb32
    {
        [FieldOffset(0)]
        public Byte Blue;
        [FieldOffset(1)]
        public Byte Green;
        [FieldOffset(2)]
        public Byte Red;
        [FieldOffset(3)]
        public Byte Alpha;

        //[FieldOffset(0)]
        //public Byte Alpha;
        //[FieldOffset(1)]
        //public Byte Red;
        //[FieldOffset(2)]
        //public Byte Green;
        //[FieldOffset(3)]
        //public Byte Blue;

        public Argb32(int red, int green, int blue, int alpha = 255)
        {
            Red = (byte)red;
            Green = (byte)green;
            Blue = (byte)blue;
            Alpha = (byte)alpha;
        }

        public Argb32(byte red, byte green, byte blue, byte alpha = 255)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        public Byte ToGray()
        {
            return (Byte)(0.299 * Red + 0.587 * Green + 0.114 * Blue);
        }

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

    public partial class ImageArgb32 : UnmanagedImage<Argb32>
    {
        public unsafe ImageArgb32(Int32 width, Int32 height)
            : base(width, height)
        {
        }

        public ImageArgb32(Bitmap map):base(map)
        {
        }

        protected override IByteConverter<Argb32> CreateByteConverter()
        {
            return new Argb32Converter();
        }

        public ImageU8 ToGrayscaleImage()
        {
            return ToGrayscaleImage(0.299, 0.587, 0.114);
        }

        public unsafe ImageU8 ToGrayscaleImage(double rCoeff, double gCoeff, double bCoeff)
        {
            ImageU8 img = new ImageU8(this.Width, this.Height);
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

        public override IImage Clone()
        {
            ImageArgb32 img = new ImageArgb32(this.Width, this.Height);
            img.CloneFrom(this);
            return img;
        }
    }
}
