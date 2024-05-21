using System.Numerics;

using FLG.Cs.Math;


namespace FLG.Cs.Datamodel {
    public interface ILayoutElement {
        public const string DEFAULT_CHILDREN_TARGET = "default";

        public string Name { get; }
        public ELayoutElement Type { get; }
        public bool IsTarget { get; }
        public RectXform RectXform { get; }
        public Vector2 Position { get; }
        public Size Dimensions { get; }
        public Size Size { get; }
        public int Order { get; }
        public float Weight { get; }

        public void ComputeRectXform();
        public IEnumerable<string> GetTargets();
        public void AddChild(ILayoutElement child, string id = DEFAULT_CHILDREN_TARGET);
        public void OnAddedToPage(string id);
        public bool HasChildren(string id = DEFAULT_CHILDREN_TARGET);
        public IEnumerable<ILayoutElement> GetChildrens(string id = DEFAULT_CHILDREN_TARGET);
    }
}
