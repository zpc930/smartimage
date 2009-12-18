using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Orc.SmartImage;
using Orc.Util;
using Orc.Util.WinForm;

namespace Orc.SmartImage.UI
{
	public partial class MainForm : Form
	{
		protected BindingList<IProcessor> Processors { get; set; }
		protected BindingList<IResult> Results { get; set; }
		protected Bitmap m_inputBitmap;
		protected Boolean m_handleSuspending;
		protected HistoryContext m_history;
		protected Context m_context;
		protected String m_contextFullPath;
		private Boolean m_contextChanged = true;

		protected String m_title;

		public MainForm()
		{
			this.Processors = new BindingList<IProcessor>();
			this.Results = new BindingList<IResult>();
			InitializeComponent();
			Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
			this.dgvProcessors.UseDefaultMode00();
			this.dgvProcessors.AutoGenerateColumns = false;
			this.dgvOutputs.UseDefaultMode00();
			this.dgvOutputs.AutoGenerateColumns = false;
			BindProcessors();
			BindOutputs();
		}

		public void HideProcessorCommandButtons()
		{
			this.btnAddProcessor.Visible = false;
			this.btnDelProcessor.Visible = false;
			this.btnDown.Visible = false;
			this.btnLoadProcessor.Visible = false;
			this.btnSaveCfgAs.Visible = false;
			this.btnUp.Visible = false;
		}

		private void btnOpen_Click(object sender, EventArgs e)
		{
			this.dgOpenFile.ShowDialog();
		}

		private void dgOpenFile_FileOk(object sender, CancelEventArgs e)
		{
			LoadImage(this.dgOpenFile.FileName);
			this.m_contextChanged = true;
		}
		private void LoadImage(String imgFullPath)
		{
			LoadImage(imgFullPath, false);
		}

		private void LoadImage(String imgFullPath, Boolean check)
		{
			Image img = null;
			try
			{
				img = Image.FromFile(imgFullPath);
			}
			catch
			{
				if (check == true)
					throw;
			}

			if (img == null)
			{
				if (check == true)
				{
					throw new Exception("图像加载失败:" + imgFullPath);
				}
			}
			this.tbInputFilePath.Text = imgFullPath;
			this.boxInput.Image = Image.FromFile(imgFullPath);
			this.m_history.ImageList.Add(imgFullPath);
			m_contextChanged = true;
			m_context.InputImageFilePath = imgFullPath;
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			m_title = this.Text;
			LoadHistory();
			LoadContext(Context.DefaultPath);
		}

		private void LoadHistory()
		{
			this.m_history = HistoryContext.LoadDefault();
			if (m_history == null) m_history = new HistoryContext();
		}

		private void LoadContext(String cfgFullPath)
		{
			LoadContext(cfgFullPath, false);
		}

		private void LoadContext(String cfgFullPath, Boolean check)
		{
			FileInfo fi = new FileInfo(cfgFullPath);
			String fullPath = fi.FullName;
			Context cxt = Context.Load(fullPath);
			if (cxt == null)
			{
				if (check == false)
				{
					cxt = new Context();
				}
				else throw new Exception("cfg文件加载失败: 文件不存在或格式不兼容 - " + cfgFullPath);
			}

			if (m_context != null && m_contextChanged == true)
			{
				this.menuCfg.Hide();
				if( MessageBox.Show("您的当前配置尚未保存，是否进行保存？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					m_contextFullPath = fullPath;
					m_context.Save(m_contextFullPath);
				}
			}

			m_context = cxt;
			m_contextFullPath = fullPath;

			this.Results.Clear();
			this.Processors.Clear();
			this.resultShow.ClearResult();
			this.boxInput.Image = null;
			
			if (String.IsNullOrEmpty(cxt.InputImageFilePath) == false && System.IO.File.Exists(cxt.InputImageFilePath) == true)
			{
				LoadImage(cxt.InputImageFilePath);
			}

			if (cxt.ProcessorContextList != null)
			{
				foreach (var item in cxt.GetProcessors())
				{
					this.Processors.Add(item);
				}
			}

			this.m_history.CfgList.Add(fullPath);
			this.Text = this.m_title + " - " + fi.Name;
			this.m_contextChanged = false;
		}

		private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			MessageBox.Show(e.Exception.Message);
		}

		private void BindProcessors()
		{
			this.dgvProcessors.DataSource = this.Processors;
		}

		private void BindOutputs()
		{
			this.dgvOutputs.DataSource = this.Results;
		}

