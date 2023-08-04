using System;
using System.Diagnostics;

namespace AutoFan
{
	internal class PowerPlan
	{
		private static readonly string PowerPlan_Balanced_GUID = "381b4222-f694-41f0-9685-ff5bb260df2e";
		private static readonly string PowerPlan_UltimatePerformance_GUID = "afe1e188-f406-464f-a156-c60b66d855ed";

		public enum Mode
		{
			Balanced, UltimatePerformance
		}

		public static void SetMode(Mode mode)
		{
			string guid = null;
			switch (mode)
			{
				case Mode.Balanced: guid = PowerPlan_Balanced_GUID; break;
				case Mode.UltimatePerformance: guid = PowerPlan_UltimatePerformance_GUID; break;
			}

			if (guid == null)
			{
				return;
			}

			try
			{
				Process process = new Process();
				ProcessStartInfo startInfo = new ProcessStartInfo
				{
					FileName = "powercfg",
					Arguments = $"/S {guid}",
					RedirectStandardOutput = true,
					UseShellExecute = false,
					CreateNoWindow = true
				};

				process.StartInfo = startInfo;
				process.Start();
				process.WaitForExit();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
