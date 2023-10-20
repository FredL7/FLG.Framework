using FLG.Cs.Math;
using FLG.Cs.UI.Layouts;

using System.Diagnostics;
using System.Numerics;
using System.Xml;

namespace FLG.Cs.UI.Grid {
    internal abstract class Stack : AbstractLayoutElementComposite {
        private EGridDirection _direction;
        private EGridJustify _justify; // Along the main direction
        private EGridAlignment _alignment; // Along the other direction

        internal Stack(RectXform rectXform, Size size, int order, float weight,
            EGridDirection direction, EGridJustify justify, EGridAlignment alignment)
            : base(rectXform, size, order, weight)
        {
            _direction = direction;
            _justify = justify;
            _alignment = alignment;
        }

        internal Stack(XmlNode node) : base(node) {
            _direction = LayoutXMLParser.GetDirection(node);
            _justify = LayoutXMLParser.GetJustify(node);
            _alignment = LayoutXMLParser.GetAlignment(node);
        }

        protected sealed override void ComputeChildrenSizesAndPositions(Size parentDimensions)
        {
            var childrens = GetChildrensInOrder();
            if (childrens.Length == 0)
                return;

            var mainDimensionsAndMargins = ComputeDimensionsAndMarginsAlongMainAxis(childrens);
            var mainDimensions = mainDimensionsAndMargins.Item1;
            var mainMargins = mainDimensionsAndMargins.Item2;

            var secondaryDimensionsAndMargins = ComputeDimensionsAndMarginsAlongSecondaryAxis(childrens);
            var secondaryDimensions = secondaryDimensionsAndMargins.Item1;
            var secondaryMargins = secondaryDimensionsAndMargins.Item2;

            Size[] sizes = GetFinalSizes(mainDimensions, secondaryDimensions);
            Vector2[] positions = GetFinalPositions(mainMargins, secondaryMargins, mainDimensions);

            for (int i = 0; i < childrens.Length; ++i)
                childrens[i].RectXform.SetSizesAndPosition(sizes[i], positions[i]);
        }

        protected abstract float GetChildSizeMain(AbstractLayoutElement child);
        protected abstract float GetChildSizeSecondary(AbstractLayoutElement child);
        protected abstract float GetChildMainMarginFirst(AbstractLayoutElement child);
        protected abstract float GetChildMainMarginLast(AbstractLayoutElement child);
        protected abstract float GetChildSecondaryMarginFirst(AbstractLayoutElement child);
        protected abstract float GetChildSecondaryMarginLast(AbstractLayoutElement child);
        protected abstract float GetContainerDimensionMain(Size containerDimensions);
        protected abstract float GetContainerDimensionSecondary(Size containerDimensions);
        protected abstract Size[] GetFinalSizes(float[] mainDimensions, float[] secondaryDimensions);
        protected abstract Vector2[] GetFinalPositions(float[] mainMargins, float[] secondaryMargins, float[] mainDimensions);

        private AbstractLayoutElement[] GetChildrensInOrder()
        {
            var childrens = GetChildrensInternal();
            var childrensOrdered = childrens.OrderBy(x => x.Order);
            if (_direction == EGridDirection.REVERSE)
                return childrensOrdered.Reverse().ToArray();
            return childrensOrdered.ToArray();
        }

        private (float[], float[]) ComputeDimensionsAndMarginsAlongMainAxis(AbstractLayoutElement[] childrens)
        {
            var expectedSizes = GetExpectedSizes(childrens);
            var expectedSizesSum = expectedSizes.Sum();

            var margins = ComputeMargins(childrens);
            var marginsSum = margins.Sum();

            var containerDimensions = RectXform.GetDimensions();
            var containerDimensionMain = GetContainerDimensionMain(containerDimensions);
            var spaceRequired = expectedSizesSum + marginsSum;
            var spaceAvailable = containerDimensionMain - spaceRequired;
            Debug.Assert(spaceAvailable >= 0);

            var stretchedSizes = GetSizesForStretch(childrens, expectedSizes, spaceAvailable);
            var stretchedSizeSum = stretchedSizes.Sum();
            var stretchedSpaceRequired = stretchedSizeSum + marginsSum;
            var spaceAvailableAfterStretch = containerDimensionMain - stretchedSpaceRequired;
            Debug.Assert(spaceAvailableAfterStretch >= 0);

            var justifiedMargins = UpdateMarginsForJustify(margins, spaceAvailableAfterStretch);
            var justifiedMarginsSum = justifiedMargins.Sum();
            var justifiedSpaceRequired = stretchedSizeSum + justifiedMarginsSum;
            var spaceAvailableAfterJustify = containerDimensionMain - justifiedSpaceRequired;
            Debug.Assert(MathF.Abs(spaceAvailableAfterJustify) < float.Epsilon);

            return (stretchedSizes, justifiedMargins);
        }

