using System;

namespace Bowling.Framework.Scene
{
	[AttributeUsage(AttributeTargets.Class)]
	class ManagedSceneAttribute : Attribute
	{
		public readonly int Priority;
		public readonly bool IsStartScene;
		public ManagedSceneAttribute(bool isStartScene = false, int priority = 0)
		{
			IsStartScene = isStartScene;
			Priority = priority;
		}
	}
}
