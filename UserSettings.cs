using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NLog;
using TestClient.Utilities.Console;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace TestClient
{
    sealed class UserSettings
    {
        public static UserSettings Instance { get; private set; }

        public static event VoidEventHandler SettingsChanged;

        public static void RaiseSettingsChanged() => SettingsChanged?.Invoke();

        static UserSettings()
        {
            initialiseCommandParameters();
        }

        #region I/O
        private static readonly JsonSerializer serializer = makeSerializer();

        private static JsonSerializer makeSerializer()
        {
            var s = new JsonSerializer();
            s.Converters.Add(new StringEnumConverter());
            s.ContractResolver = new CamelCasePropertyNamesContractResolver();
            s.Formatting = Formatting.Indented;
            return s;
        }

        public static void Load(Logger logger)
        {
            logger.Debug($"Attempting to load settings from settings file: {Constants.Paths.UserSettingsFile}");

            try
            {
                using (var reader = File.OpenText(Constants.Paths.UserSettingsFile))
                {
                    Instance = serializer.Deserialize<UserSettings>(new JsonTextReader(reader));
                    SettingsChanged?.Invoke();
                }
                logger.Debug("Finished loading user settings.");
            }
            catch (Exception e)
            {
                logger.Warn($"Could not load user settings: {e.Message}");
                logger.Info("Loading default settings.");
                Instance = getDefaultInstance();
            }
        }

        public static bool Save(Logger logger)
        {
            logger.Debug($"Attempting to save settings to settings file: {Constants.Paths.UserSettingsFile}");

            try
            {
                using (var writer = new StringWriter())
                {
                    serializer.Serialize(writer, Instance);

                    var fileName = Constants.Paths.UserSettingsFile;
                    var dirName = Path.GetDirectoryName(fileName);

                    // ReSharper disable AssignNullToNotNullAttribute
                    if (!Directory.Exists(dirName))
                        Directory.CreateDirectory(dirName);
                    // ReSharper restore AssignNullToNotNullAttribute
                    File.WriteAllText(fileName, writer.ToString());
                }
                logger.Debug("Finished saving user settings.");
                return true;
            }
            catch (Exception e)
            {
                logger.Debug($"Could not save user settings: {e.Message}");
            }
            return false;
        }

        private static UserSettings getDefaultInstance()
        {
            return new UserSettings();
        }
        #endregion

        #region Console

        private static readonly Assembly thisAssembly = typeof(UserSettings).Assembly;

        private static void initialiseCommandParameters()
        {
            var allParameters = getFieldsOf(typeof(UserSettings)).ToList();

            ConsoleCommands.AddParameterCompletion("allSettingStrings", allParameters);
        }

        private static IEnumerable<string> getFieldsOf(Type type)
        {
            return type.GetFields()
                .SelectMany(field =>
                    {
                        var fieldName = field.Name.ToLower();
                        var fieldType = field.FieldType;

                        if (fieldType.Assembly != thisAssembly)
                            return new[] {fieldName};

                        return getFieldsOf(fieldType).Select(suffix => $"{fieldName}.{suffix}");
                    });
        }

        [Command("setting", "allSettingStrings")]
        private static void setSetting(Logger logger, CommandParameters p)
        {
            if (p.Args.Length != 2)
            {
                logger.Warn("Usage: \"setting [setting_name] [setting_value]\"");
                return;
            }

            // Convert to JSON.
            var splitSettingName = p.Args[0].Split('.');
            var result = buildJson(splitSettingName, 0);
            var json = result.Item1 + p.Args[1] + result.Item2;
            try
            {
                serializer.Populate(new StringReader(json), Instance);
                SettingsChanged?.Invoke();
            }
            catch (JsonReaderException e)
            {
                logger.Warn($"Problem with parsing your setting: {e.Message}");
                return;
            }
            Save(logger);
        }

        private static Tuple<string, string> buildJson(string[] parts, int i)
        {
            if (i >= parts.Length) return new Tuple<string,string>("", "");
            var tuple = buildJson(parts, i + 1);
            return new Tuple<string,string>($"{{ \"{parts[i]}\": {tuple.Item1}", $"{tuple.Item2} }}");
        }
        #endregion

        public MiscSettings Misc = new MiscSettings();
        public UISettings UI = new UISettings();
        public GraphicsSettings Graphics = new GraphicsSettings();
        public DebugSettings Debug = new DebugSettings();

        public class MiscSettings
        {
            public string Username = "";
            public string SavedNetworkAddress = "";

            public bool ShowTraceMessages = true;
        }

        public class UISettings
        {
            public float UIScale = 1f;
        }

        public class GraphicsSettings
        {
            public float UpSample = 1f;
        }

        public class DebugSettings
        {
            public bool Deferred = false;
            public int Pathfinding = 0;
            public int InfoScreen = 0;
            public bool InvulnerableBuildings = false;
        }
    }
}
