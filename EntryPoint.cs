using System;
using System.IO;
using OpenTK;
using System.Reflection;
using System.Globalization;
using System.Threading;
using NLog;

namespace TestClient
{
	static class EntryPoint
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		public static void Main (string[] args)
		{
			var exeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			Directory.SetCurrentDirectory(exeDir);

			using (Toolkit.Init(new ToolkitOptions() {Backend = PlatformBackend.PreferNative}))
			{
				Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
				Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

				logger.Info("");
				logger.Info("Creating game");
				var game = new TheGame(logger);

				logger.Info("Running game");
			    game.Run(60);
				logger.Info("Safely exited game");
			}
		}
	}
}

