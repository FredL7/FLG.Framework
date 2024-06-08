namespace FLG.Cs.Datamodel {
    public enum ETextAlignHorizontal {
        LEFT, CENTER, RIGHT
    }

    public class ETextAlignHorizontalExtension {
        public static ETextAlignHorizontal FromString(string value)
        {
            return value.ToLower() switch
            {
                "left" => ETextAlignHorizontal.LEFT,
                "center" => ETextAlignHorizontal.CENTER,
                "right" => ETextAlignHorizontal.RIGHT,
                _ => ETextAlignHorizontal.LEFT,
            };
        }
    }

    public enum ETextAlignVertical {
        TOP, CENTER, BOTTOM
    }

    public class ETextAlignVerticalExtension {
        public static ETextAlignVertical FromString(string value)
        {
            return value.ToLower() switch
            {
                "top" => ETextAlignVertical.TOP,
                "center" => ETextAlignVertical.CENTER,
                "bottom" => ETextAlignVertical.BOTTOM,
                _ => ETextAlignVertical.TOP,
            };
        }
    }
}
