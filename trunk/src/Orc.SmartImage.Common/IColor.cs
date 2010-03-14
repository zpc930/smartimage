using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Orc.SmartImage
{
    public interface IColor
    {
        unsafe void Copy(Byte* from, Argb32* to);
        unsafe void Copy(Argb32* from, Byte* to);
    }
}
