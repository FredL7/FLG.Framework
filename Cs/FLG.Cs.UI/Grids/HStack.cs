using System.Numerics;
using System.Xml;

using FLG.Cs.Datamodel;
using FLG.Cs.Math;
using FLG.Cs.UI.Layouts;


namespace FLG.Cs.UI.Grids {
    public class HStack : Stack {
        public override ELayoutElement Type { get => ELayoutElement.HSTACK; }

        public HStack(string name, XmlNode node) : base(name, node) { }
        public HStack(string name, LayoutAttributes layoutAttr, GridAttributes gridAttr) : base(name, layoutAttr, gridAttr) { }

        protected override float GetChildMainMarginFirst(ILayoutElement child) => child.RectXform.Margin.Left;
        protected override float GetChildMainMarginLast(ILayoutElement child) => child.RectXform.Margin.Right;
        protected override float GetChildSecondaryMarginFirst(ILayoutElement child) => child.RectXform.Margin.Top;
        protected override float GetChildSecondaryMarginLast(ILayoutElement child) => child.RectXform.Margin.Bottom;

        protected override float GetChildSizeMain(ILayoutElement child) => child.Size.Width;
        protected override float GetChildSizeSecondary(ILayoutElement child) => child.Size.Height;

        protected override float GetStackDimensionMain(Size stackDimensions) => stackDimensions.Width;
        protected override float GetStackDimensionSecondary(Size stackDimensions) => stackDimensions.Height;

        protected override Vector2[] GetFinalPositions(float[] mainMargins, float[] secondaryMargins, float[] mainDimensions)
        {
            Vector2[] positions = new Vector2[mainDimensions.Length];
            var accumulate = 0f;
            for (int i = 0; i < mainDimensions.Length; ++i)
            {
                float mainDimensionDelta = i == 0 ? 0 : mainDimensions[i - 1];
                positions[i] = new Vector2(accumulate + mainMargins[i] + mainDimensionDelta, secondaryMargins[i]);
                accumulate += mainMargins[i] + mainDimensionDelta;
            }
            return positions;
        }

        protected override Size[] GetFinalSizes(float[] mainDimensions, float[] secondaryDimensions)
        {
            Size[] sizes = new Size[mainDimensions.Length];
            for (int i = 0; i < sizes.Length; ++i)
                sizes[i] = new Size(mainDimensions[i], secondaryDimensions[i]);
            return sizes;
        }
    }
}
