using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Orc.SmartImage
{
    public abstract class UnmanagedImage<T> : IDisposable, IEnumerable<T>
        where T : struct
    {
        public Int32 ByteCount { get; private set; }
        public Int32 Length { get; private set; }
        public Int32 SizeOfType { get; private set; }

        public Int32 Width { get; private set; }
        public Int32 Height { get; private set; }

        public IntPtr StartIntPtr { get; private set; }

        private IByteConverter<T> m_converter;
        private unsafe Byte* m_start;

        public unsafe UnmanagedImage(Int32 width, Int32 height)
        {
            Width = width;
            Height = height;
            Length = Width * Height;
            SizeOfType = SizeOfT();
            ByteCount = SizeOfType * Length;
            m_converter = this.CreateByteConverter();
            StartIntPtr = Marshal.AllocHGlobal(ByteCount);
            m_start = (Byte*)StartIntPtr;
        }

        public UnmanagedImage(Bitmap map):this(map.Width, map.Height)
        {
            if (map == null) throw new ArgumentNullException("map");
            this.CreateFromBitmap(map);
        }

        /// <summary>
        /// 性能约是指针操作的1/4。不适用于性能要求高的地方。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public unsafe T this[int index]
        {
            get
            {
                T t = new T();
                m_converter.Copy(m_start + index * SizeOfType, ref t);
                return t;
            }
            set 
            {
                Byte* to = m_start + index * SizeOfType;
                m_converter.Copy(ref value, to);
            }
        }

        /// <summary>
        /// 性能约是指针操作的1/4。不适用于性能要求高的地方。
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public unsafe T this[int row, int col]
        {
             get
            {
                T t = new T();
                m_converter.Copy(m_start + (row * Width + col) * SizeOfType, ref t);
                return t;
            }
            set 
            {
                Byte* to = m_start + (row * Width + col) * SizeOfType;
                m_converter.Copy(ref value, to);
            }
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
                            m_converter.Copy(c, t);
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
                            m_converter.Copy(c, t);
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
                        m_converter.Copy(t, c);
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

        protected abstract IByteConverter<T> CreateByteConverter();

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return new ImageEnum<T>(this, this.m_converter);
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }

    internal class ImageEnum<T> : IEnumerator<T>
        where T : struct
    {
        private UnmanagedImage<T> m_img;
        private unsafe Byte* m_start;
        private Int32 m_step;
        private unsafe Byte* m_end;
        private unsafe Byte* m_current;
        private IByteConverter<T> m_converter;

        public unsafe ImageEnum(UnmanagedImage<T> img, IByteConverter<T> converter)
        {
            m_img = img;
            m_start = (Byte*)m_img.StartIntPtr;
            m_step = m_img.SizeOfType;
            m_end = m_start + m_step * m_img.Length;
            m_current = m_start;
            m_converter = converter;
        }

        #region IEnumerator<T> Members

        public unsafe T Current
        {
            get 
            { 
                T t = new T();
                m_converter.Copy(m_current, ref t); 
                return t; 
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion

        #region IEnumerator Members

        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }

        public unsafe bool MoveNext()
        {
            m_current += this.m_step;
            return m_current < m_end;
        }

        public unsafe void Reset()
        {
            m_current = m_start;
        }

        #endregion
    }
}
