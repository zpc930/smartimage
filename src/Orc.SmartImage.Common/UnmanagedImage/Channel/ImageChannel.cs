using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Orc.SmartImage
{
    public class ImageChannel<T> : IDisposable where T : struct
    {
        public Int32 Length { get; private set; }
        public Int32 PixelSize { get; private set; }
        public Int32 Width { get; private set; }
        public Int32 Height { get; private set; }
        public Int32 SizeOfType { get; private set; }
        public IntPtr StartIntPtr { get; private set; }
        public Int32 Step { get { return PixelSize / SizeOfType; } }
        public Boolean IsMemoryManager { get; private set; }

        private static Int32 SizeOfT()
        {
            return Marshal.SizeOf(typeof(T));
        }

        public ImageChannel(int width, int height, IntPtr start, int pixelSize)
        {
            if (width <= 0) throw new ArgumentOutOfRangeException("width");
            else if (height <= 0) throw new ArgumentOutOfRangeException("height");
            else if (pixelSize <= 0) throw new ArgumentOutOfRangeException("pixelSize");

            this.Width = width;
            this.Height = height;
            this.Length = width * height;
            this.StartIntPtr = start;
            this.PixelSize = pixelSize;
            this.SizeOfType = SizeOfT();
        }

        public ImageChannel(int width, int height)
        {
            if (width <= 0) throw new ArgumentOutOfRangeException("width");
            else if (height <= 0) throw new ArgumentOutOfRangeException("height");
            this.Width = width;
            this.Height = height;
            this.Length = width * height;
            this.SizeOfType = SizeOfT();
            this.PixelSize = this.SizeOfType;
            IsMemoryManager = true;
            StartIntPtr = Marshal.AllocHGlobal(SizeOfType * Length);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (false == disposed)
            {
                 disposed = true;
                 if (IsMemoryManager == true)
                 {
                     Marshal.FreeHGlobal(StartIntPtr);
                 }
            }
        }

        private bool disposed;

        ~ImageChannel()
        {
            Dispose(false);
        }
    }
}
