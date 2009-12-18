using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Orc.SmartImage.UI.Controls
{
	public partial class HistogramShow : UserControl, IResultControl
	{
		public HistogramShow()
		{
			InitializeComponent();
		}

		#region IResultControl Members

		public void ClearResult()
		{
			this.histogram.Values = null;
		}

		public bool ShowResult(IResult result)
		{
			return this.ShowValues(this.GetResult<int[]>(result));
		}

		#endregion

		private Boolean ShowValues(int[] data)
		{
			this.histogram.Values = data;
			return data != null;
		}
	}
}
