using FLG.Cs.Math;
using FLG.Cs.UI.Grid;

namespace FLG.Cs.UI {
    public static class UILibrary {
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
            // TODO: Add result to pipeline and notify Front-end UI
            // Also change it so ctor goes through here with XMLNode instead of directly using new?
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
            // TODO: Add result to pipeline and notify Front-end UI
        }


        /* ============================= *
         * ========== Widgets ========== *
         * ============================= */

        public static void Button(string name)
        {

        }

        // TODO: Others
    }
}
