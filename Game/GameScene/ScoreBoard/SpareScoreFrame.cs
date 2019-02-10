using System;
using System.Collections.Generic;

namespace Bowling.Game.ScoreBoard
{
	class SpareScoreFrame : ScoreFrame
	{
		readonly List<int> pinScores;

		public override IReadOnlyList<int> PinScores => pinScores;
		public override int AvailableNextMaxPinScore => ScoreRules.MaxPinScore - TotalPinScore;

		public SpareScoreFrame(IScoreFrame previousScoreFrame, int firstPinScore) : base(previousScoreFrame)
		{
			if (!ScoreHelper.IsValidPinScore(firstPinScore))
			{
				throw new ArgumentOutOfRangeException(nameof(firstPinScore));
			}

			pinScores = new List<int>(2)
			{
				firstPinScore
			};
		}

		public override ScoreFrameState AddPinScore(int pinScore)
		{
			if (pinScores.Count != 1)
			{
				throw new InvalidOperationException();
			}

			if (!ScoreHelper.IsValidPinScore(pinScore, pinScores[0]))
			{
				throw new ArgumentOutOfRangeException(nameof(pinScore));
			}
			pinScores.Add(pinScore);
			SetComplete();
			if (TotalPinScore == ScoreRules.MaxPinScore)
			{
				return ScoreFrameState.Spare;

			}
			ConfirmFrameScore(0);
			return ScoreFrameState.Open;
		}

		public override FrameScorePostProcessor CreateFrameScorePostProcessor()
		{
			return new FrameScorePostProcessor(this, 1);
		}
	}
}
