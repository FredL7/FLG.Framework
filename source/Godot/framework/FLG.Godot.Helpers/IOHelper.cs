using Godot;

namespace FLG.Godot.Helpers {
    public static class IOHelper {
        public static string GlobalizePathResources(string relativePath) => ProjectSettings.GlobalizePath("res://" + relativePath);
        public static string GlobalizePathUser(string relativePath) => ProjectSettings.GlobalizePath("user://" + relativePath);
    }
}
