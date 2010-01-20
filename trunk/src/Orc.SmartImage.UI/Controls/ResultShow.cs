using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Orc.SmartImage.UI.Controls
{
	public partial class ResultShow : UserControl, IResultControl
	{
		public ResultShow()
		{
			InitializeComponent();
			this.AddControl(new ImageShow());
			this.AddControl(new TextShow());
		}

		private void ResultShow_Load(object sender, EventArgs e)
		{
		}

		private void AddControl(Control c)
		{
			this.SuspendLayout();
			c.Dock = DockStyle.Fill;
			c.Location = new Point(0, 0);
			c.Size = this.Size;
			this.Controls.Add(c);
			this.ResumeLayout(false);
		}

		public Boolean ShowResult(IResult result)
		{
			this.ClearResult();
			if (result == null) return false;
			Object obj = result.Result;
			if (obj == null) return false;

			foreach (Control c in this.Controls)
			{
				IResultControl rc = c as IResultControl;
				if (rc != null)
				{
					if (rc.ShowResult(result) == true)
					{
						c.Visible = true;
						return true;
					}
				}
			}

			return false;
		}

		public void ClearResult()
		{
			foreach (Control c in this.Controls)
			{
				IResultControl rc = c as IResultControl;
				if (rc != null)
				{
					rc.ClearResult();
				}
				c.Visible = false;
			}
		}
	}
}
