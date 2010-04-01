using TPixel = System.Byte;
using TCache = System.Int32;
using TKernel = System.Int32;

using System;
using System.Collections.Generic;
using System.Text;

namespace Orc.SmartImage.Hidden
{
    static class FilterHelper_Template
    {
        #region mixin

        // 本算法是错误的，只为说明C#模板程序的使用。
        public unsafe static UnmanagedImage<TPixel> Filter(this UnmanagedImage<TPixel> src, FilterKernel<TKernel> filter)
        {
            TKernel* kernel = stackalloc TKernel[filter.Length];

            Int32 srcWidth = src.Width;
            Int32 srcHeight = src.Height;
            Int32 kWidth = filter.Width;
            Int32 kHeight = filter.Height;

            TPixel* start = (TPixel*)src.StartIntPtr;
            TPixel* lineStart = start;
            TPixel* pStart = start;
            TPixel* pTemStart = pStart;
            TPixel* pT;
            TKernel* pK;

            for (int c = 0; c < srcWidth; c++)
            {
                for (int r = 0; r < srcHeight; r++)
                {
                    pTemStart = pStart;
                    pK = kernel;

                    Int32 val = 0;
                    for (int kc = 0; kc < kWidth; kc++)
                    {
                        pT = pStart;
                        for (int kr = 0; kr < kHeight; kr++)
                        {
                            val += *pK * *pT;
                            pT++;
                            pK++;
                        }

                        pStart += srcWidth;
                    }

                    pStart = pTemStart;
                    pStart++;
                }

                lineStart += srcWidth;
                pStart = lineStart;
            }
            return null;
        }
        #endregion
    }
}
