
namespace Bowling.Game.ScoreBoard
{
	static class ScoreHelper
	{
		public static bool IsValidPinScore(int pinScore)
		{
			return pinScore >= 0 && pinScore <= ScoreRules.MaxPinScore;
		}

		public static bool IsValidPinScore(int pinScore, int firstPinScore)
		{
			return pinScore >= 0 && (pinScore + firstPinScore) <= ScoreRules.MaxPinScore;
		}
	}
}
