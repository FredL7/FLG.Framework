using System.Numerics;

using FLG.Cs.Math;


namespace FLG.Cs.IDatamodel {
    public interface ILayoutElement {
        public const string DEFAULT_CHILDREN_CONTAINER = "default";

        public string Name { get; }
        public bool IsTarget { get; }
        public RectXform RectXform { get; }
        public Vector2 Position { get; }
        public Size Dimensions { get; }
        public Size Size { get; }
        public int Order { get; }
        public float Weight { get; }

        public void ComputeRectXform();
        public IEnumerable<string> GetContainers();
        public void AddChild(ILayoutElement child, string id = DEFAULT_CHILDREN_CONTAINER);
        public bool HasChildren(string id = DEFAULT_CHILDREN_CONTAINER);
        public IEnumerable<ILayoutElement> GetChildrens(string id = DEFAULT_CHILDREN_CONTAINER);
    }
}
