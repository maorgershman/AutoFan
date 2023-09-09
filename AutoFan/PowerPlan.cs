using System;
using System.Diagnostics;

namespace AutoFan
{
	internal class PowerPlan
	{
		private static readonly string PowerPlan_Balanced_GUID = "381b4222-f694-41f0-9685-ff5bb260df2e";
		private static readonly string PowerPlan_HighPerformance_GUID = "8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c";

		public enum Mode
		{
			Balanced, HighPerformance
		}

		public static void SetMode(Mode mode)
		{
			string guid = null;
			switch (mode)
			{
				case Mode.Balanced: guid = PowerPlan_Balanced_GUID; break;
				case Mode.HighPerformance: guid = PowerPlan_HighPerformance_GUID; break;
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
