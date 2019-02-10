using Bowling.Game.Common;
using System;

namespace Bowling.Game.GameScene.View
{
	class GameSceneView
	{
		readonly ScoreBoardView scoreBoardView = new ScoreBoardView();

		public Action<int> OnInputPinScore;
		public Action OnGoToTitle;

		public void Draw(GameSceneViewContext viewContext)
		{
			int currentPinScore = 0;
			while (true)
			{
				Console.Clear();
				scoreBoardView.Draw(viewContext.LaneNumber, viewContext.UserScores);
				if (viewContext.IsGameCompleted) { return; }

				Console.WriteLine();
				Console.WriteLine($" {viewContext.CurrentPlayerName} 턴 (0 ~ {viewContext.AvailableNextMaxPinScore} 중 선택, -1를 입력하면 타이틀로 돌아감)");
				Console.Write($" => ");
				var input = Console.ReadLine();
				if (InputConverter.TryConvertInteger(input, out currentPinScore))
				{
					if (currentPinScore == -1)
					{
						OnGoToTitle?.Invoke();
						return;
					}

					if (currentPinScore >= 0 && currentPinScore <= viewContext.AvailableNextMaxPinScore)
					{
						break;
					}
				}

				Console.WriteLine();
				Console.WriteLine($" -1 ~ {viewContext.AvailableNextMaxPinScore} 사이 숫자만 입력해주세요.");
				Console.ReadLine();
			}

			OnInputPinScore?.Invoke(currentPinScore);
		}

		public void DrawWinner(string winnerName)
		{
			Console.WriteLine();
			Console.WriteLine(" !경기 종료!");
			Console.WriteLine();
			Console.Write($" {winnerName} 승리!");
			Console.ReadLine();
		}
	}
}
