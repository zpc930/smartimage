using System;
using System.Collections.Generic;
using System.Text;

namespace Orc.SmartImage.UI.Controls
{
	public interface IResultControl
	{
		void ClearResult();
		Boolean ShowResult(IResult result);
	}

	public static class IResultControlClassHelper
	{
		public static T GetResult<T>(this IResultControl control, IResult result) where T : class
		{
			if (result == null || result.Result == null) return null;
			else return result.Result as T;
		}
	}
}
