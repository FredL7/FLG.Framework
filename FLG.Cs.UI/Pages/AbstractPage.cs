namespace FLG.Cs.UI.Pages {
    public abstract class AbstractPage {
        private EPageStatus _status;

        private uint _layoutId;
        internal uint LayoutId { get => _layoutId; }

        public AbstractPage(uint layoutId)
        {
            _status = EPageStatus.CLOSED;
            _layoutId = layoutId;
        }

        internal void Open()
        {
            if (_status == EPageStatus.CLOSED)
            {
                _status = EPageStatus.OPEN;
                OnPageOpen();
            }
        }
        protected void OnPageOpen() { }

        internal void Close()
        {
            if (_status == EPageStatus.OPEN)
            {
                _status = EPageStatus.CLOSED;
                OnPageClosed();
            }
        }
        protected void OnPageClosed() { }
    }
}
