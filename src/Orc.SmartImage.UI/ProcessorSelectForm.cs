using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Orc.Util;
using Orc.Util.WinForm;
using Orc.SmartImage;

namespace Orc.SmartImage.UI
{
	public partial class ProcessorSelectForm : Form
	{
		public List<IProcessor> SelectProcessors { get; set; }

		protected CategoryItemSelectorMediator ItemSelector;

		public ProcessorSelectForm()
		{
			InitializeComponent();
			ItemSelector = new CategoryItemSelectorMediator();
			SelectProcessors = new List<IProcessor>();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void InitMediator()
		{
			this.ItemSelector.Bind( ProcessorManager.CategoryItems , this.cmbCategory, this.lbSelector, this.tbIntroduce);
		}

		private void ProcessorSelectForm_Load(object sender, EventArgs e)
		{
			InitMediator();
			this.ItemSelector.ListBoxItemDoubleClick += new EventHandler<EventArgs>(ItemSelector_ListBoxItemDoubleClick);
		}

		private void ItemSelector_ListBoxItemDoubleClick(object sender, EventArgs e)
		{
			this.btnSelect.PerformClick();
		}

		private void btnSelect_Click(object sender, EventArgs e)
		{
			SelectProcessors.Clear();
			List<Type> types = ItemSelector.GetSelectValues<Type>();
			foreach (var item in types)
			{
				SelectProcessors.Add(TypeTemplate.Create<IProcessor>(item));
			}
			this.DialogResult = DialogResult.OK;
		}
	}
}
