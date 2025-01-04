namespace FLG.Cs.Datamodel.UI.Grids {
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

    public enum EGridDirection { NORMAL, REVERSE }
    public class EGridDirectionExtension {
        public static EGridDirection FromString(string value)
        {
            return value.ToLower() switch
            {
                "normal" => EGridDirection.NORMAL,
                "reverse" => EGridDirection.REVERSE,
                _ => EGridDirection.NORMAL,
            };
        }
    }

    public enum EGridJustify { START, END, CENTER, SPACE_BETWEEN, SPACE_AROUND, SPACE_EVENLY }
    public class EGridJustifyExtension {
        public static EGridJustify FromString(string value)
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
