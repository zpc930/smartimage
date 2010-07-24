using System;
using System.Collections.Generic;
using System.Text;

namespace Orc.SmartImage
{
    using TPixel = Argb32;
    using TCache = System.Int32;
    using TKernel = System.Int32;

    public static partial class ImageArgb32ClassHelper
    {
        #region include "ImageClassHelper_Template.cs"
        #endregion
    }

    public partial class ImageArgb32
    {
        #region include "Image_Template.cs"
        #endregion
    }

    public partial struct Argb32
    {
        #region include "TPixel_Template.cs"
        #endregion
    }
}

