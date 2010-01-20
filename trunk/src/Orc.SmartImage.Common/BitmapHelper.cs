using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Orc.SmartImage
{
    public static class BitmapHelper
    {
        public static IImage ToIImage(this Bitmap src)
        {
            return new Image<Emgu.CV.Structure.Bgra, Byte>(src);
        }
    }
}
