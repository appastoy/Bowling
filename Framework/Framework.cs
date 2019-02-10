using Bowling.Framework.Scene;
using System.Reflection;

namespace Bowling.Framework
{
	class Application
	{
		SceneManager sceneManager = new SceneManager();

		public virtual void Initialize(params Assembly[] sceneAssemblies)
		{
			sceneManager.Initialize(sceneAssemblies);
		}

		public virtual void Start()
		{
			sceneManager.Start();
		}

		public virtual void Update()
		{
			sceneManager.Update();
		}

		public virtual void Release()
		{
			sceneManager.Release();
		}
	}
}
