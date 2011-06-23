using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Orc.Util;

namespace Orc.SmartImage
{
    public class ProcessorManager
    {
        public static List<CategoryItem> CategoryItems { get; private set; }

        static ProcessorManager()
        {
            CategoryItems = new List<CategoryItem>();
        }

        public static void Add(Type type)
        {
            Type rootType = typeof(BaseProcessor);
            if (type.IsSubclassOf(rootType) == false)
            {
                throw new NotSupportedException("type 必须继承自 " + rootType.ToString());
            }

            object[] atts = type.GetCustomAttributes(typeof(ProcessorAttribute), true);
            if (atts == null || atts.Length == 0)
            {
                AddCore(type, null);
            }
            else
            {
                ProcessorAttribute att = atts[0] as ProcessorAttribute;
                AddCore(type, att);
            }
        }

        private static void AddCore(Type type, ProcessorAttribute att)
        {
            if (att == null)
            {
                att = new ProcessorAttribute();
            }

            CategoryItem item = new CategoryItem();
            item.Name = String.IsNullOrEmpty(att.Name) ? type.ToString() : att.Name;
            item.Introduce = String.IsNullOrEmpty(att.Introduce) ? type.ToString() : att.Introduce;
            item.Category = att.Category;
            item.Value = type;
            CategoryItems.Add(item);
        }

        public static void LoadPlugin()
        {
            //TODO: Load plugin from plugin folder
        }
    }
}
