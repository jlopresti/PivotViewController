using System;

namespace pagedview
{
	public static class MathEx
	{
		public static int Mod(int a, int b)
		{
			if (a < 0)
				return b + (a % b);
			else
				return a % b;
		}
	}
}

