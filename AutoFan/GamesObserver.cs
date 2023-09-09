using System;
using System.Diagnostics;
using System.Threading;

namespace AutoFan
{
	internal class GamesObserver
	{
		private static readonly string[] ProcessesNames = new[] { "bf4", "cod" };
		private static readonly TimeSpan FullSpeedTime = TimeSpan.FromMinutes(5);

		private static bool IsRunning = false;

		public enum GameMode
		{ 
			Playing, NotPlaying, JustFinishedPlaying
		}

		public static void Run()
		{
			if (IsAnyProcessRunning(ProcessesNames))
			{
				SetGameMode(GameMode.Playing);
			}
			else
			{
				SetGameMode(GameMode.NotPlaying);
			}

			while (true)
			{
				var isCurrentlyRunning = IsAnyProcessRunning(ProcessesNames);
				var wasRunning = IsRunning;

				if (!isCurrentlyRunning && wasRunning)
				{
					SetGameMode(GameMode.JustFinishedPlaying);
                    Thread.Sleep(FullSpeedTime);

                    // Check if game is running again
                    if (IsAnyProcessRunning(ProcessesNames))
                    {
                        PowerPlan.SetMode(PowerPlan.Mode.HighPerformance);
                        FanControl.SetConfiguration(FanControl.Configuration.Turbo);
                    }
                    else
                    {
                        PowerPlan.SetMode(PowerPlan.Mode.Balanced);
                        FanControl.SetConfiguration(FanControl.Configuration.Silent);
                    }
                }
				else if (isCurrentlyRunning && !wasRunning)
				{
					SetGameMode(GameMode.Playing);
				}

				Thread.Sleep(TimeSpan.FromSeconds(1));
			}
		}

		public static void SetGameMode(GameMode gameMode)
		{
			switch (gameMode)
			{
				case GameMode.Playing:
					{
						IsRunning = true;
						PowerPlan.SetMode(PowerPlan.Mode.HighPerformance);
						FanControl.SetConfiguration(FanControl.Configuration.Turbo);
					}
					break;
				case GameMode.NotPlaying:
					{
						IsRunning = false;
						PowerPlan.SetMode(PowerPlan.Mode.Balanced);
						FanControl.SetConfiguration(FanControl.Configuration.Silent);
					} break;
				default:
					{
						IsRunning = false;
						PowerPlan.SetMode(PowerPlan.Mode.Balanced);
						FanControl.SetConfiguration(FanControl.Configuration.FullSpeed);
					} break;
			}
		}

		private static bool IsAnyProcessRunning(string[] processNames)
		{
			Process[] runningProcesses = Process.GetProcesses();

			foreach (string processName in processNames)
			{
				foreach (Process process in runningProcesses)
				{
					if (process.ProcessName.Equals(processName, StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
			}

			return false;
		}
	}
}
