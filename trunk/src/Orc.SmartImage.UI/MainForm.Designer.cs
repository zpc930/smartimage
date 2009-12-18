namespace Orc.SmartImage.UI
{
	partial class MainForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.boxInput = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnImageHistory = new System.Windows.Forms.Button();
            this.btnOpenRight = new System.Windows.Forms.Button();
            this.btnOpenLeft = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.tbInputFilePath = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnCfgHistory = new System.Windows.Forms.Button();
            this.dgvProcessors = new System.Windows.Forms.DataGridView();
            this.dgvProcessorEnable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvProcessorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnAddProcessor = new System.Windows.Forms.Button();
            this.btnSaveCfg = new System.Windows.Forms.Button();
            this.btnSaveCfgAs = new System.Windows.Forms.Button();
            this.btnDelProcessor = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnLoadProcessor = new System.Windows.Forms.Button();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.plResultItem = new System.Windows.Forms.Panel();
            this.resultShow = new Orc.SmartImage.UI.Controls.ResultShow();
            this.dgvOutputs = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.imageProcessorWorker = new System.ComponentModel.BackgroundWorker();
            this.dgOpenCfg = new System.Windows.Forms.OpenFileDialog();
            this.dgSaveCfg = new System.Windows.Forms.SaveFileDialog();
            this.menuCfg = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuImage = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.boxInput)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcessors)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.plResultItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutputs)).BeginInit();
            this.SuspendLayout();
            // 
            // boxInput
            // 
            this.boxInput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.boxInput.Location = new System.Drawing.Point(8, 16);
            this.boxInput.Name = "boxInput";
            this.boxInput.Size = new System.Drawing.Size(392, 240);
            this.boxInput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.boxInput.TabIndex = 0;
            this.boxInput.TabStop = false;
            this.boxInput.DoubleClick += new System.EventHandler(this.boxInput_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnImageHistory);
            this.groupBox1.Controls.Add(this.btnOpenRight);
            this.groupBox1.Controls.Add(this.btnOpenLeft);
            this.groupBox1.Controls.Add(this.btnOpen);
            this.groupBox1.Controls.Add(this.tbInputFilePath);
            this.groupBox1.Controls.Add(this.boxInput);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(408, 320);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "输入图像";
            // 
            // btnImageHistory
            // 
            this.btnImageHistory.Location = new System.Drawing.Point(384, 288);
            this.btnImageHistory.Name = "btnImageHistory";
            this.btnImageHistory.Size = new System.Drawing.Size(16, 23);
            this.btnImageHistory.TabIndex = 3;
            this.btnImageHistory.Text = "V";
            this.btnImageHistory.UseVisualStyleBackColor = true;
            this.btnImageHistory.Click += new System.EventHandler(this.btnImageHistory_Click);
            // 
            // btnOpenRight
            // 
            this.btnOpenRight.Location = new System.Drawing.Point(352, 288);
            this.btnOpenRight.Name = "btnOpenRight";
            this.btnOpenRight.Size = new System.Drawing.Size(24, 23);
            this.btnOpenRight.TabIndex = 3;
            this.btnOpenRight.Text = "&>";
            this.btnOpenRight.UseVisualStyleBackColor = true;
            // 
            // btnOpenLeft
            // 
            this.btnOpenLeft.Location = new System.Drawing.Point(312, 288);
            this.btnOpenLeft.Name = "btnOpenLeft";
            this.btnOpenLeft.Size = new System.Drawing.Size(24, 23);
            this.btnOpenLeft.TabIndex = 3;
            this.btnOpenLeft.Text = "&<";
            this.btnOpenLeft.UseVisualStyleBackColor = true;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(312, 264);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(91, 23);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "打开文件(&O)";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // tbInputFilePath
            // 
            this.tbInputFilePath.Location = new System.Drawing.Point(8, 264);
            this.tbInputFilePath.Multiline = true;
            this.tbInputFilePath.Name = "tbInputFilePath";
            this.tbInputFilePath.ReadOnly = true;
            this.tbInputFilePath.Size = new System.Drawing.Size(296, 45);
            this.tbInputFilePath.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.splitContainer1);
            this.groupBox2.Location = new System.Drawing.Point(424, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(584, 320);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "处理器";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 17);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnCfgHistory);
            this.splitContainer1.Panel1.Controls.Add(this.dgvProcessors);
            this.splitContainer1.Panel1.Controls.Add(this.btnRun);
            this.splitContainer1.Panel1.Controls.Add(this.btnDown);
            this.splitContainer1.Panel1.Controls.Add(this.btnAddProcessor);
            this.splitContainer1.Panel1.Controls.Add(this.btnSaveCfg);
            this.splitContainer1.Panel1.Controls.Add(this.btnSaveCfgAs);
            this.splitContainer1.Panel1.Controls.Add(this.btnDelProcessor);
            this.splitContainer1.Panel1.Controls.Add(this.btnUp);
            this.splitContainer1.Panel1.Controls.Add(this.btnLoadProcessor);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.propertyGrid);
            this.splitContainer1.Size = new System.Drawing.Size(578, 300);
            this.splitContainer1.SplitterDistance = 316;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnCfgHistory
            // 
            this.btnCfgHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCfgHistory.Location = new System.Drawing.Point(59, 248);
            this.btnCfgHistory.Name = "btnCfgHistory";
            this.btnCfgHistory.Size = new System.Drawing.Size(16, 23);
            this.btnCfgHistory.TabIndex = 3;
            this.btnCfgHistory.Text = "V";
            this.btnCfgHistory.UseVisualStyleBackColor = true;
            this.btnCfgHistory.Click += new System.EventHandler(this.btnCfgHistory_Click);
            // 
            // dgvProcessors
            // 
            this.dgvProcessors.BackgroundColor = System.Drawing.Color.White;
            this.dgvProcessors.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvProcessors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProcessors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvProcessorEnable,
            this.dgvProcessorName});
            this.dgvProcessors.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvProcessors.Location = new System.Drawing.Point(0, 0);
            this.dgvProcessors.Name = "dgvProcessors";
            this.dgvProcessors.RowHeadersVisible = false;
            this.dgvProcessors.RowTemplate.Height = 23;
            this.dgvProcessors.Size = new System.Drawing.Size(316, 240);
            this.dgvProcessors.TabIndex = 0;
            this.dgvProcessors.SelectionChanged += new System.EventHandler(this.dgvProcessors_SelectionChanged);
            this.dgvProcessors.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProcessors_CellContentClick);
            // 
            // dgvProcessorEnable
            // 
            this.dgvProcessorEnable.DataPropertyName = "Enable";
            this.dgvProcessorEnable.HeaderText = "启用";
            this.dgvProcessorEnable.Name = "dgvProcessorEnable";
            // 
            // dgvProcessorName
            // 
            this.dgvProcessorName.DataPropertyName = "Name";
            this.dgvProcessorName.FillWeight = 500F;
            this.dgvProcessorName.HeaderText = "处理器名称";
            this.dgvProcessorName.Name = "dgvProcessorName";
            this.dgvProcessorName.ReadOnly = true;
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Location = new System.Drawing.Point(238, 248);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 48);
            this.btnRun.TabIndex = 3;
            this.btnRun.Text = "运行(&R)";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDown.Location = new System.Drawing.Point(157, 272);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(75, 23);
            this.btnDown.TabIndex = 8;
            this.btnDown.Text = "下移(&D)";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnAddProcessor
            // 
            this.btnAddProcessor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddProcessor.Location = new System.Drawing.Point(80, 248);
            this.btnAddProcessor.Name = "btnAddProcessor";
            this.btnAddProcessor.Size = new System.Drawing.Size(75, 23);
            this.btnAddProcessor.TabIndex = 1;
            this.btnAddProcessor.Text = "添加(&+)";
            this.btnAddProcessor.UseVisualStyleBackColor = true;
            this.btnAddProcessor.Click += new System.EventHandler(this.btnAddProcessor_Click);
            // 
            // btnSaveCfg
            // 
            this.btnSaveCfg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveCfg.Location = new System.Drawing.Point(40, 272);
            this.btnSaveCfg.Name = "btnSaveCfg";
            this.btnSaveCfg.Size = new System.Drawing.Size(40, 23);
            this.btnSaveCfg.TabIndex = 5;
            this.btnSaveCfg.Text = "保存(&S)";
            this.btnSaveCfg.UseVisualStyleBackColor = true;
            this.btnSaveCfg.Click += new System.EventHandler(this.btnSaveCfg_Click);
            // 
            // btnSaveCfgAs
            // 
            this.btnSaveCfgAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveCfgAs.Location = new System.Drawing.Point(0, 272);
            this.btnSaveCfgAs.Name = "btnSaveCfgAs";
            this.btnSaveCfgAs.Size = new System.Drawing.Size(40, 23);
            this.btnSaveCfgAs.TabIndex = 5;
            this.btnSaveCfgAs.Text = "另存";
            this.btnSaveCfgAs.UseVisualStyleBackColor = true;
            this.btnSaveCfgAs.Click += new System.EventHandler(this.btnSaveProcessor_Click);
            // 
            // btnDelProcessor
            // 
            this.btnDelProcessor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelProcessor.Location = new System.Drawing.Point(80, 272);
            this.btnDelProcessor.Name = "btnDelProcessor";
            this.btnDelProcessor.Size = new System.Drawing.Size(75, 23);
            this.btnDelProcessor.TabIndex = 2;
            this.btnDelProcessor.Text = "删除(&-)";
            this.btnDelProcessor.UseVisualStyleBackColor = true;
            this.btnDelProcessor.Click += new System.EventHandler(this.btnDelProcessor_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUp.Location = new System.Drawing.Point(157, 248);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(75, 23);
            this.btnUp.TabIndex = 7;
            this.btnUp.Text = "上移(&U)";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnLoadProcessor
            // 
            this.btnLoadProcessor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoadProcessor.Location = new System.Drawing.Point(0, 248);
            this.btnLoadProcessor.Name = "btnLoadProcessor";
            this.btnLoadProcessor.Size = new System.Drawing.Size(56, 23);
            this.btnLoadProcessor.TabIndex = 6;
            this.btnLoadProcessor.Text = "加载(&L)";
            this.btnLoadProcessor.UseVisualStyleBackColor = true;
            this.btnLoadProcessor.Click += new System.EventHandler(this.btnLoadProcessor_Click);
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(258, 300);
            this.propertyGrid.TabIndex = 4;
            this.propertyGrid.BindingContextChanged += new System.EventHandler(this.propertyGrid_BindingContextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.plResultItem);
            this.groupBox3.Controls.Add(this.dgvOutputs);
            this.groupBox3.Location = new System.Drawing.Point(8, 336);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1000, 354);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "输出";
            // 
            // plResultItem
            // 
            this.plResultItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.plResultItem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.plResultItem.Controls.Add(this.resultShow);
            this.plResultItem.Location = new System.Drawing.Point(416, 24);
            this.plResultItem.Name = "plResultItem";
            this.plResultItem.Size = new System.Drawing.Size(576, 322);
            this.plResultItem.TabIndex = 1;
            // 
            // resultShow
            // 
            this.resultShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultShow.Location = new System.Drawing.Point(0, 0);
            this.resultShow.Name = "resultShow";
            this.resultShow.Size = new System.Drawing.Size(572, 318);
            this.resultShow.TabIndex = 0;
            // 
            // dgvOutputs
            // 
            this.dgvOutputs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvOutputs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvOutputs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOutputs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dgvOutputs.Location = new System.Drawing.Point(8, 24);
            this.dgvOutputs.Name = "dgvOutputs";
            this.dgvOutputs.RowTemplate.Height = 23;
            this.dgvOutputs.Size = new System.Drawing.Size(400, 322);
            this.dgvOutputs.TabIndex = 0;
            this.dgvOutputs.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvOutputs_DataError);
            this.dgvOutputs.SelectionChanged += new System.EventHandler(this.dgvOutputs_SelectionChanged);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Name";
            this.Column1.HeaderText = "项名称";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // dgOpenFile
            // 
            this.dgOpenFile.Filter = "图像文件|*.bmp;*.jpg;.jpeg;*.gif;*.png";
            this.dgOpenFile.FileOk += new System.ComponentModel.CancelEventHandler(this.dgOpenFile_FileOk);
            // 
            // imageProcessorWorker
            // 
            this.imageProcessorWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.imageProcessorWorker_DoWork);
            this.imageProcessorWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.imageProcessorWorker_RunWorkerCompleted);
            // 
            // dgOpenCfg
            // 
            this.dgOpenCfg.Filter = "cfg 文件|*.cfg";
            this.dgOpenCfg.FileOk += new System.ComponentModel.CancelEventHandler(this.dgOpenCfg_FileOk);
            // 
            // dgSaveCfg
            // 
            this.dgSaveCfg.Filter = "cfg 文件|*.cfg";
            this.dgSaveCfg.FileOk += new System.ComponentModel.CancelEventHandler(this.dgSaveCfg_FileOk);
            // 
            // menuCfg
            // 
            this.menuCfg.Name = "menu";
            this.menuCfg.Size = new System.Drawing.Size(61, 4);
            this.menuCfg.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuCfg_ItemClicked);
            // 
            // menuImage
            // 
            this.menuImage.Name = "menuImage";
            this.menuImage.Size = new System.Drawing.Size(61, 4);
            this.menuImage.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuImage_ItemClicked);
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnRun;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 700);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1022, 734);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "阿牛图像实验室(by xiaotie@gmail.com QQ:271630067)";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.boxInput)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcessors)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.plResultItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutputs)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox boxInput;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.TextBox tbInputFilePath;
		private System.Windows.Forms.OpenFileDialog dgOpenFile;
		private System.Windows.Forms.DataGridView dgvProcessors;
		private System.Windows.Forms.DataGridView dgvOutputs;
		private System.Windows.Forms.Button btnRun;
		private System.Windows.Forms.Button btnDelProcessor;
		private System.Windows.Forms.Button btnAddProcessor;
		private System.Windows.Forms.PropertyGrid propertyGrid;
		private System.Windows.Forms.Panel plResultItem;
		private System.Windows.Forms.Button btnLoadProcessor;
		private System.Windows.Forms.Button btnSaveCfgAs;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.Button btnUp;
		private System.ComponentModel.BackgroundWorker imageProcessorWorker;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private Orc.SmartImage.UI.Controls.ResultShow resultShow;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dgvProcessorEnable;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvProcessorName;
		private System.Windows.Forms.OpenFileDialog dgOpenCfg;
		private System.Windows.Forms.SaveFileDialog dgSaveCfg;
		private System.Windows.Forms.Button btnOpenLeft;
		private System.Windows.Forms.Button btnOpenRight;
		private System.Windows.Forms.Button btnImageHistory;
		private System.Windows.Forms.Button btnCfgHistory;
		private System.Windows.Forms.ContextMenuStrip menuCfg;
		private System.Windows.Forms.ContextMenuStrip menuImage;
		private System.Windows.Forms.Button btnSaveCfg;
	}
}

