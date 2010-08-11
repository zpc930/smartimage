using TPixel = System.Byte;
using TCache = System.Int32;
using TKernel = System.Int32;

using System;
using System.Collections.Generic;
using System.Text;

namespace Orc.SmartImage.Hidden
{
    public abstract class Image_Template : UnmanagedImage<TPixel>
    {
        private Image_Template()
            : base(1,1)
        {
            throw new NotImplementedException();
        }

        #region mixin

        public unsafe TPixel* Start { get { return (TPixel*)this.StartIntPtr; } }

        public unsafe TPixel this[int index]
        {
            get
            {
                return Start[index];
            }
            set
            {
                Start[index] = value;
            }
        }

        public unsafe TPixel this[int row, int col]
        {
            get
            {
                return Start[row * this.Width + col];
            }
            set
            {
                Start[row * this.Width + col] = value;
            }
        }

        public unsafe TPixel* Row(Int32 row)
        {
            if (row < 0 || row >= this.Height) throw new ArgumentOutOfRangeException("row");
            return Start + row * this.Width;
        }

        public unsafe void Fill(TPixel pixel)
        {
            TPixel* p = this.Start;
            TPixel* end = p + this.Length;
            while (p != end)
            {
                *p = pixel;
                p++;
            }
        }

        #endregion
    }
}
