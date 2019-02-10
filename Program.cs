using Bowling.Framework;
using System.Reflection;

namespace Bowling
{
	class Program
	{
		static void Main(string[] args)
		{
			var application = new Application();
			application.Initialize(Assembly.GetExecutingAssembly());
			application.Start();
		}
	}
}
