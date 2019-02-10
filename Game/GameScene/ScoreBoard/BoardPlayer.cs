using System;
using System.Collections.Generic;
using System.Linq;

namespace Bowling.Game.ScoreBoard
{
	class BoardPlayer
	{
		List<ScoreFrame> scoreFrames;
		List<FrameScorePostProcessor> frameScorePostProcessors;
		ScoreFrame currentScoreFrame;

		public readonly string Name;
		public IReadOnlyList<IScoreFrame> ScoreFrames => scoreFrames;
		public bool IsFrameCompleted => currentScoreFrame == null;
		public bool IsGameCompleted
		{
			get
			{
				return scoreFrames.Count == ScoreRules.ScoreFrameCount &&
					scoreFrames[ScoreRules.ScoreFrameCount - 1].IsFrameScoreConfirmed;
			}
		}

		public BoardPlayer(string name)
		{
			Name = name;
			scoreFrames = new List<ScoreFrame>(ScoreRules.ScoreFrameCount);
			frameScorePostProcessors = new List<FrameScorePostProcessor>(ScoreRules.ScoreFrameCount);
		}

		public ScoreFrameState ProcessPinScore(int pinScore)
		{
			ProcessFrameScorePostProcessors(pinScore);
			return AddOrUpdateScoreFrame(pinScore);
		}

		void ProcessFrameScorePostProcessors(int pinScore)
		{
			frameScorePostProcessors.ForEach(processor => processor.Process(pinScore));
			frameScorePostProcessors.RemoveAll(processor => processor.IsCompleted);
		}

		ScoreFrameState AddOrUpdateScoreFrame(int pinScore)
		{
			if (currentScoreFrame == null)
			{
				return AddScoreFrame(pinScore);	
			}
			else
			{
				return UpdateScoreFrame(pinScore);
			}
		}

		ScoreFrameState AddScoreFrame(int pinScore)
		{
			var result = ScoreFrameState.Pending;
			bool isStrike = pinScore == ScoreRules.MaxPinScore;
			if (scoreFrames.Count < ScoreRules.LastScoreFrame)
			{
				var previousScoreFrame = scoreFrames.Count > 0 ? scoreFrames[scoreFrames.Count - 1] : null;
				if (isStrike)
				{
					var strikeScoreFrame = new StrikeScoreFrame(previousScoreFrame);
					scoreFrames.Add(strikeScoreFrame);
					frameScorePostProcessors.Add(strikeScoreFrame.CreateFrameScorePostProcessor());
					result = ScoreFrameState.Strike;
				}
				else
				{
					currentScoreFrame = new SpareScoreFrame(previousScoreFrame, pinScore);
					scoreFrames.Add(currentScoreFrame);
				}
			}
			else if (scoreFrames.Count == ScoreRules.LastScoreFrame)
			{
				currentScoreFrame = new LastScoreFrame(scoreFrames[scoreFrames.Count-1], pinScore);
				scoreFrames.Add(currentScoreFrame);
				if (isStrike)
				{
					frameScorePostProcessors.Add(currentScoreFrame.CreateFrameScorePostProcessor());
					result = ScoreFrameState.Strike;
				}
			}
			else
			{
				throw new InvalidOperationException();
			}

			return result;
		}

		ScoreFrameState UpdateScoreFrame(int pinScore)
		{
			var frameResult = currentScoreFrame.AddPinScore(pinScore);
			if (frameResult == ScoreFrameState.Spare)
			{
				frameScorePostProcessors.Add(currentScoreFrame.CreateFrameScorePostProcessor());
			}
			if (currentScoreFrame.IsCompleted)
			{
				currentScoreFrame = null;
			}

			return frameResult;
		}

		public int GetAvailableNextMaxPinScore()
		{
			if (currentScoreFrame == null) { return ScoreRules.MaxPinScore; }

			return currentScoreFrame.AvailableNextMaxPinScore;
		}

		public int GetCurrentFrameScore()
		{
			int frameScore = 0;
			for (int i = scoreFrames.Count - 1; i >= 0; i--)
			{
				if (scoreFrames[i].IsFrameScoreConfirmed)
				{
					frameScore = scoreFrames[i].FrameScore;
					break;
				}
			}

			return frameScore;
		}
	}
}
