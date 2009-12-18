using System;
using System.Collections.Generic;
using System.Text;

namespace Orc.SmartImage
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ProcessorAttribute : Attribute
	{
		public String Name { get; set; }
		public String Introduce { get; set; }
		public String Category { get; set; }

		public ProcessorAttribute()
		{
			Name = "";
			Introduce = "";
			Category = "其他";
		}
	}
}
