using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoFan
{
	internal class Program
	{
		static void Main()
		{
			string errMsg = FanControl.CheckEverything();
			if (errMsg != null)
			{
				MessageBox.Show(errMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Task.Run(GamesObserver.Run);

			using (var trayIcon = new SystemTrayIcon())
			{
				trayIcon.Display();
				Application.Run();
			}
		}

		public static void Cleanup()
		{
			GamesObserver.SetGameMode(GamesObserver.GameMode.NotPlaying);
		}
	}
}
