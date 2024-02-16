namespace FLG.Cs.UI.Grids {
    public enum EGridAlignment { START, END, CENTER, STRETCH }

    internal class EGridAlignmentExtension {
        internal static EGridAlignment FromString(string value)
        {
            return value.ToLower() switch
            {
                "start" => EGridAlignment.START,
                "end" => EGridAlignment.END,
                "center" => EGridAlignment.CENTER,
                "stretch" => EGridAlignment.STRETCH,
                _ => EGridAlignment.STRETCH,
            };
        }
    }
}
