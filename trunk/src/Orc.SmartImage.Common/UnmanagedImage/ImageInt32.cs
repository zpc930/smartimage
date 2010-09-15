using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Orc.SmartImage
{
    public struct Int32Converter : IByteConverter<Int32>
    {
        public unsafe void Copy(byte* from, Argb32* to)
        {
            Int32 c = *((Int32*)from);
            byte data = c > 255 ? (byte)255 : c < 0 ? (byte)0 : (byte)c;
            to->Blue = data;
            to->Green = data;
            to->Red = data;
            to->Alpha = 255;
        }

        public unsafe void Copy(Argb32* from, byte* to)
        {
            Int32* p = (Int32*)to;
            *p = (Byte)(from->Blue * 0.114 + from->Green * 0.587 + from->Red * 0.299);
        }

        public unsafe void Copy(Rgb24* from, byte* to)
        {
            Int32* p = (Int32*)to;
            *p = (Byte)(from->Blue * 0.114 + from->Green * 0.587 + from->Red * 0.299);
        }

        public unsafe void Copy(byte* from, ref Int32 to)
        {
            Int32* p = (Int32*)from;
            to = *p;
        }

        public unsafe void Copy(ref Int32 from, byte* to)
        {
            Int32* p = (Int32*)to;
            *p = from;
        }
    }

    public partial class ImageInt32 : UnmanagedImage<Int32>
    {
        public unsafe ImageInt32(Int32 width, Int32 height)
            : base(width, height)
        {
        }

        public ImageInt32(Bitmap map)
            : base(map)
        {
        }

        protected override IByteConverter<Int32> CreateByteConverter()
        {
            return new Int32Converter();
        }

        public override IImage Clone()
        {
            ImageInt32 img = new ImageInt32(this.Width, this.Height);
            img.CloneFrom(this);
            return img;
        }

        public unsafe ImageU8 ToImageU8()
        {
            ImageU8 imgU8 = new ImageU8(this.Width, this.Height);
            int length = imgU8.Length;
            Int32* start = this.Start;
            Byte* dst = imgU8.Start;
            Int32* end = start + this.Length;
            while (start != end)
            {
                int val = *start;
                *dst = val < 0 ? (Byte)0 : (val > 255 ? (Byte)255 : (Byte)val);
                start++;
                dst++;
            }
            return imgU8;
        }
    }
}
