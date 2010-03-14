using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Orc.SmartImage
{
    public class GrayscaleImage : ImageU8
    {
        public GrayscaleImage(Int32 width, Int32 height)
            : base(width, height)
        {
        }

        public GrayscaleImage(Bitmap map)
            : base(map)
        {
        }

    }
}
