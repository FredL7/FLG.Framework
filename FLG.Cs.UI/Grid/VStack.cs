using FLG.Cs.Math;
using FLG.Cs.UI.Layouts;
using System.Numerics;
using System.Xml;

namespace FLG.Cs.UI.Grid {
    public class VStack : Stack {
        public VStack(RectXform rectXform, Size size, int order, float weight,
            EGridDirection direction, EGridJustify justify, EGridAlignment alignment)
            : base(rectXform, size, order, weight, direction, justify, alignment) { }

        public VStack(XmlNode node) : base(node) { }

        protected override float GetChildMainMarginFirst(AbstractLayoutElement child) => child.RectXform.Margin.Top;
        protected override float GetChildMainMarginLast(AbstractLayoutElement child) => child.RectXform.Margin.Bottom;
        protected override float GetChildSecondaryMarginFirst(AbstractLayoutElement child) => child.RectXform.Margin.Left;
        protected override float GetChildSecondaryMarginLast(AbstractLayoutElement child) => child.RectXform.Margin.Right;

        protected override float GetChildSizeMain(AbstractLayoutElement child) => child.Size.Height;
        protected override float GetChildSizeSecondary(AbstractLayoutElement child) => child.Size.Width;

        protected override float GetContainerDimensionMain(Size containerDimensions) => containerDimensions.Height;
        protected override float GetContainerDimensionSecondary(Size containerDimensions) => containerDimensions.Width;

        protected override Vector2[] GetFinalPositions(float[] mainMargins, float[] secondaryMargins, float[] mainDimensions)
        {
            Vector2[] positions = new Vector2[mainMargins.Length];
            var accumulate = 0f;
            for (int i = 0; i < positions.Length; ++i)
            {
                float mainDimensionDelta = i == 0 ? 0 : mainDimensions[i - 1];
                positions[i] = new Vector2(secondaryMargins[i], accumulate + mainMargins[i] + mainDimensionDelta);
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
