using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Orc.SmartImage;
using Orc.SmartImage.UI;
using Orc.SmartImage.Processor;

namespace Orc.SmartImage.Lab
{
	public partial class MainForm : Orc.SmartImage.UI.MainForm
	{
		public MainForm():base()
		{
			InitializeComponent();
			InitProcessors();
		}

		private void InitProcessors()
		{
            //ProcessorManager.Add(typeof(CannyEdgeDetectProcessor));
            //ProcessorManager.Add(typeof(HitAndMissProcessor));
            //ProcessorManager.Add(typeof(Rgb2GrayProcessor));
            //ProcessorManager.Add(typeof(SimpleHoughLineProcessor));
            //ProcessorManager.Add(typeof(CornersMarkerProcessor));
            //ProcessorManager.Add(typeof(MedianFilter));
		}
	}
}
