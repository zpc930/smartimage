﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using AForge.Imaging.Filters;

namespace Orc.SmartImage.Processor
{
	[Processor(Category = "形态学", Name = "Hit and Miss处理器", Introduce = "Hit and Miss处理器")]
	public class HitAndMissProcessor : BaseProcessor, IProcessor
	{
		protected HitAndMiss m_filter = new HitAndMiss(new short[,] {
			{ -1, -1, -1 },
			{  1,  1,  0 },
			{ -1, -1, -1 }
		}, HitAndMiss.Modes.HitAndMiss);

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
			this.m_cacheResults.Add(this.CreateInfoResult("hahaha"));
			return dst;
		}
	}
}
