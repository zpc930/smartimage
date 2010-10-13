using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Orc.SmartImage.Processor
{
    using AForge.Imaging;
    using AForge.Imaging.Filters;

	[Processor(Category = "边缘检测", Name = "Canny边缘检测处理器", Introduce = "Canny边缘检测处理器")]
	public class CannyEdgeDetectProcessor : BaseProcessor, IProcessor
	{
        private CannyEdgeDetector m_filter;

		public CannyEdgeDetectProcessor()
			: base()
		{
            m_filter = new CannyEdgeDetector();
		}

        [Category("参数")]
        public int GaussianSize
        {
            get { return m_filter.GaussianSize; }
            set { m_filter.GaussianSize = value; }
        }

        [Category("参数")]
        public double GaussianSigma
        {
            get { return m_filter.GaussianSigma; }
            set { m_filter.GaussianSigma = value; }
        }

        [Category("参数")]
        public byte HighThreshold
        {
            get { return m_filter.HighThreshold; }
            set { m_filter.HighThreshold = value; }
        }

        [Category("参数")]
        public byte LowThreshold
        {
            get { return m_filter.LowThreshold; }
            set { m_filter.LowThreshold = value; }
        }

		#region IProcessor Members

		[Browsable(false)]
		public override bool Undamaged
		{
			get { return true; }
		}

		#endregion

        protected override IImage HandleCore(IImage src)
		{
            Bitmap bmpSrc = src.ToBitmap();
            if (bmpSrc.PixelFormat != System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
                bmpSrc = Grayscale.CommonAlgorithms.BT709.Apply(bmpSrc);
            Bitmap dst = m_filter.Apply(bmpSrc);
            return new BitmapWrapper(dst);
		}
	}
}
