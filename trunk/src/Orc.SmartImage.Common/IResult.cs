using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Orc.SmartImage
{
    public interface IResult
    {
        String Name { get; set; }
        Object Result { get; set; }
    }

    public class ProcessorResult : IResult
    {
        public String Name { get; set; }
        public Object Result { get; set; }

        public static ProcessorResult CreateInput(IProcessor pro, IImage src)
        {
            ProcessorResult r = new ProcessorResult();
            r.Result = src == null ? null : src.ToBitmap();
            r.Name = "输入图像";
            return r;
        }

        public static ProcessorResult CreateOutput(IProcessor pro, IImage output)
        {
            ProcessorResult r = new ProcessorResult();
            r.Result = output == null ? null : output.ToBitmap();
            r.Name = "输出图像";
            return r;
        }

        public static ProcessorResult CreateTempResult(IProcessor pro, String name, Bitmap map)
        {
            ProcessorResult r = new ProcessorResult();
            r.Result = map;
            r.Name = name;
            return r;
        }
    }

    public static class ProcessorResultClassHelper
    {
        public static ProcessorResult CreateInput(this IProcessor pro, IImage src)
        {
            return ProcessorResult.CreateInput(pro, src);
        }

        public static ProcessorResult CreateOutput(this IProcessor pro, IImage output)
        {
            return ProcessorResult.CreateOutput(pro, output);
        }

        public static ProcessorResult CreateTempResult(this IProcessor pro, String name, Bitmap map)
        {
            return ProcessorResult.CreateTempResult(pro, name, map);
        }

        public static ProcessorResult CreateInfoResult(this IProcessor pro, String msg)
        {
            return pro.CreateInfoResult("消息", msg);
        }

        public static ProcessorResult CreateExceptionResult(this IProcessor pro, Exception ex)
        {
            return pro.CreateInfoResult("异常", ex.Message + Environment.NewLine + ex.StackTrace);
        }

        public static ProcessorResult CreateInfoResult(this IProcessor pro, String name, String msg)
        {
            ProcessorResult r = new ProcessorResult();
            r.Result = msg;
            r.Name = name;
            return r;
        }
    }
}
