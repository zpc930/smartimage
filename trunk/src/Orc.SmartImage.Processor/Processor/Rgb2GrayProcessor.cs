using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using AForge.Imaging.Filters;

namespace Orc.SmartImage.Processor
{
	/// <summary>
	/// 对图像灰度化
	/// </summary>
    [Processor(Category = "常用", Name = "图像灰度化处理器", Introduce = "图像灰度化处理器")]
	public class Rgb2GrayProcessor :BaseProcessor, IProcessor
	{
		protected Grayscale m_filter;
		protected String m_filterString;

		public Rgb2GrayProcessor():base()
		{
			m_filter = new Grayscale(0.299, 0.587, 0.114);
			m_filterString = "0.299, 0.587, 0.114";
		}

		[Category("参数")]
		[Description("三个浮点数分别为 Red coefficient、Green coefficient与Blue coefficient")]
		public String Filter
		{
			get { return this.m_filterString; }
			set {
				double[] values = this.ParseDoubleArray(3, value, ',');
				this.m_filter = new Grayscale(values[0], values[1], values[2]);
				m_filterString = String.Format("{0}, {1}, {2}", values[0], values[1], values[2]);
			}
		}

		#region IProcessor Members

		[Browsable(false)]
		public override bool Undamaged
		{
			get { return true; }
		}

		#endregion

		protected override Bitmap HandleCore(Bitmap src)
		{
			Bitmap dst = null;
			if (src.PixelFormat != System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
			{
				dst = m_filter.Apply(src);
			}
			else
			{
				dst = src.Clone() as Bitmap;
			}
			return dst;
		}
	}
}
