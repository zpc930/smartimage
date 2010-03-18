using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Orc.SmartImage
{
    public abstract class UnmanagedImage<T> : IDisposable
        where T : struct
    {
        public Int32 ByteCount { get; private set; }
        public Int32 Length { get; private set; }
        public Int32 SizeOfType { get; private set; }

        public Int32 Width { get; private set; }
        public Int32 Height { get; private set; }

        public IntPtr StartIntPtr { get; private set; }

        public UnmanagedImage(Int32 width, Int32 height)
        {
            Width = width;
            Height = height;
            Length = Width * Height;
            SizeOfType = SizeOfT();
            ByteCount = SizeOfType * Length;
            StartIntPtr = Marshal.AllocHGlobal(ByteCount);
        }

        public UnmanagedImage(Bitmap map):this(map.Width, map.Height)
        {
            if (map == null) throw new ArgumentNullException("map");
            this.CreateFromBitmap(map);
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
                 Marshal.FreeHGlobal(StartIntPtr);
            }
        }

        private bool disposed;

        ~UnmanagedImage()
        {
            Dispose(false);
        }

        private static Int32 SizeOfT()
        {
            return Marshal.SizeOf(typeof(T));
        }

        protected abstract IColorConverter GetColorConvert();

        private unsafe void CreateFromBitmap(Bitmap map)
        {
            int height = map.Height;
            int width = map.Width;
            PixelFormat format = map.PixelFormat;

            if (this.Width != width || this.Height != height)
            {
                return;
            }

            Bitmap newMap = map;
            IColorConverter convert = GetColorConvert();
            Int32 step = SizeOfT();

            switch (format)
            {
                case PixelFormat.Format24bppRgb:
                    break;
                case PixelFormat.Format32bppArgb:
                    break;
                default:
                    format = PixelFormat.Format32bppArgb;
                    newMap = map.Clone(new Rectangle(0, 0, width, height), PixelFormat.Format32bppArgb);
                    break;
            }

            Byte* t = (Byte*)StartIntPtr;

            BitmapData data = newMap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, format);
            try
            {
                if (format == PixelFormat.Format24bppRgb)
                {
                    Byte* line = (Byte*)data.Scan0;

                    for (int h = 0; h < height; h++)
                    {
                        Rgb24* c = (Rgb24*)line;
                        for (int w = 0; w < width; w++)
                        {
                            convert.Copy(c, t);
                            t += step;
                            c++;
                        }
                        line += data.Stride;
                    }
                }
                else
                {
                    Byte* line = (Byte*)data.Scan0;

                    for (int h = 0; h < height; h++)
                    {
                        Argb32* c = (Argb32*)line;
                        for (int w = 0; w < width; w++)
                        {
                            convert.Copy(c, t);
                            t += step;
                            c++;
                        }
                        line += data.Stride;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                newMap.UnlockBits(data);
            }
        }

        public unsafe Bitmap ToBitmap()
        {
            Bitmap map = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);
            ToBitmap(map);
            return map;
        }

        public unsafe void ToBitmap(Bitmap map)
        {
            if (map == null) throw new ArgumentNullException("map");
            if (map.Width != this.Width || map.Height != this.Height)
            {
                throw new ArgumentException("尺寸不匹配.");
            }

            if (map.PixelFormat != PixelFormat.Format32bppArgb)
            {
                throw new ArgumentException("只支持 Format32bppArgb 格式。 ");
            }

            // map.InitGrayscalePalette();

            IColorConverter convert = GetColorConvert();
            Int32 step = SizeOfT();
            Byte* t = (Byte*)StartIntPtr;

            BitmapData data = map.LockBits(new Rectangle(0, 0, map.Width, map.Height), ImageLockMode.ReadWrite, map.PixelFormat);
            try
            {
                int width = map.Width;
                int height = map.Height;


                Byte* line = (Byte*)data.Scan0;

                for (int h = 0; h < height; h++)
                {
                    Argb32* c = (Argb32*)line;
                    for (int w = 0; w < width; w++)
                    {
                        convert.Copy(t, c);
                        t += step;
                        c++;
                    }
                    line += data.Stride;
                }
            }
            finally
            {
                map.UnlockBits(data);
            }
        }
    }
}
