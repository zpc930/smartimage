using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using NUnit.Framework;
using Orc.SmartImage;

namespace Orc.SmartImage.CommonTest
{
    [TestFixture]
    public class UnmanagedImageTest
    {
        [Test]
        public void TestConvertRgb24Image()
        {
            String imgPath0 = "..\\images\\yilagou_1280_720_24.jpg";
            Bitmap map = new Bitmap(imgPath0);
            Color c = map.GetPixel(0, 0);
            Console.WriteLine(c.ToString());
            Rgb24Image r = new Rgb24Image(map);
            Rgb24 cr = r[0, 0];
            Console.WriteLine(cr.ToString());
            Bitmap to = r.ToBitmap();
            Color tc = to.GetPixel(0, 0);
            Console.WriteLine(tc.ToString());
            Assert.AreEqual(c.R, cr.Red);
            Assert.AreEqual(c.G, cr.Green);
            Assert.AreEqual(c.B, cr.Blue);
            Assert.AreEqual(c.R, tc.R);
            Assert.AreEqual(c.G, tc.G);
            Assert.AreEqual(c.B, tc.B);

        }

        [Test]
        public void TestConvertArgb32Image()
        {
            String imgPath0 = "..\\images\\yilagou_1280_720_24.jpg";
            Bitmap map = new Bitmap(imgPath0);
            Color c = map.GetPixel(0, 0);
            Console.WriteLine(c.ToString());
            Argb32Image r = new Argb32Image(map);
            Argb32 cr = r[0, 0];
            Console.WriteLine(cr.ToString());
            Bitmap to = r.ToBitmap();
            Color tc = to.GetPixel(0, 0);
            Console.WriteLine(tc.ToString());
            Assert.AreEqual(c.A, cr.Alpha);
            Assert.AreEqual(c.R, cr.Red);
            Assert.AreEqual(c.G, cr.Green);
            Assert.AreEqual(c.B, cr.Blue);
            Assert.AreEqual(c.R, tc.R);
            Assert.AreEqual(c.G, tc.G);
            Assert.AreEqual(c.B, tc.B);
        }
    }
}
