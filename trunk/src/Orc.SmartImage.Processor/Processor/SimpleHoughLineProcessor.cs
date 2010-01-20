using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;

namespace Orc.SmartImage.Processor
{
	[Processor(Category = "常用", Name = "霍夫变换处理器", Introduce = "霍夫变换处理器")]
	public class SimpleHoughLineProcessor : BaseProcessor, IProcessor
	{
		// protected HoughLineTransformation m_filter;

		public SimpleHoughLineProcessor()
			: base()
		{
			StepsPerDegree = 1;
			LocalPeakRadius = 4;
		}

		[Category("参数")]
		[Description("The value defines quality of Hough line transformation and its ability to detect lines' slope precisely.Default value is set to 1. Minimum value is 1. Maximum value is 10.")]
		public int StepsPerDegree
		{
			get;
			set;
		}

		[Category("参数")]
		[Description("The value determines radius around a map's value, which is analyzed to determine if the map's value is a local maximum in specified area.Default value is set to 4. Minimum value is 1. Maximum value is 10.")]
		public int LocalPeakRadius
		{
			get;
			set;
		}

		[Browsable(false)]
		public override bool Undamaged
		{
			get { return true; }
		}

        protected override IImage HandleCore(IImage src)
		{
            throw new NotImplementedException();
            //IImage dst = null;
            //if (src.PixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
            //{
            //    m_filter = new HoughLineTransformation();
				
            //    if (StepsPerDegree > 10) StepsPerDegree = 10;
            //    else if (StepsPerDegree < 1) StepsPerDegree = 1;

            //    if (LocalPeakRadius > 10) LocalPeakRadius = 10;
            //    else if (LocalPeakRadius < 1) LocalPeakRadius = 1;

            //    m_filter.StepsPerDegree = this.StepsPerDegree;
            //    m_filter.LocalPeakRadius = this.LocalPeakRadius;

            //    m_filter.ProcessImage(src);
            //    WriteHoughLine();
            //    dst = m_filter.ToBitmap();
            //}
            //else
            //{
            //    dst = src.Clone() as Bitmap;
            //}
            //IImage dst;
		}

		private void WriteHoughLine()
		{
            //IResult r = this.CreateInfoResult("HoughLines", m_filter.GetHoughLinesString());
            //this.m_cacheResults.Add(r);
		}
	}
}