		private void btnAddProcessor_Click(object sender, EventArgs e)
		{
			ProcessorSelectForm sf = new ProcessorSelectForm();
			sf.ShowDialog();
			if (sf.DialogResult == DialogResult.OK)
			{
				foreach (var item in sf.SelectProcessors)
					this.Processors.Add(item);
			}
			this.m_contextChanged = true;
		}

		private void dgvProcessors_SelectionChanged(object sender, EventArgs e)
		{
			if (m_handleSuspending == true) return;
            RefreshSelectedObjectAndResult();
		}

        private void RefreshSelectedObjectAndResult()
        {
            IList<IProcessor> selects = this.dgvProcessors.GetSelectedItems<IProcessor>();
            if (selects.Count > 0)
                this.propertyGrid.SelectedObject = selects[0];
            RefreshDgvOutput();
        }

		private void btnUp_Click(object sender, EventArgs e)
		{
			DataGridViewSelectedRowCollection rc = this.dgvProcessors.SelectedRows;
			if (rc.Count == 0)
			{
				MessageBox.Show("没有选中项.");
			}
			else if (rc.Count > 1)
			{
				MessageBox.Show("只能选中一项，进行上移操作.");
			}
			else
			{
				DataGridViewRow row = rc[0];
				if (row.Index == 0)
				{
					MessageBox.Show("该项已经是最顶端了.");
				}
				else
				{
					this.SuspendHandleThenInvoke(() =>
						{
							IProcessor p0 = this.Processors[row.Index];
							this.Processors[row.Index] = this.Processors[row.Index - 1];
							this.Processors[row.Index - 1] = p0;
							row.Selected = false;
							this.dgvProcessors.Rows[row.Index - 1].Selected = true;
						});
					this.m_contextChanged = true;
				}
			}
		}

		private void btnDown_Click(object sender, EventArgs e)
		{
			DataGridViewSelectedRowCollection rc = this.dgvProcessors.SelectedRows;
			if (rc.Count == 0)
			{
				MessageBox.Show("没有选中项.");
			}
			else if (rc.Count > 1)
			{
				MessageBox.Show("只能选中一项，进行下移操作.");
			}
			else
			{
				DataGridViewRow row = rc[0];
				if (row.Index == this.dgvProcessors.Rows.Count - 1)
				{
					MessageBox.Show("该项已经是最底端了.");
				}
				else
				{
					this.SuspendHandleThenInvoke(() =>
						{
							IProcessor p0 = this.Processors[row.Index];
							this.Processors[row.Index] = this.Processors[row.Index + 1];
							this.Processors[row.Index + 1] = p0;
							row.Selected = false;
							this.dgvProcessors.Rows[row.Index + 1].Selected = true;
							this.dgvProcessors.ResumeLayout();
						});
					this.m_contextChanged = true;
				}
			}
		}

		private void SuspendHandleThenInvoke(Func0 func)
		{
			this.m_handleSuspending = true;
			this.dgvProcessors.SuspendLayout();
			func();
			this.dgvProcessors.ResumeLayout();
			this.m_handleSuspending = false;
		}

		private void btnRun_Click(object sender, EventArgs e)
		{
			Bitmap map = this.boxInput.Image as Bitmap;
			if (map == null)
			{
				MessageBox.Show("没有输入图像");
				return;
			}
			else if (this.Processors == null || this.Processors.Count == 0)
			{
				MessageBox.Show("没有处理器");
				return;
			}
			this.HandleMap(map);
		}

		private void HandleMap(Bitmap map)
		{
			m_inputBitmap = map;
			this.EnableControls(false);
			ClearDgvOutput();
			this.imageProcessorWorker.RunWorkerAsync();
		}

		private void imageProcessorWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			Bitmap map = m_inputBitmap;
			foreach (var handler in this.Processors)
			{
                if (handler.Undamaged == true)
                    map = handler.Handle(map);
                else
                    map = handler.Handle(map.Clone(new Rectangle(0, 0, map.Width, map.Height), map.PixelFormat));
			}
		}

