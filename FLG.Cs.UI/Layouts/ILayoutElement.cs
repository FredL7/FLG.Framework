using FLG.Cs.Math;
using System.Numerics;

namespace FLG.Cs.UI.Layouts {
    public interface ILayoutElement {
        public string GetName();
        public bool HasChildren();
        public IEnumerable<ILayoutElement> GetChildrens();
        public Vector2 GetPosition();
        public Size GetDimensions();
    }
}
