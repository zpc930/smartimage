using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Orc.SmartImageLab.PerformanceTest
{
    public class Image
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class Bitmap : Image
    {
        public int[] Data;

        public Bitmap(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            Data = new int[width*height];
        }

        public void FillCore(int index, int value)
        {
            Data[index] = value;
        }

        public void Fill(int value)
        {
            int height = Height;
            int width = Width;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Data[y * width + x] = value;
                }
            }
        }

        public void FillEx(int value)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Data[y * Width + x] = value;
                }
            }
        }

        public static void Test()
        {
            Bitmap img = new Bitmap(5000, 6000);
            CodeTimer.Time("Fill", 1, () => { img.Fill(1); });
            CodeTimer.Time("FillEx", 1, () => { img.FillEx(2); });
            CodeTimer.Time("Fill", 1, () => { img.Fill(1); });
            CodeTimer.Time("FillEx", 1, () => { img.FillEx(2); });
            CodeTimer.Time("Fill", 1, () => { img.Fill(1); });
            CodeTimer.Time("FillEx", 1, () => { img.FillEx(2); });
            CodeTimer.Time("Fill", 1, () => { img.Fill(1); });
            CodeTimer.Time("FillEx", 1, () => { img.FillEx(2); });
        }
    }

    public class UnmanagedBitmap : Image
    {
        public IntPtr Data;

        public UnmanagedBitmap(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            Data = Marshal.AllocHGlobal(sizeof(int) * width * height);
        }

        public unsafe void Fill(int value)
        {
            int height = Height;
            int width = Width;
            int* p = (int*)Data;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    *p = value;
                    p++;
                }
            }
        }

        public unsafe void FillEx(int value)
        {
            int* p = (int*)Data;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    *p = value;
                    p++;
                }
            }
        }

        public static void Test()
        {
            UnmanagedBitmap img = new UnmanagedBitmap(5000, 6000);
            CodeTimer.Time("FillEx", 1, () => { img.FillEx(2); });
            CodeTimer.Time("Fill", 1, () => { img.Fill(1); });
            CodeTimer.Time("FillEx", 1, () => { img.FillEx(2); });
            CodeTimer.Time("Fill", 1, () => { img.Fill(1); });
            CodeTimer.Time("FillEx", 1, () => { img.FillEx(2); });
            CodeTimer.Time("Fill", 1, () => { img.Fill(1); });
            CodeTimer.Time("FillEx", 1, () => { img.FillEx(2); });
            CodeTimer.Time("Fill", 1, () => { img.Fill(1); });
            CodeTimer.Time("FillEx", 1, () => { img.FillEx(2); });
        }
    }
}
