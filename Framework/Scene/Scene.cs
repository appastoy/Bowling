using System;

namespace Bowling.Framework.Scene
{
	abstract class Scene
	{
		static readonly SceneAssetManager globalSceneAssetManager = new SceneAssetManager();
		readonly SceneAssetManager sceneAssetManager = new SceneAssetManager();
		ISceneChangable sceneChangable = null;
		
		public virtual string Name => GetType().Name;

		public virtual void OnInitialize() { }
		public virtual void OnEnter(Scene prevScene) { }
		public virtual void OnLoad(Scene prevScene) { }
		public virtual void OnUpdate() { }
		public virtual void OnUnload(Scene nextScene) { }
		public virtual void OnExit(Scene nextScene) { }
		public virtual void OnRelease() { }

		public void ChangeScene(Scene nextScene) => sceneChangable.ChangeScene(nextScene);
		public void ChangeScene(string nextSceneName) => sceneChangable.ChangeScene(nextSceneName);
		public void ChangeScene(int nextSceneIndex) => sceneChangable.ChangeScene(nextSceneIndex);
		public void ChangeScene(Type nextSceneType) => sceneChangable.ChangeScene(nextSceneType);
		public void ChangeScene<TScene>() where TScene : Scene => sceneChangable.ChangeScene<TScene>();

		public static void AddOrUpdateGlobalSceneAsset(string name, object asset) => globalSceneAssetManager.AddOrUpdateAsset(name, asset);
		public static void RemoveGlobalSceneAsset(string name) => globalSceneAssetManager.RemoveAsset(name);
		public static void ClearGlobalSceneAssets() => globalSceneAssetManager.ClearAssets();
		public static bool HasGlobalSceneAsset(string name) => globalSceneAssetManager.HasAsset(name);
		public static bool TryGetGlobalSceneAsset(string name, out object asset) => globalSceneAssetManager.TryGetAsset(name, out asset);
		public static bool TryGetGlobalSceneAsset<T>(string name, out T asset) => globalSceneAssetManager.TryGetAsset(name, out asset);
		public static object GetGlobalSceneAsset(string name) => globalSceneAssetManager.GetAsset(name);
		public static T GetGlobalSceneAsset<T>(string name) => globalSceneAssetManager.GetAsset<T>(name);

		protected void AddOrUpdateSceneAsset(string name, object asset) => sceneAssetManager.AddOrUpdateAsset(name, asset);
		protected void RemoveSceneAsset(string name) => sceneAssetManager.RemoveAsset(name);
		protected void ClearSceneAssets() => sceneAssetManager.ClearAssets();
		public bool HasSceneAsset(string name) => sceneAssetManager.HasAsset(name);
		public bool TryGetSceneAsset(string name, out object asset) => sceneAssetManager.TryGetAsset(name, out asset);
		public bool TryGetSceneAsset<T>(string name, out T asset) => sceneAssetManager.TryGetAsset(name, out asset);
		public object GetSceneAsset(string name) => sceneAssetManager.GetAsset(name);
		public T GetSceneAsset<T>(string name) => sceneAssetManager.GetAsset<T>(name);
	}
}
