using Bowling.Game.GameScene.View;
using Bowling.Game.ScoreBoard;
using System;
using System.Linq;

namespace Bowling.Game.GameScene.Presenter
{
	class GameScenePresenter
	{
		readonly GameSceneView gameSceneView;

		public Action<int> OnInputPinScore
		{
			get => gameSceneView.OnInputPinScore;
			set => gameSceneView.OnInputPinScore = value;
		}

		public Action OnGoToTitle
		{
			get => gameSceneView.OnGoToTitle;
			set => gameSceneView.OnGoToTitle = value;
		}

		public GameScenePresenter(GameSceneView gameSceneView)
		{
			this.gameSceneView = gameSceneView ?? throw new ArgumentNullException(nameof(gameSceneView));
		}

		public void DrawScoreBoard(ScoreBoard.ScoreBoard scoreBoard)
		{
			var userScoreViewContexts = scoreBoard.UserScores.Select(userScore =>
			{
				var scoreFrameViewContexts = userScore.ScoreFrames.Select(scoreFrame =>
				{
					var pinScoreStrings = ScoreStringConverter.ConvertToPinScoreStrings(scoreFrame.PinScores);
					var frameScoreString = ScoreStringConverter.ConvertToFrameScoreString(scoreFrame);
					return new ScoreFrameViewContext(pinScoreStrings, frameScoreString);
				})
				.ToArray();
				return new UserScoreViewContext(userScore.Name, scoreFrameViewContexts, userScore.GetCurrentFrameScore().ToString());
			})
			.ToArray();
			gameSceneView.Draw(new GameSceneViewContext(scoreBoard.LaneNumber, userScoreViewContexts, scoreBoard.CurrentPlayer.Name, scoreBoard.CurrentPlayer.GetAvailableNextMaxPinScore(), scoreBoard.IsGameCompleted));
		}

		public void DrawWinner(string winnerName)
		{
			gameSceneView.DrawWinner(winnerName ?? throw new ArgumentNullException(nameof(winnerName)));
		}
	}
}
