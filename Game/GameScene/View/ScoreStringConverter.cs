using Bowling.Game.ScoreBoard;
using System;
using System.Collections.Generic;

namespace Bowling.Game.GameScene.View
{
	static class ScoreStringConverter
	{
		static readonly string StrikeString = "X";
		static readonly string SpareString = "/";
		static readonly string MissingString = "-";

		public static IReadOnlyList<string> ConvertToPinScoreStrings(IReadOnlyList<int> pinScores)
		{
			int sum = 0;
			bool isStrike = false;
			var result = new List<string>(pinScores.Count);
			for (int i = 0; i < pinScores.Count; i++)
			{
				var current = pinScores[i];
				sum += current;
				if (i == 0)
				{
					isStrike = current == ScoreRules.MaxPinScore;
					result.Add(ConvertToPinScoreString(current));
					continue;
				}

				if (i == 1 && !isStrike && sum == ScoreRules.MaxPinScore)
				{
					result.Add(SpareString);
				}
				else
				{
					result.Add(ConvertToPinScoreString(current));
				}
				isStrike = false;
				sum = 0;
			}

			return result;
		}

		internal static object ConvertToPinScoreStrings(object pinScores)
		{
			throw new NotImplementedException();
		}

		public static string ConvertToPinScoreString(int pinScore)
		{
			string result = null;
			if (pinScore == ScoreRules.MaxPinScore)
			{
				result = StrikeString;
			}
			else if (pinScore == 0)
			{
				result = MissingString;
			}
			else
			{
				result = pinScore.ToString();
			}

			return result;
		}

		public static string ConvertToFrameScoreString(IScoreFrame scoreFrame)
		{
			return scoreFrame.IsFrameScoreConfirmed ? scoreFrame.FrameScore.ToString() : string.Empty;
		}
	}
}
