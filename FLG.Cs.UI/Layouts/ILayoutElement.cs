using System.Numerics;

using FLG.Cs.Math;

namespace FLG.Cs.UI.Layouts {
    public interface ILayoutElement {
        public string GetName();
        public bool GetIsTarget();
        public bool HasChildren();
        public IEnumerable<ILayoutElement> GetChildrens();
        public Vector2 GetPosition();
        public Size GetDimensions();
    }
}
