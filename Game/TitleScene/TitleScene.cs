using Bowling.Framework.Scene;
using Bowling.Game.Common;

namespace Bowling.Game.TitleScene
{
	[ManagedScene(true)]
	class TitleScene : Scene
	{
		TitleSceneView titleSceneView;

		public override void OnInitialize()
		{
			base.OnInitialize();
			titleSceneView = new TitleSceneView()
			{
				OnSelectPlayerCount = playerCount =>
				{
					AddOrUpdateSceneAsset(AssetNames.PlayerCount, playerCount);
					ChangeScene<GameScene.GameScene>();
				},
				OnEndGame = () => System.Environment.Exit(0)
			};
		}

		public override void OnEnter(Scene prevScene)
		{
			base.OnEnter(prevScene);
			titleSceneView.Draw();
		}

		public override void OnUnload(Scene nextScene)
		{
			base.OnUnload(nextScene);
			ClearSceneAssets();
		}
	}
}
