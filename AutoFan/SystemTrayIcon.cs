using System;
using System.Drawing;
using System.Windows.Forms;

namespace AutoFan
{
	internal class SystemTrayIcon : IDisposable
	{
		private NotifyIcon notifyIcon;

		public void Display()
		{
			notifyIcon = new NotifyIcon
			{
				Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath),
				Visible = true,
			};

			var contextMenu = new ContextMenu();
			var exitMenuItem = new MenuItem("Exit");
			exitMenuItem.Click += ExitMenuItem_Click;
			contextMenu.MenuItems.Add(exitMenuItem);

			notifyIcon.ContextMenu = contextMenu;
		}

		private void ExitMenuItem_Click(object sender, EventArgs e)
		{
			Program.Cleanup();
			notifyIcon.Visible = false;
			Application.Exit();
		}

		public void Dispose()
		{
			notifyIcon?.Dispose();
		}
	}
}
