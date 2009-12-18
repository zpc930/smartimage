namespace Orc.SmartImage.UI.Controls
{
	partial class HistogramShow
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.histogram = new AForge.Controls.Histogram();
			this.SuspendLayout();
			// 
			// histogram
			// 
			this.histogram.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.histogram.Location = new System.Drawing.Point(8, 8);
			this.histogram.Name = "histogram";
			this.histogram.Size = new System.Drawing.Size(480, 376);
			this.histogram.TabIndex = 0;
			this.histogram.Text = "histogram1";
			this.histogram.Values = null;
			// 
			// HistogramShow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.histogram);
			this.Name = "HistogramShow";
			this.Size = new System.Drawing.Size(497, 394);
			this.ResumeLayout(false);

		}

		#endregion

		private AForge.Controls.Histogram histogram;
	}
}
