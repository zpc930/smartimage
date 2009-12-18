using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace Orc.SmartImage.Processor
{
	[Processor(Category = "局部特征算子", Name = "Susan角点标注器", Introduce = "使用Susan角点标注算法，进行角点标注")]
	public class CornersMarkerProcessor : BaseProcessor, IProcessor
	{
		CornersMarker m_filter;
		SusanCornersDetector m_detector;

		public CornersMarkerProcessor()
		{
			m_detector = new SusanCornersDetector();
			m_filter = new CornersMarker(m_detector, Color.Red);
		}

		[Category("参数")]
		[Description("The geometrical threshold sets the maximum number of pixels in USAN area around corner. If potential corner has USAN with more pixels, than it is not a corner.Decreasing this value decreases the amount of detected corners - only sharp corners are detected. Increasing this value increases the amount of detected corners, but also increases amount of flat corners, which may be not corners at all.Default value is set to 18, which is half of maximum amount of pixels in USAN.")]
		public int GeometricalThreshold
		{
			get { return this.m_detector.GeometricalThreshold; }
			set
			{
				this.m_detector.GeometricalThreshold = value;
			}
		}

		[Category("参数")]
		[Description("The brightness difference threshold controls the amount of pixels, which become part of USAN area. If difference between central pixel (nucleus) and surrounding pixel is not higher than difference threshold, then that pixel becomes part of USAN.Increasing this value increases the amount of detected corners.Default value is set to 25.")]
		public int DifferenceThreshold 
		{
			get { return this.m_detector.DifferenceThreshold; }
			set
			{
				this.m_detector.DifferenceThreshold = value;
			}
		}

		#region IProcessor Members

		[Browsable(false)]
		public override bool Undamaged
		{
			get { return false; }
		}

		#endregion

		protected override Bitmap HandleCore(Bitmap src)
		{
			Bitmap dst = src.Clone(new Rectangle(0,0,src.Width,src.Height), src.PixelFormat);
			m_filter.ApplyInPlace(dst);
			return dst;
		}
	}
}
