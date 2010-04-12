using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace Orc.SmartImage
{
    public interface IImage
    {
        IImage Clone();
        Bitmap ToBitmap();
    }
}
