using Godot;

using FLG.Cs.Datamodel.UI.Widgets.Text;


namespace FLG.Godot.Helpers {
    public static class TextAlignmentConverter {
        public static HorizontalAlignment Horizontal(ETextAlignHorizontal textAlign)
        {
            return textAlign switch
            {
                ETextAlignHorizontal.LEFT => HorizontalAlignment.Left,
                ETextAlignHorizontal.CENTER => HorizontalAlignment.Center,
                ETextAlignHorizontal.RIGHT => HorizontalAlignment.Right,
                _ => throw new ArgumentException($"{textAlign} is not valid"),
            };
        }

        public static VerticalAlignment Vertical(ETextAlignVertical textAlign)
        {
            return textAlign switch
            {
                ETextAlignVertical.TOP => VerticalAlignment.Top,
                ETextAlignVertical.CENTER => VerticalAlignment.Center,
                ETextAlignVertical.BOTTOM => VerticalAlignment.Bottom,
                _ => throw new ArgumentException($"{textAlign} is not valid"),
            };
        }
    }
}
