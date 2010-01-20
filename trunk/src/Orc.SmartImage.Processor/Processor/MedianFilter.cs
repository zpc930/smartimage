using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;

namespace Orc.SmartImage.Processor
{
	[Processor(Category = "常用", Name = "中值滤波", Introduce = "中值滤波")]
	public class MedianFilter : BaseProcessor
	{
//		private Median m_filter;

		public MedianFilter()
			: base()
		{
//			m_filter = new Median();
		}

        protected override IImage HandleCore(IImage src)
		{
			// Bitmap dst = src.Clone(new Rectangle(0,0,src.Width,src.Height), src.PixelFormat) as Bitmap;
//			m_filter.ApplyInPlace(dst);
			// return dst;
            throw new NotImplementedException();
		}
	}
}
