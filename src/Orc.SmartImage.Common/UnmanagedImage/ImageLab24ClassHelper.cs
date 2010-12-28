using System;
using System.Collections.Generic;
using System.Text;

namespace Orc.SmartImage
{
    using TPixel = Orc.SmartImage.Lab24;
    using TCache = Orc.SmartImage.Lab24;
    using TKernel = System.Int32;
    using TImage = Orc.SmartImage.ImageLab24;

    public partial struct Lab24
    {
        #region include "TPixel_Template.cs"
        #endregion
    }

    public static partial class ImageLab24ClassHelper
    {
        #region include "ImageClassHelper_Template.cs"
        #endregion
    }

    public partial class ImageLab24
    {
        #region include "Image_Template.cs"
        #endregion
    }
}
