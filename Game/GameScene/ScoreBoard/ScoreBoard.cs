using System;
using System.Collections.Generic;

namespace Bowling.Game.ScoreBoard
{
	class ScoreBoard
	{
		List<BoardPlayer> boardPlayers = new List<BoardPlayer>();
		int currentPlayerIndex;
		
		public readonly int LaneNumber;

		public BoardPlayer CurrentPlayer => boardPlayers[currentPlayerIndex];
		public bool IsGameCompleted => boardPlayers.TrueForAll(userScore => userScore.IsGameCompleted);

		public IReadOnlyList<BoardPlayer> UserScores => boardPlayers;

		public ScoreBoard(int playerCount)
		{
			var random = new Random();
			LaneNumber = random.Next(1, 20);

			for (int i = 0; i < playerCount; i++)
			{
				AddUser();
			}			
		}

		void AddUser()
		{
			string userName = ((char)('A' + boardPlayers.Count)).ToString();
			boardPlayers.Add(new BoardPlayer(userName));
		}

		public ScoreFrameState ProcessPinScore(int pinScore)
		{
			return CurrentPlayer.ProcessPinScore(pinScore);
		}

		public bool TryMoveNext()
		{
			if (CurrentPlayer.IsFrameCompleted)
			{
				currentPlayerIndex = (currentPlayerIndex + 1) % boardPlayers.Count;
				return true;
			}
			return false;
		}

		public BoardPlayer GetHighestTotalScorePlayer()
		{
			var highestPlayer = boardPlayers[0];
			var highestScore = highestPlayer.GetCurrentFrameScore();

			for (int i = 1; i < boardPlayers.Count; i++)
			{
				var currentPlayer = boardPlayers[i];
				var currentScore = currentPlayer.GetCurrentFrameScore();
				if (currentScore > highestScore)
				{
					highestScore = currentScore;
					highestPlayer = currentPlayer;
				}
			}

			return highestPlayer;
		}
	}
}
