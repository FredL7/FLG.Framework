using FLG.Cs.Logger;
using FLG.Cs.UI.Layouts;

namespace FLG.Cs.UI.Pages {
    internal class Page : IPage {
        private EPageStatus _status;

        private string _layoutId;
        internal string LayoutId { get => _layoutId; }

        private Dictionary<string, List<AbstractLayoutElement>> _content;
        internal IEnumerable<string> GetTargetsId() => _content.Keys;
        internal List<AbstractLayoutElement> GetContentElements(string targetid)
        {
            if (!_content.ContainsKey(targetid))
                LogManager.Instance.Error($"Page {_name} does not contain content for target {targetid}");
            return _content[targetid];
        }

        readonly string _name;
        public string GetName() => _name;

        #region Observer
        private List<IPageObserver> _observers;
        public void AddObserver(IPageObserver observer)
        {
            _observers.Add(observer);
        }
        #endregion Observer

        internal Page(string layoutId, string name, Dictionary<string, List<AbstractLayoutElement>> content)
        {
            _status = EPageStatus.CLOSED;
            _layoutId = layoutId;
            _observers = new();
            _name = name;
            _content = content;
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
