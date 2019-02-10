using System;
using System.Collections.Generic;
using System.Linq;

namespace Bowling.Game.ScoreBoard
{
	abstract class ScoreFrame : IFrameScoreConfirmable, IScoreFrame
	{
		readonly IScoreFrame previousScoreFrame;

		public int FrameScore { get; private set; }
		public bool IsCompleted { get; private set; }
		public bool IsFrameScoreConfirmed { get; private set; }
		public abstract IReadOnlyList<int> PinScores { get; }
		public virtual int TotalPinScore => PinScores.Sum();
		public abstract int AvailableNextMaxPinScore { get; }

		public ScoreFrame(IScoreFrame previousScoreFrame)
		{
			this.previousScoreFrame = previousScoreFrame;
		}

		public abstract ScoreFrameState AddPinScore(int pinScore);
		public abstract FrameScorePostProcessor CreateFrameScorePostProcessor();

		public void SetComplete()
		{
			IsCompleted = true;
		}

		public void ConfirmFrameScore(int bonusScore)
		{
			if (IsFrameScoreConfirmed)
			{
				throw new InvalidOperationException();
			}
			
			FrameScore = TotalPinScore + bonusScore;
			if (previousScoreFrame != null)
			{
				FrameScore += previousScoreFrame.FrameScore;
			}
			IsFrameScoreConfirmed = true;
		}
	}
}
