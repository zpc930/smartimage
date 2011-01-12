using System;
using System.Collections.Generic;
using System.Text;

namespace Orc.SmartImage
{
    public interface IColorConverter
    {
        unsafe void Copy(Rgb24* from, void* to, int length);
        unsafe void Copy(Argb32* from, void* to, int length);
        unsafe void Copy(Byte* from, void* to, int length);
    }
}
