using Godot;

using FLG.Cs.Datamodel;
using FLG.Cs.Framework;
using FLG.Cs.Model;

namespace FLG.Godot.Helpers {
    public static class FrameworkHelper {
        public static PreferencesFramework SanitizePreferences(PreferencesFramework prefs)
        {
            if (prefs.logs != null)
            {
                PreferencesLogs logsNoNull = prefs.logs.Value;
                logsNoNull.dir = GlobalizePathUser(logsNoNull.dir);
                prefs.logs = logsNoNull;
            }

            if (prefs.ui != null)
            {
                PreferencesUI uiNoNull = prefs.ui.Value;
                for (int i = 0; i < uiNoNull.dirs.Length; ++i)
                {
                    uiNoNull.dirs[i] = GlobalizePathResources(uiNoNull.dirs[i]);
                }
            }

            if (prefs.serialization != null)
            {
                PreferencesSerialization serializationNoNull = prefs.serialization.Value;
                serializationNoNull.dir = GlobalizePathUser(serializationNoNull.dir);
            }

            return prefs;
        }

        public static void InitializeFramework(PreferencesFramework prefs)
        {
            var sanitizedPrefs = SanitizePreferences(prefs);
            var result = FrameworkManager.Instance.Initialize(sanitizedPrefs);
            if (!result) GD.PrintErr(result.GetMessage());
        }

        private static string GlobalizePathResources(string relativePath) => ProjectSettings.GlobalizePath("res://" + relativePath);
        private static string GlobalizePathUser(string relativePath) => ProjectSettings.GlobalizePath("user://" + relativePath);
    }
}
