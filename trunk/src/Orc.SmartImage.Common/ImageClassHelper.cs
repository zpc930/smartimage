using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace Orc.SmartImage
{
	public static class ImageClassHelper
	{
		public static void InitGrayscalePalette(this Image img)
		{
            ColorPalette palette = img.Palette;
			for(int i=0; i<255;i++)
			{
                palette.Entries[i] = Color.FromArgb(i, i, i);
			}

            img.Palette = palette;
		}
	}
}
