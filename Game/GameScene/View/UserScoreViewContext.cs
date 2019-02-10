using System.Collections.Generic;

namespace Bowling.Game.GameScene.View
{
	class UserScoreViewContext
	{
		readonly IReadOnlyList<ScoreFrameViewContext> ScoreFrames;

		public readonly string Name;
		public readonly string TotalScore;

		public UserScoreViewContext(string name, IReadOnlyList<ScoreFrameViewContext> scoreFrames, string totalScore)
		{
			Name = name;
			ScoreFrames = scoreFrames;
			TotalScore = totalScore;
		}

		public string GetPinScore(int scoreFrameIndex, int pinScoreIndex)
		{
			if (scoreFrameIndex < 0 || scoreFrameIndex >= ScoreFrames.Count || pinScoreIndex < 0)
			{
				return string.Empty;
			}

			var scoreFrame = ScoreFrames[scoreFrameIndex];
			if (pinScoreIndex >= scoreFrame.PinScores.Count)
			{
				return string.Empty;
			}

			return scoreFrame.PinScores[pinScoreIndex];
		}

		public string GetFrameScore(int scoreFrameIndex)
		{
			if (scoreFrameIndex < 0 || scoreFrameIndex >= ScoreFrames.Count)
			{
				return string.Empty;
			}

			var scoreFrame = ScoreFrames[scoreFrameIndex];
			return scoreFrame.FrameScore;
		}
	}
}
