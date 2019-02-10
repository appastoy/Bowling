using System;

namespace Bowling.Framework.Scene
{
	interface ISceneChangable
	{
		void ChangeScene(Scene nextScene);
		void ChangeScene(int nextSceneIndex);
		void ChangeScene(string nextSceneName);
		void ChangeScene(Type nextSceneType);
		void ChangeScene<TScene>() where TScene : Scene;
	}
}
