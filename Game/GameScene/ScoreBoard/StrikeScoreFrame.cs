using System;
using System.Collections.Generic;

namespace Bowling.Game.ScoreBoard
{
	class StrikeScoreFrame : ScoreFrame
	{
		static readonly List<int> pinScores = new List<int>()
		{
			ScoreRules.MaxPinScore
		};

		public override IReadOnlyList<int> PinScores => pinScores;
		public override int AvailableNextMaxPinScore => 0;

		public StrikeScoreFrame(IScoreFrame previousScoreFrame) : base(previousScoreFrame)
		{
			SetComplete();
		}

		public override ScoreFrameState AddPinScore(int pinScore)
		{
			throw new InvalidOperationException();
		}

		public override FrameScorePostProcessor CreateFrameScorePostProcessor()
		{
			return new FrameScorePostProcessor(this, 2);
		}
	}
}