        private (float[], float[]) ComputeDimensionsAndMarginsAlongSecondaryAxis(AbstractLayoutElement[] childrens)
        {
            var containerDimensions = RectXform.GetDimensions();
            var containerDimensionSecondary = GetContainerDimensionSecondary(containerDimensions);

            float[] margins = new float[childrens.Length];
            for (int i = 0; i < margins.Length; ++i) // TODO: Validate
            {
                var secondaryMarginFirst = GetChildSecondaryMarginFirst(childrens[i]);
                var secondaryMarginLast = GetChildSecondaryMarginLast(childrens[i]);
            }


            float[] dimensions = new float[childrens.Length];

            for (int i = 0; i < childrens.Length; ++i)
            {
                var secondaryMarginFirst = GetChildSecondaryMarginFirst(childrens[i]);
                var secondaryMarginLast = GetChildSecondaryMarginLast(childrens[i]);
                var dimension = GetChildSizeSecondary(childrens[i]);
                var dimensionStretched = containerDimensionSecondary - (secondaryMarginFirst + secondaryMarginLast);
                if (dimension == 0)
                    dimension = dimensionStretched;
                dimensions[i] = dimension;

                switch (_alignment)
                {
                    case EGridAlignment.START:
                        margins[i] = secondaryMarginFirst;
                        break;
                    case EGridAlignment.END:
                        margins[i] = containerDimensionSecondary - (dimension + secondaryMarginLast);
                        break;
                    case EGridAlignment.CENTER:
                        var spaceRemaining = containerDimensionSecondary - dimension;
                        margins[i] = spaceRemaining / 2;
                        break;
                    case EGridAlignment.STRETCH:
                        margins[i] = secondaryMarginFirst;
                        dimensions[i] = dimensionStretched;
                        break;
                }
            }

            return (dimensions, margins);
        }

        private float[] GetExpectedSizes(AbstractLayoutElement[] childrens)
        {
            float[] sizes = new float[childrens.Length];
            for (int i = 0; i < childrens.Length; ++i)
                sizes[i] = GetChildSizeMain(childrens[i]);
            return sizes;
        }

        private float[] ComputeMargins(AbstractLayoutElement[] childrens)
        {
            float[] margins = new float[childrens.Length + 1];
            margins[0] = GetChildMainMarginFirst(childrens[0]);
            margins[^1] = GetChildMainMarginLast(childrens[^1]);
            if (margins.Length > 1)
            {
                for (int i = 1; i < childrens.Length; ++i)
                {
                    float a = GetChildMainMarginLast(childrens[i - 1]);
                    float b = GetChildMainMarginFirst(childrens[i]);
                    float max = MathF.Max(a, b);
                    margins[i] = max;
                }
            }
            return margins;
        }

        private float[] GetSizesForStretch(AbstractLayoutElement[] childrens, float[] expectedSizes, float spaceAvailable)
        {
            float[] sizesForStretch = expectedSizes;
            bool hasStretch = expectedSizes.Contains(0f);
            if (hasStretch)
            {
                float weightSum = 0;
                foreach (var child in childrens)
                    if (GetChildSizeMain(child) == 0f)
                        weightSum += child.Weight;
                float stretchSizeSingle = spaceAvailable / weightSum;
                for (int i = 0; i < childrens.Length; ++i)
                    if (GetChildSizeMain(childrens[i]) == 0f)
                        sizesForStretch[i] = childrens[i].Weight * stretchSizeSingle;
            }
            return sizesForStretch;
        }

        private float[] UpdateMarginsForJustify(float[] margins, float spaceAvailable)
        {
            float[] justifiedMargin = margins;
            switch (_justify)
            {
                case EGridJustify.START:
                    break;
                case EGridJustify.END:
                    justifiedMargin[0] += spaceAvailable;
                    break;
                case EGridJustify.CENTER:
                    justifiedMargin[0] += spaceAvailable / 2f;
                    break;
                case EGridJustify.SPACE_BETWEEN:
                    var spaceBetween = spaceAvailable / (justifiedMargin.Length - 1);
                    for (int i = 0; i < justifiedMargin.Length; ++i)
                        justifiedMargin[i] += spaceBetween;
                    break;
                case EGridJustify.SPACE_AROUND:
                    var spaceAround = spaceAvailable / (2 * justifiedMargin.Length);
                    justifiedMargin[0] += spaceAround;
                    for (int i = 1; i < justifiedMargin.Length; ++i)
                        justifiedMargin[i] += 2 * spaceAround;
                    break;
                case EGridJustify.SPACE_EVENLY:
                    var spaceEvenly = spaceAvailable / (justifiedMargin.Length + 1);
                    for (int i = 0; i < justifiedMargin.Length; ++i)
                        justifiedMargin[i] += spaceEvenly;
                    break;
            }
            return justifiedMargin;
        }

        private float GetSecondaryMaxMarginFirst(AbstractLayoutElement[] childrens)
        {
            return childrens.Max(x => GetChildSecondaryMarginFirst(x));
        }

        private float GetSecondaryMaxMarginLast(AbstractLayoutElement[] childrens)
        {
            return childrens.Max(x => GetChildSecondaryMarginLast(x));
        }
    }
}
