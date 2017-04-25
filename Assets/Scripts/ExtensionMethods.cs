
using System.Collections.Generic;
using System.Linq;

public static class ExtensionMethods
{
	public static bool IsNullOrEmpty<T>(this IEnumerable<T> e)
	{
		if (e == null)
			return true;
		if (!e.Any())
		{
			return true;
		}

		return false;
	}
}
