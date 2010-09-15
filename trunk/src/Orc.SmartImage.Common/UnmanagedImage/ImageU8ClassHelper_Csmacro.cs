using System;
using System.Collections.Generic;
using System.Text;

namespace Orc.SmartImage
{
    using TPixel = System.Byte;
    using TCache = System.Int32;
    using TKernel = System.Int32;
    using TImage = Orc.SmartImage.ImageU8;

    public static partial class ImageU8ClassHelper
    {
        

        public unsafe delegate void ActionOnPixel(TPixel* p);
        public unsafe delegate void ActionWithPosition(Int32 row, Int32 column, TPixel* p);
        public unsafe delegate Boolean PredicateOnPixel(TPixel* p);

        public unsafe static void ForEach(this UnmanagedImage<TPixel> src, ActionOnPixel handler)
        {
            TPixel* start = (TPixel*)src.StartIntPtr;
            TPixel* end = start + src.Length;
            while (start != end)
            {
                handler(start);
                ++start;
            }
        }

        public unsafe static void ForEach(this UnmanagedImage<TPixel> src, ActionWithPosition handler)
        {
            Int32 width = src.Width;
            Int32 height = src.Height;

            TPixel* p = (TPixel*)src.StartIntPtr;
            for (Int32 r = 0; r < height; r++)
            {
                for (Int32 w = 0; w < width; w++)
                {
                    handler(w, r, p);
                    p++;
                }
            }
        }

        public unsafe static void ForEach(this UnmanagedImage<TPixel> src, TPixel* start, uint length, ActionOnPixel handler)
        {
            TPixel* end = start + src.Length;
            while (start != end)
            {
                handler(start);
                ++start;
            }
        }

        public unsafe static Int32 Count(this UnmanagedImage<TPixel> src, PredicateOnPixel handler)
        {
            TPixel* start = (TPixel*)src.StartIntPtr;
            TPixel* end = start + src.Length;
            Int32 count = 0;
            while (start != end)
            {
                if (handler(start) == true) count++;
                ++start;
            }
            return count;
        }

        public unsafe static Int32 Count(this UnmanagedImage<TPixel> src, Predicate<TPixel> handler)
        {
            TPixel* start = (TPixel*)src.StartIntPtr;
            TPixel* end = start + src.Length;
            Int32 count = 0;
            while (start != end)
            {
                if (handler(*start) == true) count++;
                ++start;
            }
            return count;
        }

        public unsafe static List<TPixel> Where(this UnmanagedImage<TPixel> src, PredicateOnPixel handler)
        {
            List<TPixel> list = new List<TPixel>();

            TPixel* start = (TPixel*)src.StartIntPtr;
            TPixel* end = start + src.Length;
            while (start != end)
            {
                if (handler(start) == true) list.Add(*start);
                ++start;
            }

            return list;
        }

        public unsafe static List<TPixel> Where(this UnmanagedImage<TPixel> src, Predicate<TPixel> handler)
        {
            List<TPixel> list = new List<TPixel>();

            TPixel* start = (TPixel*)src.StartIntPtr;
            TPixel* end = start + src.Length;
            while (start != end)
            {
                if (handler(*start) == true) list.Add(*start);
                ++start;
            }

            return list;
        }

        /// <summary>
        /// 查找模板。模板中值代表实际像素值。负数代表任何像素。返回查找得到的像素的左上端点的位置。
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public static unsafe List<System.Drawing.Point> FindTemplate(this UnmanagedImage<TPixel> src, int[,] template)
        {
            List<System.Drawing.Point> finds = new List<System.Drawing.Point>();
            int tHeight = template.GetUpperBound(0) + 1;
            int tWidth = template.GetUpperBound(1) + 1;
            int toWidth = src.Width - tWidth + 1;
            int toHeight = src.Height - tHeight + 1;
            int stride = src.Width;
            TPixel* start = (TPixel*)src.StartIntPtr;
            for (int r = 0; r < toHeight; r++)
            {
                for (int c = 0; c < toWidth; c++)
                {
                    TPixel* srcStart = start + r * stride + c;
                    for (int rr = 0; rr < tHeight; rr++)
                    {
                        for (int cc = 0; cc < tWidth; cc++)
                        {
                            int pattern = template[rr, cc];
                            if (pattern >= 0 && srcStart[rr * stride + cc] != pattern)
                            {
                                goto Next;
                            }
                        }
                    }

                    finds.Add(new System.Drawing.Point(c, r));

                Next:
                    continue;
                }
            }

            return finds;
        }

        
    }

