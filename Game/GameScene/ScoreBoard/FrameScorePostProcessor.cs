using System;

namespace Bowling.Game.ScoreBoard
{
	class FrameScorePostProcessor
	{
		readonly IFrameScoreConfirmable frameScoreConfirmable;
		int bonusScore;
		int remainBonusCount;

		public bool IsCompleted => remainBonusCount <= 0;

		public FrameScorePostProcessor(IFrameScoreConfirmable frameScoreConfirmable, int bonusCount)
		{
			if (bonusCount <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(bonusCount));
			}

			this.frameScoreConfirmable = frameScoreConfirmable ?? throw new ArgumentNullException(nameof(frameScoreConfirmable));
			remainBonusCount = bonusCount;
		}

		public void Process(int pinScore)
		{
			if (remainBonusCount <= 0)
			{
				throw new InvalidOperationException();
			}

			if (!ScoreHelper.IsValidPinScore(pinScore))
			{
				throw new ArgumentOutOfRangeException(nameof(pinScore));
			}

			bonusScore += pinScore;
			remainBonusCount--;
			if (remainBonusCount <= 0)
			{
				frameScoreConfirmable.ConfirmFrameScore(bonusScore);
			}
		}
	}
}
