using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.IO;
using Orc.SmartImage;

namespace Orc.SmartImageLab.PerformanceTest
{
    public class TestLoadImageAndConvert
    {
        public static void Test(String imgName, String filePath)
        {
            using(Bitmap map = new Bitmap(filePath))
            {
                int count = 0;
                Rgb24Image rgb24 = null;
                GrayscaleImage gray = null;
                Bitmap to = null;
                CodeTimer.Time("LoadRgb24Image-" + imgName, 1, () => { rgb24 = new Rgb24Image(map); });
                CodeTimer.Time("ConvertRgb24ToGrayscale-" + imgName, 1, () => { gray = rgb24.ToGrayscaleImage(); });
                CodeTimer.Time("ConvertGrayscaleToBitmap-" + imgName, 1, () => { to = gray.ToBitmap(); });
                CodeTimer.Time("ImageEnumerator-" + imgName, 1, () => 
                {
                    foreach (Rgb24 r in rgb24)
                    {
                        count++;
                    }
                });

                Console.WriteLine(count);
                
                Save(to, filePath + "_grayscale.png");
                Save(rgb24.ToBitmap(), filePath + "_rgb24.png");
                rgb24.Dispose();
                gray.Dispose();
                to.Dispose();
            }
        }

        private static void Save(Bitmap map, String filePath)
        {
            if (File.Exists(filePath)) File.Delete(filePath);
            map.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
        }

        public static void Test()
        {
            String imgPath0 = "..\\images\\yilagou_1280_720_24.jpg";
            String imgPath1 = "..\\images\\xiangfan_5000_6000_24.jpg";
            Test("1280_720_24", imgPath0);
            Test("5000_6000_24", imgPath1);
        }
    }
}
