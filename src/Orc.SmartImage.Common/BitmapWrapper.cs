using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Orc.SmartImage
{
    public class BitmapWrapper : IImage
    {
        public Bitmap Source { get; private set; }

        public BitmapWrapper(Bitmap src)
        {
            if (src == null) throw new ArgumentNullException("src");
            Source = src;
        }

        public IImage Clone()
        {
            return new BitmapWrapper(Source.Clone() as Bitmap);
        }

        public Bitmap ToBitmap()
        {
            return Source;
        }
    }
}
