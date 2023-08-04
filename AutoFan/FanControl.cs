using System;
using System.Diagnostics;
using System.IO;

namespace AutoFan
{
	internal class FanControl
	{
		private static readonly string FanControlPath = @"C:\FanControl";
		private static readonly string FanControlConfig_FullSpeed = "full_speed.json";
		private static readonly string FanControlConfig_Silent = "silent.json";
		private static readonly string FanControlConfig_Turbo = "turbo.json";

		private static readonly string FanControlExePath = Path.Combine(FanControlPath, "FanControl.exe");
		private static readonly string FanControlConfigurationsPath = Path.Combine(FanControlPath, "Configurations");
		private static readonly string FanControlConfig_FullSpeed_Path = Path.Combine(FanControlConfigurationsPath, FanControlConfig_FullSpeed);
		private static readonly string FanControlConfig_Silent_Path = Path.Combine(FanControlConfigurationsPath, FanControlConfig_Silent);
		private static readonly string FanControlConfig_Turbo_Path = Path.Combine(FanControlConfigurationsPath, FanControlConfig_Turbo);

		public enum Configuration
		{
			FullSpeed, Silent, Turbo
		}

		public static void SetConfiguration(Configuration configuration)
		{
			string configStr = null;
			switch (configuration)
			{ 
				case Configuration.FullSpeed: configStr = FanControlConfig_FullSpeed; break;
				case Configuration.Silent: configStr = FanControlConfig_Silent; break;
				case Configuration.Turbo: configStr = FanControlConfig_Turbo; break;
			}

			if (configStr == null)
			{
				return;
			}

			string errMsg = CheckEverything();
			if (errMsg != null)
			{
				throw new Exception(errMsg);
			}

			Process.Start(FanControlExePath, $"-c {configStr}");
		}

		public static string CheckEverything()
		{
			if (!Directory.Exists(FanControlPath))
			{
				return "Can't find FanControl folder! (" + FanControlPath + ")";
			}

			if (!File.Exists(FanControlExePath))
			{
				return "Can't find FanControl.exe! (" + FanControlExePath + ")";
			}

			if (!Directory.Exists(FanControlConfigurationsPath))
			{
				return "Can't find FanControl configuration folder! (" + FanControlConfigurationsPath + ")";
			}

			if (!File.Exists(FanControlConfig_FullSpeed_Path))
			{
				return "Can't find FanControl full speed configuration! (" + FanControlConfig_FullSpeed_Path + ")";
			}

			if (!File.Exists(FanControlConfig_Silent_Path))
			{
				return "Can't find FanControl silent configuration! (" + FanControlConfig_Silent_Path + ")";
			}

			if (!File.Exists(FanControlConfig_Turbo_Path))
			{
				return "Can't find FanControl turbo configuration! (" + FanControlConfig_Turbo_Path + ")";
			}

			return null;
		}
	}
}
