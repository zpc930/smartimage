using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Orc.SmartImage
{
    public struct ByteConverter : IColorConverter
    {
        public unsafe void Copy(Rgb24* from, void* to, int length)
        {
            UnmanagedImageConverter.ToByte(from, (byte*)to, length);
        }

        public unsafe void Copy(Argb32* from, void* to, int length)
        {
            UnmanagedImageConverter.ToByte(from, (byte*)to, length);
        }

        public unsafe void Copy(byte* from, void* to, int length)
        {
            UnmanagedImageConverter.Copy(from, (byte*)to, length);
        }
    }

    public partial class ImageU8 : UnmanagedImage<Byte>
    {
        public unsafe ImageU8(Int32 width, Int32 height)
            : base(width, height)
        {
        }

        public ImageU8(Bitmap map)
            : base(map)
        {
        }

        protected override IColorConverter CreateByteConverter()
        {
            return new ByteConverter();
        }

        public override IImage Clone()
        {
            ImageU8 img = new ImageU8(this.Width, this.Height);
            img.CloneFrom(this);
            return img;
        }

        public unsafe void Invert()
        {
            Byte* p = this.Start;
            Byte* end = p + this.Length;
            while (p != end)
            {
                *p = (Byte)(255 - *p);
                p++;
            }
        }

        /// <summary>
        /// 计算八联结的联结数，计算公式为：
        ///     (p6 - p6*p7*p0) + sigma(pk - pk*p(k+1)*p(k+2)), k = {0,2,4)
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private unsafe Int32 DetectConnectivity(Int32* list)
        {
            Int32 count = list[6] - list[6] * list[7] * list[0];
            count += list[0] - list[0] * list[1] * list[2];
            count += list[2] - list[2] * list[3] * list[4];
            count += list[4] - list[4] * list[5] * list[6];
            return count;
        }

        private unsafe void FillNeighbors(Byte* p, Int32* list, Int32 width, Byte foreground = 255)
        {
            // list 存储的是补集，即前景点为0，背景点为1，以方便联结数的计算

            list[0] = p[1] == foreground ? 0 : 1;
            list[1] = p[1 - width] == foreground ? 0 : 1;
            list[2] = p[-width] == foreground ? 0 : 1;
            list[3] = p[-1 - width] == foreground ? 0 : 1;
            list[4] = p[-1] == foreground ? 0 : 1;
            list[5] = p[-1 + width] == foreground ? 0 : 1;
            list[6] = p[width] == foreground ? 0 : 1;
            list[7] = p[1 + width] == foreground ? 0 : 1;
        }

        /// <summary>
        /// 使用 hilditch 算法进行细化
        /// </summary>
        public unsafe void Thinning(Byte foreground = 255)
        {
            Byte* start = this.Start;
            Int32 width = this.Width;
            Int32 height = this.Height;
            Int32* list = stackalloc Int32[8];
            Byte background = (Byte)(255 - foreground);
            Int32 length = this.Length;

            using (ImageU8 mask = new ImageU8(this.Width, this.Height))
            {
                mask.Fill(0);

                Boolean loop = true;
                while (loop == true)
                {
                    loop = false;
                    for (Int32 r = 1; r < height - 1; r++)
                    {
                        for (Int32 c = 1; c < width - 1; c++)
                        {
                            Byte* p = start + r * width + c;

                            // 条件1：p 必须是前景点
                            if (*p != foreground) continue;

                            //  p3  p2  p1
                            //  p4  p   p0
                            //  p5  p6  p7
                            // list 存储的是补集，即前景点为0，背景点为1，以方便联结数的计算
                            FillNeighbors(p, list, width, foreground);

                            // 条件2：p0,p2,p4,p6 不皆为前景点
                            if (list[0] == 0 && list[2] == 0 && list[4] == 0 && list[6] == 0)
                                continue;

                            // 条件3: p0~p7至少两个是前景点
                            Int32 count = 0;
                            for (int i = 0; i < 8; i++)
                            {
                                count += list[i];
                            }

                            if (count > 6) continue;

                            // 条件4：联结数等于1
                            if (DetectConnectivity(list) != 1) continue;

                            // 条件5: 假设p2已标记删除，则令p2为背景，不改变p的联结数
                            if (mask[r - 1, c] == 1)
                            {
                                list[2] = 1;
                                if (DetectConnectivity(list) != 1)
                                    continue;
                                list[2] = 0;
                            }

                            // 条件6: 假设p4已标记删除，则令p4为背景，不改变p的联结数
                            if (mask[r, c - 1] == 1)
                            {
                                list[4] = 1;
                                if (DetectConnectivity(list) != 1)
                                    continue;
                            }
                            mask[r, c] = 1; // 标记删除
                            loop = true;
                        }
                    }

                    for (int i = 0; i < length; i++)
                    {
                        if (mask[i] == 1)
                        {
                            this[i] = background;
                        }
                    }
                }
            }
        }

        public unsafe void SkeletonizeByMidPoint(Byte foreground = 255)
        {
            using (ImageU8 mask = new ImageU8(this.Width, this.Height))
            {
                mask.Fill(0);
                Int32 width = this.Width;
                Int32 height = this.Height;
                for (Int32 r = 0; r < height; r++)
                {
                    Int32 lineStart = -1;
                    for (Int32 c = 0; c < width; c++)
                    {
                        if (this[r, c] == foreground)
                        {
                            if (lineStart == -1) lineStart = c;
                        }
                        else
                        {
                            if (lineStart != -1)
                            {
                                mask[r, (lineStart + c) / 2] = 1;
                                lineStart = -1;
                            }
                        }
                    }
                }

                for (Int32 c = 0; c < width; c++)
                {
                    Int32 lineStart = -1;
                    for (Int32 r = 0; r < height; r++)
                    {
                        if (this[r, c] == foreground)
                        {
                            if (lineStart == -1) lineStart = r;
                        }
                        else
                        {
                            if (lineStart != -1)
                            {
                                mask[(lineStart + r) / 2, c] = 1;
                                lineStart = -1;
                            }
                        }
                    }
                }

                Byte bg = (Byte)(255 - foreground);
                Int32 length = this.Length;
                for (int i = 0; i < length; i++)
                {
                    if (mask[i] == 1)
                    {
                        this[i] = foreground;
                    }
                    else
                    {
                        this[i] = bg;
                    }
                }
            }
        }

        public unsafe ImageInt32 ToImageInt32()
        {
            ImageInt32 img32 = new ImageInt32(this.Width, this.Height);
            Byte* start = this.Start;
            Byte* end = this.Start + this.Length;
            Int32* dst = img32.Start;
            while (start != end)
            {
                *dst = *start;
                start++;
                dst++;
            }
            return img32;
        }

        public void ApplyGaussianBlur(double sigma = 1.4, int size = 5)
        {
            ConvolutionKernel kernel = ConvolutionKernel.CreateGaussianKernel(sigma, size);
            this.ApplyConvolution(kernel);
        }

        public unsafe void ApplyCannyEdgeDetector(double gaussianSiama = 1.4, int gaussianSize = 5, byte lowThreshold = 20, byte highThreshold = 100)
        {
            ImageU8 copy = this.Clone() as ImageU8;
            int startX = 1;
            int startY = 1;
            int width = this.Width;
            int height = this.Height;

            int stopX = width - 1;
            int stopY = height - 1;
            int ww = width - 2;
            int hh = height - 2;

            // orientation array
            byte[] orients = new byte[ww * hh];
            // gradients array
            float[,] gradients = new float[this.Width, this.Height];
            float maxGradient = float.NegativeInfinity;
            double gx, gy;
            double orientation, toAngle = 180.0 / System.Math.PI;
            float leftPixel = 0, rightPixel = 0;

            // 第一步，Gauss 平滑
            copy.ApplyGaussianBlur(gaussianSiama, gaussianSize);
            byte* start = copy.Start + startX;
            int o = 0;
            for (int y = startY; y < stopY; y++)
            {
                byte* line = start + y * width;
                byte* p = line;
                for (int x = startX; x < stopX; x++, p++, o++)
                {
                    gx = p[-width+1] + p[width+1] 
                        - p[-width-1] - p[width-1]
                        + 2 * (p[1] - p[-1]);
                    gy = p[-width - 1] + p[-width + 1]
                        - p[width - 1] - p[width + 1]
                        + 2 * (p[-width] - p[width]);
                    gradients[x, y] = (float)Math.Sqrt(gx * gx + gy * gy);
                    if (gradients[x, y] > maxGradient)
                        maxGradient = gradients[x, y];

                    // get orientation
                    if (gx == 0)
                    {
                        orientation = (gy == 0) ? 0 : 90;
                    }
                    else
                    {
                        double div = gy / gx;
                        // handle angles of the 2nd and 4th quads
                        if (div < 0)
                        {
                            orientation = 180 - System.Math.Atan(-div) * toAngle;
                        }
                        // handle angles of the 1st and 3rd quads
                        else
                        {
                            orientation = System.Math.Atan(div) * toAngle;
                        }

                        // get closest angle from 0, 45, 90, 135 set
                        if (orientation < 22.5)
                            orientation = 0;
                        else if (orientation < 67.5)
                            orientation = 45;
                        else if (orientation < 112.5)
                            orientation = 90;
                        else if (orientation < 157.5)
                            orientation = 135;
                        else orientation = 0;
                    }

                    // save orientation
                    orients[o] = (byte)orientation;
                }
            }

            // STEP 3 - suppres non maximums
            o = 0;
            start = this.Start + startX;
            for (int y = startY; y < stopY; y++)
            {
                byte* line = start + y * width;
                byte* p = line;
                // for each pixel
                for (int x = startX; x < stopX; x++, p++, o++)
                {
                    // get two adjacent pixels
                    switch (orients[o])
                    {
                        case 0:
                            leftPixel = gradients[x - 1, y];
                            rightPixel = gradients[x + 1, y];
                            break;
                        case 45:
                            leftPixel = gradients[x - 1, y + 1];
                            rightPixel = gradients[x + 1, y - 1];
                            break;
                        case 90:
                            leftPixel = gradients[x, y + 1];
                            rightPixel = gradients[x, y - 1];
                            break;
                        case 135:
                            leftPixel = gradients[x + 1, y + 1];
                            rightPixel = gradients[x - 1, y - 1];
                            break;
                    }
                    // compare current pixels value with adjacent pixels
                    if ((gradients[x, y] < leftPixel) || (gradients[x, y] < rightPixel))
                    {
                        *p = 0;
                    }
                    else
                    {
                        *p = (byte)(gradients[x, y] / maxGradient * 255);
                    }
                }
            }

            // STEP 4 - hysteresis
            start = this.Start + startX;
            for (int y = startY; y < stopY; y++)
            {
                byte* line = start + y * width;
                byte* p = line;
                for (int x = startX; x < stopX; x++, p++)
                {
                    if (*p < highThreshold)
                    {
                        if (*p < lowThreshold)
                        {
                            // non edge
                            *p = 0;
                        }
                        else
                        {
                            // check 8 neighboring pixels
                            if ((p[-1] < highThreshold) &&
                                (p[1] < highThreshold) &&
                                (p[-width - 1] < highThreshold) &&
                                (p[-width] < highThreshold) &&
                                (p[-width + 1] < highThreshold) &&
                                (p[width - 1] < highThreshold) &&
                                (p[width] < highThreshold) &&
                                (p[width + 1] < highThreshold))
                            {
                                *p = 0;
                            }
                        }
                    }
                }
            }

            // STEP 4 将第1行，最后一行，第0列，最后1列

            // 第1行
            start = this.Start;
            byte* end = start + width;
            while (start != end)
            {
                *start = 0;
                start++;
            }

            // 最后一行
            start = this.Start + width * height - width;
            end = start + width;
            while (start != end)
            {
                *start = 0;
                start++;
            }

            // 第一列和最后一列
            start = this.Start;
            for (int y = 0; y < height; y++,start += width)
            {
                start[0] = 0;
                start[width-1] = 0;
            }
        }

        protected override System.Drawing.Imaging.PixelFormat GetOutputBitmapPixelFormat()
        {
            return System.Drawing.Imaging.PixelFormat.Format8bppIndexed;
        }

        protected override unsafe void ToBitmapCore(byte* src, byte* dst, int width)
        {
            UnmanagedImageConverter.Copy(src, dst, width);
        }
    }
}