    public partial class ImageU8
    {
        

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

        public unsafe TPixel this[System.Drawing.Point location]
        {
            get
            {
                return this[location.Y, location.X];
            }
            set
            {
                this[location.Y, location.X] = value;
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

        public unsafe void Replace(TPixel pixel, TPixel replaced)
        {
            TPixel* p = this.Start;
            TPixel* end = p + this.Length;
            while (p != end)
            {
                if (*p == pixel)
                {
                    *p = replaced;
                }
                p++;
            }
        }

        public unsafe void Copy(UnmanagedImage<TPixel> src, System.Drawing.Point start, System.Drawing.Rectangle region, System.Drawing.Point destAnchor)
        {
            if (start.X >= src.Width || start.Y >= src.Height) return;
            int startSrcX = Math.Max(0, start.X);
            int startSrcY = Math.Max(0, start.Y);
            int endSrcX = Math.Min(start.X + region.Width, src.Width);
            int endSrcY = Math.Min(start.Y + region.Height, src.Height);
            int offsetX = start.X < 0? -start.X : 0;
            int offsetY = start.Y < 0? -start.Y : 0;
            offsetX = destAnchor.X + offsetX;
            offsetY = destAnchor.Y + offsetY;
            int startDstX = Math.Max(0, offsetX);
            int startDstY = Math.Max(0, offsetY);
            offsetX = offsetX < 0 ? -offsetX : 0;
            offsetY = offsetY < 0 ? -offsetY : 0;
            startSrcX += offsetX;
            startSrcY += offsetY;
            int endDstX = Math.Min(destAnchor.X + region.Width, this.Width);
            int endDstY = Math.Min(destAnchor.Y + region.Height, this.Height);
            int copyWidth = Math.Min(endSrcX - startSrcX, endDstX - startDstX);
            int copyHeight = Math.Min(endSrcY - startSrcY, endDstY - startDstY);
            if (copyWidth <= 0 || copyHeight <= 0) return;

            int srcWidth = src.Width;
            int dstWidth = this.Width;

            TPixel* srcLine = (TPixel*)(src.StartIntPtr) + srcWidth * startSrcY + startSrcX;
            TPixel* dstLine = this.Start + dstWidth * startDstY + startDstX;
            TPixel* endSrcLine = srcLine + srcWidth * copyHeight;
            while (srcLine < endSrcLine)
            {
                TPixel* pSrc = srcLine;
                TPixel* endPSrc = pSrc + copyWidth;
                TPixel* pDst = dstLine;
                while (pSrc < endPSrc)
                {
                    *pDst = *pSrc;
                    pSrc++;
                    pDst++;
                }
                srcLine += srcWidth;
                dstLine += dstWidth;
            }
        }

        

        

        public unsafe void ApplyConvolution(ConvolutionKernel  k)
        {
            int kernelHeight = k.Width;
            int kernelWidth = k.Height;
            int scale = k.Scale;
            int[,] kernel = k.Kernel;
            int extend = Math.Max(kernelWidth, kernelHeight) / 2;
            TImage maskImage = new TImage(Width + extend * 2, Height + extend * 2);
            maskImage.Fill(0);//这里效率不高。原本只需要填充四周扩大的部分即可
            maskImage.Copy(this, new System.Drawing.Point(0, 0), new System.Drawing.Rectangle(0, 0, this.Width, this.Height), new System.Drawing.Point(extend, extend));
            int width = this.Width;
            int height = this.Height;
            TPixel* start = (TPixel*)this.StartIntPtr;
            if (scale == 1)
            {
                for (int h = 0; h < height; h++)
                {
                    for (int w = 0; w < width; w++)
                    {
                        int val = 0;
                        for (int kw = 0; kw < kernelWidth; kw++)
                        {
                            for (int kh = 0; kh < kernelHeight; kh++)
                            {
                                val += maskImage[h + kh, w + kw] * kernel[kh, kw];
                            }
                        }
                        start[h * width + w] = (TPixel)val;
                    }
                }
            }
            else
            {
                double factor = 1.0 / scale;
                for (int h = 0; h < height; h++)
                {
                    for (int w = 0; w < width; w++)
                    {
                        int val = 0;
                        for (int kw = 0; kw < kernelWidth; kw++)
                        {
                            for (int kh = 0; kh < kernelHeight; kh++)
                            {
                                val += maskImage[h + kh, w + kw] * kernel[kh, kw];
                            }
                        }
                        start[h * width + w] = (TPixel)(val * factor);
                    }
                }
            }
            maskImage.Dispose();
        }

        
    }
}



