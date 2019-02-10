using Bowling.Framework.Scene;
using Bowling.Game.Common;
using Bowling.Game.GameScene.Presenter;
using Bowling.Game.GameScene.View;

namespace Bowling.Game.GameScene
{
	[ManagedScene]
	class GameScene : Scene
	{
		ScoreBoard.ScoreBoard scoreBoard;
		GameSceneView gameSceneView;
		GameScenePresenter gameScenePresenter;

		public override void OnLoad(Scene prevScene)
		{
			base.OnLoad(prevScene);
			scoreBoard = new ScoreBoard.ScoreBoard(prevScene.GetSceneAsset<int>(AssetNames.PlayerCount));
			gameSceneView = new GameSceneView();
			gameScenePresenter = new GameScenePresenter(gameSceneView)
			{
				OnInputPinScore = pinScore =>
				{
					scoreBoard.ProcessPinScore(pinScore);
					scoreBoard.TryMoveNext();
					gameScenePresenter.DrawScoreBoard(scoreBoard);

					if (scoreBoard.IsGameCompleted)
					{
						var winner = scoreBoard.GetHighestTotalScorePlayer();
						gameScenePresenter.DrawWinner(winner.Name);
						ChangeScene<TitleScene.TitleScene>();
					}
				},
				OnGoToTitle = () => ChangeScene<TitleScene.TitleScene>(),
			};
		}

		public override void OnEnter(Scene prevScene)
		{
			base.OnEnter(prevScene);
			gameScenePresenter.DrawScoreBoard(scoreBoard);
		}
	}
}
