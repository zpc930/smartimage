using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace Orc.SmartImage
{
	public class ImageChannel
	{
		private int[,] m_buff;
		private int m_width;
		private int m_height;

		public ImageChannel(int width, int height)
		{
            m_buff = new int[height, width];
			m_width = width;
			m_height = height;
		}

        /// <summary>
        /// int [height, width]
        /// </summary>
        public int[,] Data { get { return m_buff; } }

		public int Width { get { return m_width; } }

		public int Height { get { return m_height; } }

        public void HandleEachPixel(Func<ImageChannel, int, int, int> pixelHandler)
		{
            int[,] cache = new int[m_width, m_height];

            for (int i = 0; i < m_width; i++)
			{
                for (int j = 0; j < m_height; j++)
				{
                    int value = pixelHandler(this, i, j);
					cache[i, j] = value;
				}
			}

			m_buff = cache;
		}

        public List<Point> QueryPoints(Predicate<int> predicate)
        {
            List<Point> points = new List<Point>();

            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    if (predicate(this.m_buff[y, x]) == true)
                        points.Add(new Point(x, y));
                }
            }

            return points;
        }

        public unsafe Bitmap ToGrayBitmap()
		{
			Bitmap map = new Bitmap(m_width, m_height, PixelFormat.Format8bppIndexed);
			for (int i = 0; i < 255; i++)
			{
				map.Palette.Entries[i] = Color.FromArgb(i, i, i);
			}

			int width = map.Width;
			int height = map.Height;
			BitmapData d = map.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, map.PixelFormat);
			try
			{
				int startX = 0;
				int startY = 0;
				int stopX = width;
				int stopY = height;
				int stride = d.Stride;
				int pixelSize = 1;
				byte* line = (byte*)d.Scan0;
				byte* point = line;

				for (int y = startY; y < stopY; y++)
				{
					for (int x = startX; x < stopX; x++)
					{
						*point = (byte)(m_buff[y, x]);
						point += pixelSize;
					}
					line += stride;
					point = line;
				}
			}
			finally
			{
				map.UnlockBits(d);
			}
			return map;
		}

        /// <summary>
        /// 计算输入点的卷积。如果邻域在边界之外，该点值用 0 值来计算卷积。
        /// </summary>
        /// <param name="kernel">卷积核</param>
        /// <param name="points">输入点</param>
        /// <returns>卷积结果</returns>
        public List<double> ConvolutionPoints(ConvolutionKernel kernel, IList<Point> points)
        {
            List<double> results = new List<double>(points.Count);
            foreach (var item in points)
                results.Add(ConvolutionPointCoreDouble(kernel, item.X, item.Y));
            return results;
        }

        /// <summary>
        /// 计算卷积。如果邻域在边界之外，该点值用 0 值来计算卷积。
        /// </summary>
        /// <param name="kernel">卷积核</param>
        /// <returns>卷积结果</returns>
        public ImageChannel Convolution(ConvolutionKernel kernel)
        {
            ImageChannel buf = new ImageChannel(this.Width, this.Height);
            int[,] srcData = this.Data;
            int[,] dstData = buf.Data;
            for(int y=0; y < this.Height; y++)
                for (int x = 0; x < this.Width; x++)
                {
                    dstData[y, x] = ConvolutionPointCore(kernel, x, y);
                }
            return buf;
        }

        private double ConvolutionPointCoreDouble(ConvolutionKernel kernel, int pointX, int pointY)
        {
            int rootY = pointY - (kernel.Height >> 1);
            int rootX = pointX - (kernel.Width >> 1);
            int stopY = rootY + kernel.Height;
            int stopX = rootX + kernel.Width;
            int[,] k = kernel.Kernel;
            double r = 0;
            for (int y = rootY; y < stopY; y++)
            {
                for (int x = rootX; x < stopX; x++)
                {
                    if (x >= 0 && y >= 0 && x < Width && y < Height)
                        r += this.m_buff[y, x] * k[y - rootY, x - rootX];
                }
            }
            return r / kernel.Divisor;
        }

        private int ConvolutionPointCore(ConvolutionKernel kernel, int pointX, int pointY)
        {
            int rootY = pointY - (kernel.Height >> 1);
            int rootX = pointX - (kernel.Width >> 1);
            int stopY = rootY + kernel.Height;
            int stopX = rootX + kernel.Width;
            int[,] k = kernel.Kernel;
            int r = 0;
            for (int y = rootY; y < stopY; y++)
            {
                for (int x = rootX; x < stopX; x++)
                {
                    if (x >= 0 && y >= 0 && x < Width && y < Height)
                        r += this.m_buff[y, x] * k[y - rootY, x - rootX];
                }
            }
            return r / kernel.Divisor;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.Width; i++)
            {
                for (int j = 0; j < this.Height; j++)
                {
                    int value = this.Data[j, i];
                    sb.Append(value.ToString("X2")).Append(' ');
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        #region 静态方法

        public unsafe static ImageChannel CreateFrom(Bitmap map)
        {
            if (map == null) throw new ArgumentNullException("map");

            int width = map.Width;
            int height = map.Height;
            ImageChannel buff = new ImageChannel(width, height);
            int[,] buffData = buff.Data;
            BitmapData d = map.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, map.PixelFormat);
            try
            {
                int startX = 0;
                int startY = 0;
                int stopX = width;
                int stopY = height;
                int stride = d.Stride;
                int pixelSize = 1;
                Color[] paletteColors = map.Palette.Entries;

                switch (d.PixelFormat)
                {
                    case PixelFormat.Format8bppIndexed:
                        break;
                    case PixelFormat.Format32bppArgb:
                    case PixelFormat.Format32bppPArgb:
                    case PixelFormat.Format32bppRgb:
                        pixelSize = 4;
                        break;
                    case PixelFormat.Format16bppArgb1555:
                    case PixelFormat.Format16bppGrayScale:
                    case PixelFormat.Format16bppRgb555:
                    case PixelFormat.Format16bppRgb565:
                    case PixelFormat.Format24bppRgb:
                    case PixelFormat.Format48bppRgb:
                    case PixelFormat.Format64bppArgb:
                    case PixelFormat.Format64bppPArgb:
                    case PixelFormat.Format1bppIndexed:
                    case PixelFormat.Format4bppIndexed:
                    default:
                        throw new ArgumentException("目前只支持8位与32位的图像");
                }
                byte* line = (byte*)d.Scan0;
                byte* point = line;

                for (int y = startY; y < stopY; y++)
                {
                    for (int x = startX; x < stopX; x++)
                    {

                        switch (pixelSize)
                        {
                            case 1:
                                byte p = *point;
                                buffData[y, x] = ToGray(paletteColors[p]);
                                break;
                            case 4:
                                buffData[y, x] = *((Int32*)point);
                                break;
                        }
                        point += pixelSize;
                    }
                    line += stride;
                    point = line;
                }
            }
            finally
            {
                map.UnlockBits(d);
            }
            return buff;
        }

        public static ImageChannel CreateGrayScaleFromImage(Bitmap map)
        {
            if (map == null) throw new ArgumentNullException("map");

            int width = map.Width;
            int height = map.Height;
            ImageChannel buff = new ImageChannel(width, height);
            int[,] buffData = buff.Data;

            for (int i = 0; i < map.Width; i++)
            {
                for (int j = 0; j < map.Height; j++)
                {
                    Color c = map.GetPixel(i, j);
                    int gray = ToGray(c);
                    buffData[j, i] = gray;
                }
            }

            return buff;
        }

        public unsafe static ImageChannel CreateFromImageFormat8bppGrayImage(Bitmap map)
        {
            if (map == null) throw new ArgumentNullException("map");
            if (map.PixelFormat != PixelFormat.Format8bppIndexed)
                throw new ArgumentException("只支持Format8bppIndexed格式的图像");

            int width = map.Width;
            int height = map.Height;
            ImageChannel buff = new ImageChannel(width, height);
            int[,] buffData = buff.Data;
            BitmapData d = map.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, map.PixelFormat);
            try
            {
                int startX = 0;
                int startY = 0;
                int stopX = width;
                int stopY = height;
                int stride = d.Stride;
                int pixelSize = 1;
                byte* line = (byte*)d.Scan0;
                byte* point = line;

                for (int y = startY; y < stopY; y++)
                {
                    for (int x = startX; x < stopX; x++)
                    {
                        buffData[y, x] = *point;
                        point += pixelSize;
                    }
                    line += stride;
                    point = line;
                }
            }
            finally
            {
                map.UnlockBits(d);
            }
            return buff;
        }
        
        #endregion

        public static int ToGray(Color c)
        {
            return (int)(0.299 * c.R + 0.587 * c.G + 0.114 * c.B);
        }
    }
}
