using FLG.Cs.Math;
using FLG.Cs.UI.Grid;
using FLG.Cs.UI.Layouts;
using System.Xml;


namespace FLG.Cs.UI {
    public static class UILibrary {
        internal static AbstractLayoutElement? Xml(XmlNode node, string name)
        {
            var nodeType = node.Name;
            return nodeType switch
            {
                "HStack" => new HStack(name, node),
                "VStack" => new VStack(name, node),
                "ProxyLayoutElement" => new ProxyLayoutElementLeaf(name, node),// TODO: tmp
                _ => null,
            };
        }

        /* ============================= *
         * ========== layouts ========== *
         * ============================= */

        public static void HStack(
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
        {
            HStack hstack = new(name, width, height, margin, padding, order, weight, isTarget, direction, justify, alignment);
        }

        public static void VStack(
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
        {
            VStack vstack = new(name, width, height, margin, padding, order, weight, isTarget, direction, justify, alignment);
        }


        /* ============================= *
         * ========== Widgets ========== *
         * ============================= */

        public static void Button(string name)
        {

        }
    }
}
