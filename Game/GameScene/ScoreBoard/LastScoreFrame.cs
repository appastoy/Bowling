using System;
using System.Collections.Generic;
using System.Linq;

namespace Bowling.Game.ScoreBoard
{
	class LastScoreFrame : ScoreFrame
	{
		readonly List<int> pinScores;

		public override IReadOnlyList<int> PinScores => pinScores;
		public override int TotalPinScore => pinScores[0] == ScoreRules.MaxPinScore ? pinScores[0] : pinScores[0] + pinScores[1];
		public override int AvailableNextMaxPinScore => ScoreRules.MaxPinScore - (pinScores.Sum() % ScoreRules.MaxPinScore);

		public LastScoreFrame(IScoreFrame previousScoreFrame, int firstPinScore) : base(previousScoreFrame)
		{
			if (!ScoreHelper.IsValidPinScore(firstPinScore))
			{
				throw new ArgumentOutOfRangeException(nameof(firstPinScore));
			}

			pinScores = new List<int>(3)
			{
				firstPinScore
			};
		}

		public override ScoreFrameState AddPinScore(int pinScore)
		{
			if (pinScores.Count < 1 || pinScores.Count > 2)
			{
				throw new InvalidOperationException();
			}

			if (!ScoreHelper.IsValidPinScore(pinScore))
			{
				throw new ArgumentOutOfRangeException(nameof(pinScore));
			}

			if (pinScores.Count == 1)
			{
				if (pinScores[0] == ScoreRules.MaxPinScore)
				{
					pinScores.Add(pinScore);
					return ScoreFrameState.Bonus;
				}
				if (!ScoreHelper.IsValidPinScore(pinScore, pinScores[0]))
				{
					throw new ArgumentOutOfRangeException(nameof(pinScore));
				}
				pinScores.Add(pinScore);
				if (TotalPinScore == ScoreRules.MaxPinScore)
				{
					return ScoreFrameState.Spare;
				}
				ConfirmFrameScore(0);
				SetComplete();
				return ScoreFrameState.Open;
			}
			else
			{
				pinScores.Add(pinScore);
				SetComplete();
				return ScoreFrameState.Bonus;
			}
		}

		public override FrameScorePostProcessor CreateFrameScorePostProcessor()
		{
			return new FrameScorePostProcessor(this, 3 - pinScores.Count);
		}
	}
}
