using FLG.Cs.Datamodel.UI.Layouts;


namespace FLG.Cs.Datamodel.UI.Widgets.Text
{
    public interface IText : ILayoutElement
    {
        string Content { get; set; }
        public ETextAlignHorizontal AlignHorizontal { get; }
        public ETextAlignVertical AlignVertical { get; }

        public event EventHandler TextChanged;
    }
}
