using UnityEngine;

using FLG.Cs.Datamodel.UI.Widgets.Text;


namespace FLG.Unity.Helpers {
    public static class TextAlignmentConverter {
        public static TextAnchor ToAnchor(ETextAlignHorizontal hAlign, ETextAlignVertical vAlign)
        {
            var alignment = (hAlign, vAlign); //? Is there a reason to have the separate
            return alignment switch
            {
                (ETextAlignHorizontal.LEFT, ETextAlignVertical.TOP) => TextAnchor.UpperLeft,
                (ETextAlignHorizontal.LEFT, ETextAlignVertical.CENTER) => TextAnchor.MiddleLeft,
                (ETextAlignHorizontal.LEFT, ETextAlignVertical.BOTTOM) => TextAnchor.LowerLeft,

                (ETextAlignHorizontal.CENTER, ETextAlignVertical.TOP) => TextAnchor.UpperCenter,
                (ETextAlignHorizontal.CENTER, ETextAlignVertical.CENTER) => TextAnchor.MiddleCenter,
                (ETextAlignHorizontal.CENTER, ETextAlignVertical.BOTTOM) => TextAnchor.LowerCenter,

                (ETextAlignHorizontal.RIGHT, ETextAlignVertical.TOP) => TextAnchor.UpperRight,
                (ETextAlignHorizontal.RIGHT, ETextAlignVertical.CENTER) => TextAnchor.MiddleRight,
                (ETextAlignHorizontal.RIGHT, ETextAlignVertical.BOTTOM) => TextAnchor.LowerRight,

                _ => throw new ArgumentException("Invalid combinaison of alignements"),
            };
        }
    }
}
