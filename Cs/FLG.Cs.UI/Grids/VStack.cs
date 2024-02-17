﻿using System.Numerics;
using System.Xml;

using FLG.Cs.IDatamodel;
using FLG.Cs.Math;
using FLG.Cs.UI.Layouts;


namespace FLG.Cs.UI.Grids {
    public class VStack : Stack {
        public VStack(string name, XmlNode node) : base(name, node) { }
        public VStack(
            string name, float width, float height, Spacing margin, Spacing padding, int order, float weight, bool isTarget,
            EGridDirection direction, EGridJustify justify, EGridAlignment alignment
        ) : base(name, width, height, margin, padding, order, weight, isTarget, direction, justify, alignment) { }

        protected override float GetChildMainMarginFirst(ILayoutElement child) => child.GetRectXform().Margin.Top;
        protected override float GetChildMainMarginLast(ILayoutElement child) => child.GetRectXform().Margin.Bottom;
        protected override float GetChildSecondaryMarginFirst(ILayoutElement child) => child.GetRectXform().Margin.Left;
        protected override float GetChildSecondaryMarginLast(ILayoutElement child) => child.GetRectXform().Margin.Right;

        protected override float GetChildSizeMain(ILayoutElement child) => child.GetSize().Height;
        protected override float GetChildSizeSecondary(ILayoutElement child) => child.GetSize().Width;

        protected override float GetContainerDimensionMain(Size containerDimensions) => containerDimensions.Height;
        protected override float GetContainerDimensionSecondary(Size containerDimensions) => containerDimensions.Width;

        protected override Vector2[] GetFinalPositions(float[] mainMargins, float[] secondaryMargins, float[] mainDimensions)
        {
            Vector2[] positions = new Vector2[mainDimensions.Length];
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
                sizes[i] = new Size(secondaryDimensions[i], mainDimensions[i]);
            return sizes;
        }
    }
}
