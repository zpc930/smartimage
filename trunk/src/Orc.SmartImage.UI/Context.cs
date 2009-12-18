using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Reflection;
using ProtoBuf;
using Orc.Util;

namespace Orc.SmartImage.UI
{
	[ProtoContract]
	public class Context
	{
		[ProtoMember(1)]
		public String InputImageFilePath { get; set; }
		[ProtoMember(2, DataFormat=DataFormat.Group)]
		public List<ProcessorContext> ProcessorContextList { get; set; }

		public Context()
		{
			InputImageFilePath = String.Empty;
			ProcessorContextList = new List<ProcessorContext>();
		}

		public List<BaseProcessor> GetProcessors()
		{
			List<BaseProcessor> list = new List<BaseProcessor>();
			foreach (var item in ProcessorContextList)
			{
                try
                {
                    BaseProcessor p = item.CreateProcessor();
                    if (p != null) list.Add(p);
                }
                catch
                {
                }
			}
			return list;
		}

		public static String DefaultPath
		{
			get { return AppDomain.CurrentDomain.BaseDirectory + @"\default.cfg"; }
		}

		public void Save()
		{
			Save(DefaultPath);
		}

		public void Save(String fullPath)
		{
			if (System.IO.File.Exists(DefaultPath)) System.IO.File.Delete(fullPath);
			ObjectSerializer.SaveProtoBuf<Context>(fullPath, this);
		}

		public static Context LoadDefault()
		{
			return Load(DefaultPath);
		}

		public static Context Load(String filePath)
		{
			return ObjectSerializer.ParseProtoBuf<Context>(filePath);
		}
	}

	[ProtoContract]
	public class PropertyParamContext
	{
		[ProtoMember(1)]
		public String PropertyName { get; set; }
		[ProtoMember(2)]
		public String PropertyTypeName { get; set; }
		[ProtoMember(3)]
		public String PropertyValueString { get; set; }

		public Object ToObject()
		{
			Type type = Type.GetType(PropertyTypeName);
			if (type == typeof(String)) return PropertyValueString;
			else if (type == typeof(Int16)) return Int16.Parse(PropertyValueString);
			else if (type == typeof(Int32)) return Int32.Parse(PropertyValueString);
			else if (type == typeof(Int64)) return Int64.Parse(PropertyValueString);
			else if (type == typeof(Byte)) return Byte.Parse(PropertyValueString);
			else if (type == typeof(Char)) return Char.Parse(PropertyValueString);
			else if (type == typeof(Double)) return Double.Parse(PropertyValueString);
			else if (type == typeof(float)) return float.Parse(PropertyValueString);
            else if (type == typeof(Boolean)) return Boolean.Parse(PropertyValueString);
			else return null;
		}

		public void SetValue(BaseProcessor obj)
		{
			PropertyInfo pi = obj.GetType().GetProperty(this.PropertyName);
			pi.SetValue(obj, this.ToObject(),null);
		}

		public static List<PropertyParamContext> Find(BaseProcessor obj)
		{
			List<PropertyParamContext> list = new List<PropertyParamContext>();
			PropertyInfo[] propers = obj.GetType().GetProperties();
			foreach (var pi in propers)
			{
				Object[] finds = pi.GetCustomAttributes(typeof(CategoryAttribute), true);
				Boolean match = false;
				if (finds != null)
				{
					foreach (Object o in finds)
					{
						CategoryAttribute c = (CategoryAttribute)o;
						if (c.Category == "参数")
						{
							match = true;
							break;
						}
					}
				}
				if (match == true)
				{
					list.Add(CreateFrom(obj, pi));
				}
			}
			
			return list;
		}

		public static PropertyParamContext CreateFrom(Object obj, PropertyInfo pi)
		{
			PropertyParamContext cxt = new PropertyParamContext();
			cxt.PropertyName = pi.Name;
			cxt.PropertyTypeName = pi.PropertyType.FullName;
            Object o = pi.GetValue(obj, null);
            if(o!=null)
			    cxt.PropertyValueString = pi.GetValue(obj,null).ToString();
			return cxt;
		}
	}

