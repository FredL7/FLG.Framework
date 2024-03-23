using FLG.Cs.IDatamodel;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
