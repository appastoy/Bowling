using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bowling.Framework.Scene
{
	class SceneManager : ISceneChangable
	{
		List<Scene> scenes;
		int currentSceneIndex;
		int previousSceneIndex;

		public Scene CurrentScene => FindScene(currentSceneIndex);
		public Scene PreviousScene => FindScene(previousSceneIndex);

		public void Initialize(params Assembly[] sceneAssemblies)
		{
			currentSceneIndex = 0;
			previousSceneIndex = 0;

			scenes = new List<Scene>()
			{
				new EmptyScene()
			};

			var managedScenes = sceneAssemblies.SelectMany(assembly => assembly.GetTypes().Where(type =>
												{
													if (type.IsAbstract || !type.IsSubclassOf(typeof(Scene))) { return false; }
													return type.GetCustomAttributes(typeof(ManagedSceneAttribute)).Any();
												})
											   .OrderBy(type => type.GetCustomAttributes(typeof(ManagedSceneAttribute)).OfType<ManagedSceneAttribute>().First().Priority)
											   .Select(type => Activator.CreateInstance(type) as Scene));
			scenes.AddRange(managedScenes);
			InjectSceneManager();
			scenes.ForEach(scene => scene.OnInitialize());
		}

		void InjectSceneManager()
		{
			var sceneChangableFieldInfo = typeof(Scene).GetField("sceneChangable", BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new Exception("Cann't find sceneChangable field in Scene class");
			scenes.ForEach(scene => sceneChangableFieldInfo.SetValue(scene, this));
		}

		public void Start()
		{
			var startScenes = scenes.Where(scene => scene.GetType().GetCustomAttributes(typeof(ManagedSceneAttribute)).OfType<ManagedSceneAttribute>().First().IsStartScene).ToArray();
			if (startScenes.Length == 0) { throw new InvalidOperationException("There is no start scene."); }
			if (startScenes.Length != 1) { throw new InvalidOperationException("There are multiple start scenes."); }

			ChangeScene(startScenes[0]);
		}

		public void Update()
		{
			CurrentScene.OnUpdate();
		}

		public void Release()
		{
			ChangeScene<EmptyScene>();
			scenes.ForEach(scene => scene.OnRelease());
		}

		public void ChangeScene(Scene nextScene)
		{
			var nextSceneIndex = FindSceneIndex(nextScene ?? throw new ArgumentNullException(nameof(nextScene)));
			ChangeScene(nextSceneIndex);
		}

		public void ChangeScene(string nextSceneName)
		{
			var nextSceneIndex = FindSceneIndex(nextSceneName ?? throw new ArgumentNullException(nameof(nextSceneName)));
			ChangeScene(nextSceneIndex);
		}

		public void ChangeScene(Type sceneType)
		{
			var nextSceneIndex = FindSceneIndex(sceneType ?? throw new ArgumentNullException(nameof(sceneType)));
			ChangeScene(nextSceneIndex);
		}

		public void ChangeScene<TScene>() where TScene : Scene
		{
			ChangeScene(typeof(TScene));
		}

		public void ChangeScene(int nextSceneIndex)
		{
			previousSceneIndex = currentSceneIndex;
			currentSceneIndex = nextSceneIndex;

			var prevScene = FindScene(previousSceneIndex);
			var nextScene = FindScene(nextSceneIndex);

			prevScene.OnExit(nextScene);
			nextScene.OnLoad(prevScene);
			prevScene.OnUnload(nextScene);
			nextScene.OnEnter(prevScene);
		}

		public Scene FindScene(int sceneIndex)
		{
			if (sceneIndex < 0 || sceneIndex >= scenes.Count)
			{
				throw new ArgumentOutOfRangeException(nameof(sceneIndex));
			}

			return scenes[sceneIndex];
		}

		public int FindSceneIndex(Scene scene)
		{
			return scenes.IndexOf(scene);
		}

		public int FindSceneIndex(string sceneName)
		{
			return scenes.FindIndex(scene => scene.Name == sceneName);
		}

		public int FindSceneIndex(Type type)
		{
			return scenes.FindIndex(scene => scene.GetType() == type || scene.GetType().IsSubclassOf(type));
		}

		public int FindSceneIndex<TScene>() where TScene : Scene
		{
			return FindSceneIndex(typeof(TScene));
		}
	}
}
