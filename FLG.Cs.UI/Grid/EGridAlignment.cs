namespace FLG.Cs.UI.Grid {
    public enum EGridAlignment { START, END, CENTER, STRETCH }

    public class EGridAlignmentExtension {
        public static EGridAlignment FromString(string value)
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
