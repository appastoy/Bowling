using System;
using System.Collections.Generic;

namespace Bowling.Game.GameScene.View
{
	class GameSceneViewContext
	{
		public readonly int LaneNumber;
		public readonly IReadOnlyList<UserScoreViewContext> UserScores;
		public readonly string CurrentPlayerName;
		public readonly int AvailableNextMaxPinScore;
		public readonly bool IsGameCompleted;

		public GameSceneViewContext(int laneNumber, IReadOnlyList<UserScoreViewContext> userScores, string currentPlayerName, int availableNextMaxPinScore, bool isGameCompleted)
		{
			LaneNumber = laneNumber;
			UserScores = userScores ?? throw new ArgumentNullException(nameof(userScores));
			CurrentPlayerName = currentPlayerName ?? throw new ArgumentNullException(nameof(currentPlayerName));
			AvailableNextMaxPinScore = availableNextMaxPinScore;
			IsGameCompleted = isGameCompleted;
		}
	}
}
