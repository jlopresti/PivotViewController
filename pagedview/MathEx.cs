using System;

namespace pagedview
{
	public static class MathEx
	{
		public static int Mod(int a, int b)
		{
			var m = (a % b);
			if (m < 0)
				return b + m;
			else
				return m;
		}
	}
}

