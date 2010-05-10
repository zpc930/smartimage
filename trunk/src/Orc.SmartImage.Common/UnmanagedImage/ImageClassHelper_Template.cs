using TPixel = System.Byte;
using TCache = System.Int32;
using TKernel = System.Int32;

using System;
using System.Collections.Generic;
using System.Text;

namespace Orc.SmartImage.Hidden
{
    static class ImageClassHelper_Template
    {
        #region mixin

        public unsafe delegate void ActionOnPixel(TPixel* p);
        public unsafe delegate void ActionOnPosition(Int32 row, Int32 column, TPixel* p);
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

        public unsafe static void ForEach(this UnmanagedImage<TPixel> src, ActionOnPosition handler)
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

        #endregion
    }
}

//// 本算法是错误的，只为说明C#模板程序的使用。
//        public unsafe static UnmanagedImage<TPixel> Filter(this UnmanagedImage<TPixel> src, FilterKernel<TKernel> filter)
//        {
//            TKernel* kernel = stackalloc TKernel[filter.Length];

//            Int32 srcWidth = src.Width;
//            Int32 srcHeight = src.Height;
//            Int32 kWidth = filter.Width;
//            Int32 kHeight = filter.Height;

//            TPixel* start = (TPixel*)src.StartIntPtr;
//            TPixel* lineStart = start;
//            TPixel* pStart = start;
//            TPixel* pTemStart = pStart;
//            TPixel* pT;
//            TKernel* pK;

//            for (int c = 0; c < srcWidth; c++)
//            {
//                for (int r = 0; r < srcHeight; r++)
//                {
//                    pTemStart = pStart;
//                    pK = kernel;

//                    Int32 val = 0;
//                    for (int kc = 0; kc < kWidth; kc++)
//                    {
//                        pT = pStart;
//                        for (int kr = 0; kr < kHeight; kr++)
//                        {
//                            val += *pK * *pT;
//                            pT++;
//                            pK++;
//                        }

//                        pStart += srcWidth;
//                    }

//                    pStart = pTemStart;
//                    pStart++;
//                }

//                lineStart += srcWidth;
//                pStart = lineStart;
//            }
//            return null;
//        }
