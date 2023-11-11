using Godot;

namespace TestGDInclude {
    public static class SceneHelper {
        public static void RemoveAllChildrens(Node parent)
        {
            var childrens = parent.GetChildren();
            foreach (var child in childrens)
                child.QueueFree();
        }
    }
}
