using System;
using System.Collections.Generic;
using System.Text;
using Orc.SmartImage;

namespace Orc.SmartImageLab.PerformanceTest
{
    public class UnmanagedImageTest
    {

        public static void Test()
        {
            ImageArgb32 src = new ImageArgb32(5000, 6000);
            System.Drawing.Bitmap dst = new System.Drawing.Bitmap(5000, 6000, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            CodeTimer.Time("ToBitmapEx", 1, () => { src.ToBitmapEx(dst); });
            CodeTimer.Time("ToBitmap", 1, () => { src.ToBitmap(dst); });
            CodeTimer.Time("ToBitmapEx", 1, () => { src.ToBitmapEx(dst); });
            CodeTimer.Time("ToBitmap", 1, () => { src.ToBitmap(dst); });
            CodeTimer.Time("ToBitmapEx", 1, () => { src.ToBitmapEx(dst); });
            CodeTimer.Time("ToBitmap", 1, () => { src.ToBitmap(dst); });
            CodeTimer.Time("ToBitmapEx", 1, () => { src.ToBitmapEx(dst); });
            CodeTimer.Time("ToBitmap", 1, () => { src.ToBitmap(dst); });
            CodeTimer.Time("ToBitmapEx", 1, () => { src.ToBitmapEx(dst); });
        }
    }
}
