using System.Collections.Generic;

namespace Bowling.Framework.Scene
{
	class SceneAssetManager
	{
		Dictionary<string, object> sceneAssets = new Dictionary<string, object>();

		public void AddOrUpdateAsset(string name, object asset)
		{
			if (sceneAssets.ContainsKey(name))
			{
				sceneAssets[name] = asset;
			}
			else
			{
				sceneAssets.Add(name, asset);
			}
		}

		public void RemoveAsset(string name)
		{
			sceneAssets.Remove(name);
		}

		public void ClearAssets()
		{
			sceneAssets.Clear();
		}

		public bool HasAsset(string name)
		{
			return sceneAssets.ContainsKey(name);
		}

		public bool TryGetAsset(string name, out object asset)
		{
			return sceneAssets.TryGetValue(name, out asset);
		}

		public bool TryGetAsset<T>(string name, out T asset)
		{
			asset = default(T);
			var found = sceneAssets.TryGetValue(name, out object rawAsset);
			if (found && rawAsset != null)
			{
				asset = (T)rawAsset;
			}
			return found;
		}

		public object GetAsset(string name)
		{
			return sceneAssets[name];
		}

		public T GetAsset<T>(string name)
		{
			return (T)sceneAssets[name];
		}
	}
}
