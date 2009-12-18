using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Drawing;

namespace Orc.SmartImage
{
	public class BaseProcessor : IProcessor
	{
		protected List<IResult> m_cacheResults;
		protected Bitmap m_inputImage;
		protected Bitmap m_outputImage;

		public BaseProcessor()
		{
			Enable = true;
			m_cacheResults = new List<IResult>();
		}

		#region IProcessor Members

		[Category("启用")]
		public Boolean Enable { get; set; }

		[Browsable(false)]
		public virtual bool Undamaged
		{
			get { return true; }
		}

		[Browsable(false)]
		public String Name
		{
			get {
					object[] atts = this.GetType().GetCustomAttributes(typeof(ProcessorAttribute), true);
					ProcessorAttribute att = null;
					if (atts != null && atts.Length > 0)
					{
						att = atts[0] as ProcessorAttribute;
					}
					if (att == null) att = new ProcessorAttribute();
					return String.IsNullOrEmpty(att.Name) ? this.GetType().ToString() : att.Name;
				}
		}

		public Bitmap Handle(Bitmap src)
		{
			m_cacheResults.Clear();
			if (src == null) return null;

			if (Enable == true)
			{
				try
				{
					this.m_inputImage = src;
					this.m_cacheResults.Add(this.CreateInput(this.m_inputImage));
					this.m_outputImage = this.HandleCore(src);
					if (this.m_outputImage != null)
					{
						this.m_cacheResults.Add(this.CreateOutput(this.m_outputImage));
					}
					return this.m_outputImage;
				}
				catch (Exception ex)
				{
					this.m_cacheResults.Add(this.CreateExceptionResult(ex));
					return this.m_outputImage;
				}
				finally
				{
				}
			}
			else return src;
		}

		public virtual List<IResult> RequestResults()
		{
			return this.m_cacheResults; ;
		}

		#endregion

		protected virtual Bitmap HandleCore(Bitmap src)
		{
			return src;
		}
	}
}
