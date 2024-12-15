using FLG.Cs.Datamodel.UI.Layouts;


namespace FLG.Cs.Datamodel.UI.Widgets.Text
{
    public interface ILabel : ILayoutElement
    {
        string Text { get; }
        public ETextAlignHorizontal AlignHorizontal { get; }
        public ETextAlignVertical AlignVertical { get; }
    }
}
