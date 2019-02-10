using Bowling.Game.Common;
using System;

namespace Bowling.Game.TitleScene
{
	class TitleSceneView
	{
		static readonly int maxPlayerCount = 6;

		public Action<int> OnSelectPlayerCount;
		public Action OnEndGame;

		public void Draw()
		{
			int playerCount = 0;
			while (true)
			{
				Console.Clear();
				Console.WriteLine(" $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
				Console.WriteLine(" $$$         볼링 한판?!       $$$");
				Console.WriteLine(" $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
				Console.WriteLine();
				Console.WriteLine($" 플레이 할 인원을 선택해주세요. (1명 ~ {maxPlayerCount}명 중 선택, 0을 입력하면 게임 종료)");
				Console.Write(" => ");
				var input = Console.ReadLine();
				if (InputConverter.TryConvertInteger(input, out playerCount) && playerCount >= 0 && playerCount <= maxPlayerCount)
				{
					break;
				}

				Console.WriteLine();
				Console.Write($"0 ~ {maxPlayerCount} 사이 숫자만 입력해주세요.");
				Console.ReadLine();
			}

			if (playerCount == 0)
			{
				Console.WriteLine();
				Console.WriteLine($" 게임을 종료합니다.");
				OnEndGame?.Invoke();
			}
			else
			{
				Console.WriteLine();
				Console.WriteLine($" 플레이 인원은 {playerCount}명 입니다. 게임을 시작합니다!");
				OnSelectPlayerCount?.Invoke(playerCount);
			}
		}
	}
}