	[ProtoContract]
	public class ProcessorContext
	{
		[ProtoMember(1)]
		public String TypeName { get; set; }
		[ProtoMember(2)]
		public String AssemblyName { get; set; }
		[ProtoMember(3)]
		public Boolean Enable { get; set; }
		[ProtoMember(4)]
		public List<PropertyParamContext> Params { get; set; }

		public ProcessorContext()
		{
			Params = new List<PropertyParamContext>();
		}

		public BaseProcessor CreateProcessor()
		{
			Object obj = Activator.CreateInstance(AppDomain.CurrentDomain, this.AssemblyName, TypeName).Unwrap();
			BaseProcessor p = obj as BaseProcessor;
			p.Enable = this.Enable;
			foreach (var item in this.Params)
				item.SetValue(p);
			return p;
		}

		public static ProcessorContext CreateFrom(BaseProcessor processor)
		{
			ProcessorContext cxt = new ProcessorContext();
			cxt.Enable = processor.Enable;
			cxt.TypeName = processor.GetType().FullName;
			cxt.AssemblyName = processor.GetType().Assembly.FullName;
			cxt.Params = PropertyParamContext.Find(processor);
			return cxt;
		}
	}

	[ProtoContract]
	public class HistoryContext
	{
		[ProtoMember(1)]
		public HistoryList ImageList { get; set; }
		[ProtoMember(2)]
		public HistoryList CfgList { get; set; }

		public HistoryContext()
		{
			ImageList = new HistoryList();
			CfgList = new HistoryList();
		}

		public static String DefaultPath
		{
			get { return AppDomain.CurrentDomain.BaseDirectory + @"\his.dat"; }
		}

		public void Save()
		{
			Save(DefaultPath);
		}

		public void Save(String fullPath)
		{
			if (System.IO.File.Exists(DefaultPath)) System.IO.File.Delete(fullPath);
			ObjectSerializer.SaveProtoBuf<HistoryContext>(fullPath, this);
		}

		public static HistoryContext LoadDefault()
		{
			return Load(DefaultPath);
		}

		public static HistoryContext Load(String filePath)
		{
			return ObjectSerializer.ParseProtoBuf<HistoryContext>(filePath);
		}
	}

	[ProtoContract]
	public class HistoryList
	{
		[ProtoMember(1)]
		public List<FileHistory> History { get; private set; }
		[ProtoMember(2)]
		public int MaxCount { get; private set; }

		public HistoryList(int maxCount)
		{
			MaxCount = maxCount;
			History = new List<FileHistory>(maxCount);
		}

		public HistoryList()
			: this(10)
		{
		}

		public void Add(String fullPath)
		{
			FileInfo fi = new FileInfo(fullPath);
			this.Remove(fi.FullName);
			if (History.Count > MaxCount)
			{
				History.RemoveAt(0);
			}

			FileHistory his = new FileHistory();
			his.Name = fi.Name;
			his.FullPath = fi.FullName;
			his.LastOpenTime = DateTime.Now;

			History.Add(his);
			History.Sort();
		}

		private void Remove(String fullPath)
		{
			int match = -1;
			for (int i = 0; i < History.Count; i++)
			{
				if (String.Equals( History[i].FullPath, fullPath) == true)
				{
					match = i;
					break;
				}
			}
			if (match > -1) this.History.RemoveAt(match);
		}
	}

	[ProtoContract]
	public class FileHistory : IComparable<FileHistory>
	{
		[ProtoMember(1)]
		public String Name { get; set; }
		[ProtoMember(2)]
		public String FullPath { get; set; }
		[ProtoMember(3)]
		public DateTime LastOpenTime { get; set; }

		#region IComparable<FileHistory> Members

		public int CompareTo(FileHistory other)
		{
			return this.LastOpenTime.CompareTo(other.LastOpenTime);
		}

		#endregion
	}
}
