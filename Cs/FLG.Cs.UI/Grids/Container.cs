using System.Diagnostics;
using System.Numerics;
using System.Xml;
using FLG.Cs.IDatamodel;
using FLG.Cs.Math;
using FLG.Cs.UI.Layouts;

namespace FLG.Cs.UI.Grids {
    public class Container : AbstractLayoutElementComposite {
        public override ELayoutElement Type { get => ELayoutElement.CONTAINER; }

        internal Container(string name, XmlNode node) : base(name, node) { }
        internal Container(string name, LayoutAttributes layoutAttributes, bool isTarget) : base(name, layoutAttributes, isTarget) { }

        public override sealed void AddChild(ILayoutElement child, string id = ILayoutElement.DEFAULT_CHILDREN_TARGET)
        {
            Debug.Assert(!HasChildren(id), "Container cannot have multiple childrens");
            base.AddChild(child, id);
        }

        protected sealed override void ComputeChildrenSizesAndPositions(string id = ILayoutElement.DEFAULT_CHILDREN_TARGET)
        {
            ComputeSizesAndPositions(GetChildrensInternal(id));
        }

        internal sealed override void ComputeContentSizesAndPositions(List<ILayoutElement> content)
        {
            ComputeSizesAndPositions(content);
        }

        private void ComputeSizesAndPositions(List<ILayoutElement> childrens)
        {
            var children = childrens[0]; // Assert only 1 child
            // TODO: Include margin and padding
            children.RectXform.SetSizesAndPosition(Dimensions, Vector2.Zero);
        }
    }
}
