namespace FLG.Cs.UI.Grid {
    internal enum EGridJustify { START, END, CENTER, SPACE_BETWEEN, SPACE_AROUND, SPACE_EVENLY }

    internal class EGridJustifyExtension {
        internal static EGridJustify FromString(string value)
        {
            return value.ToLower() switch
            {
                "start" => EGridJustify.START,
                "end" => EGridJustify.END,
                "center" => EGridJustify.CENTER,
                "space-between" => EGridJustify.SPACE_BETWEEN,
                "space-around" => EGridJustify.SPACE_AROUND,
                "space-evenly" => EGridJustify.SPACE_EVENLY,
                _ => EGridJustify.START,
            };
        }
    }
}
