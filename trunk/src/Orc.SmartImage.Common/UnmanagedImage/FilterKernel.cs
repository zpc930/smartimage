using System;
using System.Collections.Generic;
using System.Text;

namespace Orc.SmartImage
{
    public class FilterKernel<T>
    {
        public Int32 Width { get; private set; }
        public Int32 Height { get; private set; }
        public Int32 Length { get { return Width * Height; } }

        public T[] Data { get; private set; }
        public Int32 Scale { get; private set; }

        public FilterKernel(T[] data, Int32 width, Int32 height, Int32 scale)
        {
            this.Width = width;
            this.Height = height;
            this.Scale = scale;
            this.Data = data;
        }
    }

    public struct T
    {
        public unsafe T* t;
        public unsafe T** t1;
    }
}
