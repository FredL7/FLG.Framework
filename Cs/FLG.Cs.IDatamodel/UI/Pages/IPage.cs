namespace FLG.Cs.IDatamodel {
    public interface IPage {
        public string PageId { get; }
        public string LayoutId { get; set; }
        public void Setup(IUIFactory factory);

        public void OnOpen();
        public void OnClose();
    }
}
