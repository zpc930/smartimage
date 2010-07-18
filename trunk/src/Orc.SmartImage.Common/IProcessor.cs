using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Orc.SmartImage
{
	public interface IProcessor
	{
		String Name { get; }
		Boolean Enable { get; set; }

        Boolean DisableTempResult { get; set; }

		/// <summary>
		/// 值为 true 表明本图像处理是在原图像自身进行处理，处理后的结果写入原图像
		/// 值为 false 表明只读原图像，进行处理，处理后的结果写入新图像
		/// </summary>
		Boolean Undamaged { get; }

		/// <summary>
		/// 图像处理方法。
		/// </summary>
		/// <param name="src">输入图像</param>
		/// <returns>
		///		处理完毕的图像。
		///		如果 Undamaged 为true，则在原图像上进行处理；如果 Undamaged 为false，则只读原图像的数据，处理得到一副新图像。
		/// </returns>
        IImage Handle(IImage src);

		List<IResult> RequestResults();
	}

	public static class ProcessorClassHelper
	{
		public static double[] ParseDoubleArray(this IProcessor pro, String input, Char splitChar)
		{
			if (input == null) throw new ArgumentNullException("input");
			String[] inputs = input.Split(splitChar);
			double[] outputs = new double[inputs.Length];
			for (int i = 0; i < inputs.Length; i++)
			{
				String inputString = inputs[i].Trim();
				if (String.IsNullOrEmpty(inputString) == false)
				{
					outputs[i] = Double.Parse(inputString);
				}
			}
			return outputs;
		}

		public static double[] ParseDoubleArray(this IProcessor pro, int arrayLength, String input, Char splitChar)
		{
			double[] outputs = pro.ParseDoubleArray(input, splitChar);
			if (outputs.Length != arrayLength)
			{
				throw new NotSupportedException("必需是用','分割的三个浮点数.");
			}
			return outputs;
		}
	}
}
