using TPixel = System.Byte;
using TCache = System.Int32;
using TKernel = System.Int32;
using TImage = Orc.SmartImage.ImageU8;

using System;
using System.Collections.Generic;
using System.Text;

namespace Orc.SmartImage.Hidden
{
    public abstract class ImageFilter_Template : UnmanagedImage<TPixel>
    {
        private ImageFilter_Template()
            : base(1, 1)
        {
            throw new NotImplementedException();
        }

        #region mixin

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

        #endregion
    }
}
