using Godot;


namespace FLG.Godot.Helpers {
    public static class SceneHelper {
        public static void RemoveAllChildrens(Node parent)
        {
            var childrens = parent.GetChildren();
            foreach (var child in childrens)
            {
                parent.RemoveChild(child);
                child.QueueFree();
            }
        }

        public static void RemoveAllChildrensImmediately(Node parent)
        {
            var childrens = parent.GetChildren();
            foreach (var child in childrens)
            {
                parent.RemoveChild(child);
                child.Free();
            }
        }
    }
}
