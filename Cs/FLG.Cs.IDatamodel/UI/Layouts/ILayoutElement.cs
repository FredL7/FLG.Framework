using System.Numerics;

using FLG.Cs.Math;


namespace FLG.Cs.IDatamodel {
    public interface ILayoutElement {
        public const string DEFAULT_CHILDREN_CONTAINER = "default";

        public string GetName();
        public bool GetIsTarget();
        public RectXform GetRectXform();
        public Vector2 GetPosition();
        public Size GetDimensions();
        public Size GetSize();
        public int GetOrder();
        public float GetWeight();

        public void ComputeRectXform();
        public IEnumerable<string> GetContainers();
        public void AddChild(ILayoutElement child, string id = DEFAULT_CHILDREN_CONTAINER);
        public bool HasChildren(string id = DEFAULT_CHILDREN_CONTAINER);
        public IEnumerable<ILayoutElement> GetChildrens(string id = DEFAULT_CHILDREN_CONTAINER);
    }
}
