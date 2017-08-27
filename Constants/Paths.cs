using System.IO;
using System;

namespace TestClient
{
    static partial class Constants
    {
        public static class Paths
        {
            private static string settingsDirectory;

            public static string SettingsDirectory
            {
                get
                {
                    if (settingsDirectory != null)
                        return settingsDirectory;

                    var dir = Path.Combine(Environment.CurrentDirectory, "Settings");
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    settingsDirectory = dir;
                    return dir;
                }
            }

            public static readonly string UserSettingsFile = SettingsDirectory + "/usersettings.json";
        }
    }
}
