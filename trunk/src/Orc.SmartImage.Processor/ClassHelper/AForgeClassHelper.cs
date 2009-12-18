using System;
using System.Collections.Generic;
using System.Text;
using AForge.Math;
using AForge.Imaging;

namespace Orc.SmartImage
{
	public static class HoughLineTransformationClassHelper
	{
		public static String GetHoughLinesString(this HoughLineTransformation trans, double minRelativeIntensity)
		{
			HoughLine[] lines = trans.GetLinesByRelativeIntensity(minRelativeIntensity);
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("Intensity\tRadius\tTheta");
			foreach (var item in lines)
			{
				sb.AppendLine(String.Format("{0}\t\t{1}\t{2}", item.Intensity, item.Radius, item.Theta));
			}
			return sb.ToString();
		}

		public static String GetHoughLinesString(this HoughLineTransformation trans)
		{
			return trans.GetHoughLinesString(0.1);
		}
	}

	public static class HistogramClassHelper
	{
		public static Range GetMaxLengthContinueRange(this Histogram hg, int maxContinueBreakCount)
		{
			return hg.GetMaxLengthContinueRange(1, maxContinueBreakCount);
		}

		public static Range GetMaxLengthContinueRange(this Histogram hg, int minValue, int maxContinueBreakCount)
		{
			Range max = new Range();
			Range tmp = new Range(-1, -1);
			int[] values = hg.Values;

			int breakCount = 0;
			for (int i = 0; i < values.Length; i++)
			{
				int value = values[i];

				if (value < minValue)
				{
					breakCount++;
					if (tmp.Min > -1 && breakCount > maxContinueBreakCount)
					{
						if (tmp.Distance >= max.Distance)
						{
							max = tmp;
						}
						tmp = new Range(-1, -1);
					}
				}
				else
				{
					breakCount = 0;
					int x = values[i];

					if (tmp.Min == -1)
					{
						tmp.Min = i;
					}
					tmp.Max = i;
				}
			}

			if (tmp.Min > -1 && tmp.Distance >= max.Distance)
				max = tmp;

			return max;
		}
	}
}
