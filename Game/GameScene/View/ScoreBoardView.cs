using System;
using System.Collections.Generic;

namespace Bowling.Game.GameScene.View
{
	class ScoreBoardView
	{
		static readonly string scoreBoardHeader =
@"┌─────┬─────┬─────┬─────┬─────┬─────┬─────┬─────┬─────┬─────┬────────┬───────┐
│ {0,3} │  1  │  2  │  3  │  4  │  5  │  6  │  7  │  8  │  9  │   10   │  HDP  │";

		static readonly string scoreBoardUserScoreFormat =
@"├─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┼────────┼───────┤
│     │ {1,1} {2,1} │ {4,1} {5,1} │ {7,1} {8,1} │ {10,1} {11,1} │ {13,1} {14,1} │ {16,1} {17,1} │ {19,1} {20,1} │ {22,1} {23,1} │ {25,1} {26,1} │ {28,1} {29,1} {30,1}  │       │
│  {0}  ├─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┼────────┤       │
│     │ {3,3} │ {6,3} │ {9,3} │ {12,3} │ {15,3} │ {18,3} │ {21,3} │ {24,3} │ {27,3} │  {31,3}   │  {32,3}  │";

		static readonly string scoreBoardFooter =
@"└─────┴─────┴─────┴─────┴─────┴─────┴─────┴─────┴─────┼─────┼────────┼───────┤
                                                      │ {0,3} │  {1,3}   │  {2,3}  │
                                                      └─────┴────────┴───────┘";

		public void Draw(int laneNumber, IReadOnlyList<UserScoreViewContext> userScores)
		{
			DrawHeader(laneNumber);
			foreach (var userScore in userScores)
			{
				DrawUserScore(userScore);
			}
			DrawFooter();
		}

		void DrawHeader(int laneNumber)
		{
			Console.WriteLine(scoreBoardHeader, laneNumber);
		}

		void DrawUserScore(UserScoreViewContext viewContext)
		{
			Console.WriteLine(scoreBoardUserScoreFormat, viewContext.Name,
				viewContext.GetPinScore(0, 0), viewContext.GetPinScore(0, 1), viewContext.GetFrameScore(0),
				viewContext.GetPinScore(1, 0), viewContext.GetPinScore(1, 1), viewContext.GetFrameScore(1),
				viewContext.GetPinScore(2, 0), viewContext.GetPinScore(2, 1), viewContext.GetFrameScore(2),
				viewContext.GetPinScore(3, 0), viewContext.GetPinScore(3, 1), viewContext.GetFrameScore(3),
				viewContext.GetPinScore(4, 0), viewContext.GetPinScore(4, 1), viewContext.GetFrameScore(4),
				viewContext.GetPinScore(5, 0), viewContext.GetPinScore(5, 1), viewContext.GetFrameScore(5),
				viewContext.GetPinScore(6, 0), viewContext.GetPinScore(6, 1), viewContext.GetFrameScore(6),
				viewContext.GetPinScore(7, 0), viewContext.GetPinScore(7, 1), viewContext.GetFrameScore(7),
				viewContext.GetPinScore(8, 0), viewContext.GetPinScore(8, 1), viewContext.GetFrameScore(8),
				viewContext.GetPinScore(9, 0), viewContext.GetPinScore(9, 1), viewContext.GetPinScore(9, 2), viewContext.GetFrameScore(9),
				viewContext.TotalScore);
		}

		void DrawFooter()
		{
			Console.WriteLine(scoreBoardFooter, 0, 0, 0);
		}
	}
}
