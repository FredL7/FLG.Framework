using System.Numerics;
using System.Xml;

using FLG.Cs.IDatamodel;
using FLG.Cs.Math;
using FLG.Cs.UI.Layouts;


namespace FLG.Cs.UI.Grids {
    public class HStack : Stack {
        public HStack(string name, XmlNode node) : base(name, node) { }
        public HStack(
            string name, float width, float height, Spacing margin, Spacing padding, int order, float weight, bool isTarget,
            EGridDirection direction, EGridJustify justify, EGridAlignment alignment
        ) : base(name, width, height, margin, padding, order, weight, isTarget, direction, justify, alignment) { }

        protected override float GetChildMainMarginFirst(ILayoutElement child) => child.RectXform.Margin.Left;
        protected override float GetChildMainMarginLast(ILayoutElement child) => child.RectXform.Margin.Right;
        protected override float GetChildSecondaryMarginFirst(ILayoutElement child) => child.RectXform.Margin.Top;
        protected override float GetChildSecondaryMarginLast(ILayoutElement child) => child.RectXform.Margin.Bottom;

        protected override float GetChildSizeMain(ILayoutElement child) => child.Size.Width;
        protected override float GetChildSizeSecondary(ILayoutElement child) => child.Size.Height;

        protected override float GetContainerDimensionMain(Size containerDimensions) => containerDimensions.Width;
        protected override float GetContainerDimensionSecondary(Size containerDimensions) => containerDimensions.Height;

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
