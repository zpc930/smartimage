using System;
using System.Collections.Generic;
using System.Text;

namespace Orc.SmartImage
{
    public interface IByteConverter<T>
    {
        unsafe void Copy(Byte* from, ref T to);
        unsafe void Copy(ref T from, Byte* to);

        unsafe void Copy(Byte* from, Argb32* to);
        unsafe void Copy(Argb32* from, Byte* to);
        unsafe void Copy(Rgb24* from, Byte* to);
    }
}