		private void imageProcessorWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.EnableControls(true);
			MessageBox.Show("处理完毕.");
			RefreshDgvOutput();
		}

		private void RefreshFirstProcessors()
		{
		}

		private void RefreshDgvOutput()
		{
			ClearDgvOutput();
			List<IProcessor> list = this.dgvProcessors.GetSelectedItems<IProcessor>();
			List<IResult> results = null;

			if (list != null && list.Count > 0)
			{
				results = list[0].RequestResults();
			}

			if (results == null) return;

			foreach (var item in results)
				this.Results.Add(item);

            if (this.Results.Count > 0)
            {
                this.dgvOutputs.Rows[0].Selected = false;
                this.dgvOutputs.Rows[this.Results.Count - 1].Selected = true;
            }
		}

		private void ClearDgvOutput()
		{
			this.Results.Clear();
		}

		private void ShowResultItem()
		{
			List<IResult> results = this.dgvOutputs.GetSelectedItems<IResult>();
            if (results != null && results.Count > 0)
            {
                IResult r = results[0];
                this.resultShow.ShowResult(r);
            }
            else
                this.resultShow.ClearResult();
		}

		private void ChearResultItem()
		{
			this.resultShow.ClearResult();
		}

		private void dgvOutputs_SelectionChanged(object sender, EventArgs e)
		{
			ShowResultItem();
		}

		private void dgvOutputs_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			e.Cancel = true;
		}

		private void btnDelProcessor_Click(object sender, EventArgs e)
		{
			List<IProcessor> pList = this.dgvProcessors.GetSelectedItems<IProcessor>();
			if (this.ShowWarningEmptyListMsgBoxAndEnsureDelMsgBox<IProcessor>(pList) == true)
			{
				m_handleSuspending = true;
				this.dgvProcessors.SuspendLayout();
				foreach (var item in pList)
					this.Processors.Remove(item);
				this.dgvProcessors.ResumeLayout();

                m_handleSuspending = false;

                RefreshSelectedObjectAndResult();

                this.m_contextChanged = true;
            }
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Context cxt = GetContext();
			cxt.Save();

			m_history.Save();
		}

		private Context GetContext()
		{
			Context cxt = new Context();
			cxt.InputImageFilePath = this.tbInputFilePath.Text;
			foreach (var item in this.Processors)
			{
				BaseProcessor p = item as BaseProcessor;
				if (p != null)
					cxt.ProcessorContextList.Add(ProcessorContext.CreateFrom(p));
			}
			return cxt;
		}

		private void btnLoadProcessor_Click(object sender, EventArgs e)
		{
			this.dgOpenCfg.ShowDialog();
		}

		private void dgOpenCfg_FileOk(object sender, CancelEventArgs e)
		{
			LoadContext(this.dgOpenCfg.FileName);
		}

		private void btnSaveProcessor_Click(object sender, EventArgs e)
		{
			this.dgSaveCfg.ShowDialog();
		}

		private void dgSaveCfg_FileOk(object sender, CancelEventArgs e)
		{
			Context cxt = GetContext();
			cxt.Save(this.dgSaveCfg.FileName);
			this.m_contextChanged = false;
		}

		private void btnCfgHistory_Click(object sender, EventArgs e)
		{
			LoadHistoryData(menuCfg, this.m_history.CfgList);
			menuCfg.Show(this.btnCfgHistory.PointToScreen(new Point(15,15)));
		}

		private void btnImageHistory_Click(object sender, EventArgs e)
		{
			LoadHistoryData(menuImage, this.m_history.ImageList);
			menuImage.Show(this.btnImageHistory.PointToScreen(new Point(15, 15)));
		}

		private void menuCfg_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			ToolStripItem item = e.ClickedItem;
			this.LoadContext(item.Tag as String, true);
		}

		private void menuImage_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			ToolStripItem item = e.ClickedItem;
			this.LoadImage(item.Tag as String);
		}

		private void LoadHistoryData(ContextMenuStrip menu, HistoryList list)
		{
			menu.Items.Clear();
			for (int i = 0; i < list.History.Count; i++)
			{
				FileHistory h = list.History[list.History.Count - i -1];
				ToolStripMenuItem item = new ToolStripMenuItem();
				item.Text = h.Name;
				item.Tag = h.FullPath;
				menu.Items.Add(item);
			}
		}

		private void propertyGrid_BindingContextChanged(object sender, EventArgs e)
		{
			this.m_contextChanged = true;
		}

		private void dgvProcessors_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 0) this.m_contextChanged = true;
		}

		private void btnSaveCfg_Click(object sender, EventArgs e)
		{
			this.m_context.Save(this.m_contextFullPath);
			this.m_contextChanged = false;
		}

        private void boxInput_DoubleClick(object sender, EventArgs e)
        {
            Image img = this.boxInput.Image;
            Util.ShowImage(img);
        }
	}
}
