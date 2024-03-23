namespace FLG.Cs.IDatamodel {
    public interface IPage {
        public string PageId { get; }
        public string LayoutId { get; set; }
        public void Setup(IUIManager ui, IUIFactory factory);

        public void OnOpen();
        public void OnClose();
    }
}
