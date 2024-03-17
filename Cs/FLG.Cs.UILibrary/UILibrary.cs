using System.Xml;

using FLG.Cs.Math;
using FLG.Cs.UI.Grids;
using FLG.Cs.UI.Layouts;
using FLG.Cs.UI.Widgets;

namespace FLG.Cs.UI
{
    public static class UILibrary {
        internal static AbstractLayoutElement? Xml(XmlNode node, string name)
        {
            var nodeType = node.Name;
            return nodeType switch
            {
                "HStack" => new HStack(name, node),
                "VStack" => new VStack(name, node),
                "ProxyLayoutElement" => new ProxyLayoutElementLeaf(name, node),
                _ => null,
            };
        }

        /* ============================= *
         * ========== layouts ========== *
         * ============================= */

        public static HStack HStack(
            string name,
            float width = 0,
            float height = 0,
            Spacing margin = default,
            Spacing padding = default,
            int order = 0,
            float weight = 1f,
            bool isTarget = false,
            EGridDirection direction = EGridDirection.NORMAL,
            EGridJustify justify = EGridJustify.START,
            EGridAlignment alignment = EGridAlignment.START)
                => new(name, width, height, margin, padding, order, weight, isTarget, direction, justify, alignment);

        public static VStack VStack(
            string name,
            float width = 0,
            float height = 0,
            Spacing margin = default,
            Spacing padding = default,
            int order = 0,
            float weight = 1f,
            bool isTarget = false,
            EGridDirection direction = EGridDirection.NORMAL,
            EGridJustify justify = EGridJustify.START,
            EGridAlignment alignment = EGridAlignment.START)
                => new(name, width, height, margin, padding, order, weight, isTarget, direction, justify, alignment);


        /* ============================= *
         * ========== Widgets ========== *
         * ============================= */

        public static ProxyLayoutElementLeaf ProxyLayoutElement(
            string name,
            float width = 0,
            float height = 0,
            Spacing margin = default,
            Spacing padding = default,
            int order = 0,
            float weight = 1f,
            bool isTarget = false) // TODO: Leaf element can't be target?
            => new(name, width, height, margin, padding, order, weight, isTarget);
    }
}
