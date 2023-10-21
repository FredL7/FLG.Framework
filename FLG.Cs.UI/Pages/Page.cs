namespace FLG.Cs.UI.Pages {
    internal abstract class Page : IPage {
        private EPageStatus _status;

        private uint _layoutId;
        internal uint LayoutId { get => _layoutId; }

        string _name;
        public string GetName() => _name;

        #region Observer
        private List<IPageObserver> _observers;
        public void AddObserver(IPageObserver observer)
        {
            _observers.Add(observer);
        }
        #endregion Observer

        internal Page(uint layoutId, string name)
        {
            _status = EPageStatus.CLOSED;
            _layoutId = layoutId;
            _observers = new();
            _name = name;
        }

        internal void Open()
        {
            if (_status == EPageStatus.CLOSED)
            {
                _status = EPageStatus.OPEN;
                foreach (var o in _observers)
                    o.OnPageOpen();
            }
        }

        internal void Close()
        {
            if (_status == EPageStatus.OPEN)
            {
                _status = EPageStatus.CLOSED;
                foreach (var o in _observers)
                    o.OnPageClose();
            }
        }
    }
}
