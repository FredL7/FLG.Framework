namespace FLG.Cs.Datamodel {
    public interface IPage {
        public string PageId { get; }
        public string LayoutId { get; set; }
        public void Setup(IUIManager ui, IUIFactory factory);

        public void OnRegister();
        public void OnOpen();
        public void OnClose();
    }
}
