using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Orc.SmartImage.UI.Controls
{
	public partial class TextShow : UserControl, IResultControl
	{
		public TextShow()
		{
			InitializeComponent();
		}

		public Boolean ShowText(String txt)
		{
			this.tbContent.Text = txt;
			return txt != null;
		}

		public void ClearResult()
		{
			this.ShowText(null);
		}

		public Boolean ShowResult(IResult result)
		{
			return this.ShowText(this.GetResult<String>(result));
		}
	}
}
