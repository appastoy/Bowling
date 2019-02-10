using System.Collections.Generic;

namespace Bowling.Game.ScoreBoard
{
	interface IScoreFrame
	{
		int FrameScore { get; }
		bool IsFrameScoreConfirmed { get; }
		IReadOnlyList<int> PinScores { get; }
		int AvailableNextMaxPinScore { get; }
	}
}
