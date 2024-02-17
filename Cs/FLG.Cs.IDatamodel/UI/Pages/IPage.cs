namespace FLG.Cs.IDatamodel {
    public interface IPage {
        public string GetPageId();
        public string GetLayoutId();
        public void SetLayoutId(string layoutId);
        public void Setup();
    }
}
