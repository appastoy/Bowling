using System.Collections.Generic;

namespace Bowling.Game.GameScene.View
{
	class ScoreFrameViewContext
	{
		public IReadOnlyList<string> PinScores { get; private set; }
		public string FrameScore { get; }
		
		public ScoreFrameViewContext(IReadOnlyList<string> pinScores, string frameScore)
		{
			PinScores = pinScores;
			FrameScore = frameScore;
		}
	}
}
