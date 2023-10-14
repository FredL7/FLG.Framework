using FLG.Cs.Math;
using FLG.Cs.UI.Layouts;
using System.Numerics;

namespace FLG.Cs.UI.Grid {
    internal class HStack : Stack {
        public HStack(RectXform rectXform, Size size, int order = 0, float weight = 0,
            EGridDirection direction = EGridDirection.NORMAL,
            EGridJustify justify = EGridJustify.START,
            EGridAlignment alignment = EGridAlignment.START)
            : base(rectXform, size, order, weight, direction, justify, alignment) { }

        protected override float GetChildMainMarginFirst(AbstractLayoutElement child) => child.RectXform.Margin.Left;
        protected override float GetChildMainMarginLast(AbstractLayoutElement child) => child.RectXform.Margin.Right;
        protected override float GetChildSecondaryMarginFirst(AbstractLayoutElement child) => child.RectXform.Margin.Top;
        protected override float GetChildSecondaryMarginLast(AbstractLayoutElement child) => child.RectXform.Margin.Bottom;

        protected override float GetChildSizeMain(AbstractLayoutElement child) => child.Size.Width;
        protected override float GetChildSizeSecondary(AbstractLayoutElement child) => child.Size.Height;

        protected override float GetContainerDimensionMain(Size containerDimensions) => containerDimensions.Width;
        protected override float GetContainerDimensionSecondary(Size containerDimensions) => containerDimensions.Height;

        protected override Vector2[] GetFinalPositions(float[] mainMargins, float[] secondaryMargins)
        {
            Vector2[] positions = new Vector2[mainMargins.Length];
            var accumulate = 0f;
            for (int i = 0; i < positions.Length; ++i) {
                positions[i] = new Vector2(accumulate + mainMargins[i], secondaryMargins[i]);
                accumulate += mainMargins[i];
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
