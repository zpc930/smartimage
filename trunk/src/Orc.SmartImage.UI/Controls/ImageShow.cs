using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Orc.SmartImage.UI.Controls
{
	public partial class ImageShow : UserControl, IResultControl
	{
		public ImageShow()
		{
			InitializeComponent();
		}

		public Image Image
		{
			get { return this.boxPicture.Image; }
		}

		private Boolean ShowImage(Image img)
		{
			this.boxPicture.Image = img;
			return img != null;
		}

		public void ClearResult()
		{
			this.ShowImage(null);
		}

		public Boolean ShowResult(IResult result)
		{
			return this.ShowImage(this.GetResult<Image>(result));
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			this.dgSave.ShowDialog();
		}

		private void dgSave_FileOk(object sender, CancelEventArgs e)
		{
			this.Image.Save(this.dgSave.FileName);
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			Clipboard.SetData(DataFormats.Bitmap, Image);
		}

        private void boxPicture_DoubleClick(object sender, EventArgs e)
        {
            Util.ShowImage(this.boxPicture.Image);
        }
	}
}
