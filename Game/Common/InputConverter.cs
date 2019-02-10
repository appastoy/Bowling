using System;

namespace Bowling.Game.Common
{
	static class InputConverter
	{
		public static bool TryConvertInteger(string input, out int result)
		{
			input = input.TrimEnd(Environment.NewLine.ToCharArray());
			return int.TryParse(input, out result);
		}
	}
}
