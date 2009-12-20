namespace Orc.SmartImage.UI.Controls
{
	partial class ImageShow
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
            this.boxPicture = new System.Windows.Forms.PictureBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgSave = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.boxPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // boxPicture
            // 
            this.boxPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.boxPicture.Location = new System.Drawing.Point(0, 0);
            this.boxPicture.Name = "boxPicture";
            this.boxPicture.Size = new System.Drawing.Size(492, 392);
            this.boxPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.boxPicture.TabIndex = 0;
            this.boxPicture.TabStop = false;
            this.boxPicture.DoubleClick += new System.EventHandler(this.boxPicture_DoubleClick);
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Location = new System.Drawing.Point(336, 400);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 1;
            this.btnCopy.Text = "复制";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Visible = false;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(416, 400);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "另存为...";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgSave
            // 
            this.dgSave.Filter = "图像文件|*.bmp;*.jpg;.jpeg;*.gif;*.png";
            this.dgSave.FileOk += new System.ComponentModel.CancelEventHandler(this.dgSave_FileOk);
            // 
            // ImageShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.boxPicture);
            this.Name = "ImageShow";
            this.Size = new System.Drawing.Size(492, 428);
            ((System.ComponentModel.ISupportInitialize)(this.boxPicture)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox boxPicture;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.SaveFileDialog dgSave;
	}
}
