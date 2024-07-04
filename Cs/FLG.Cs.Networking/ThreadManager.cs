namespace FLG.Cs.Networking {
    internal class ThreadManager {
        private bool _actionToExecuteOnMainThread = false;
        private readonly List<Action> _executeOnMainThreadReceive = new();
        private readonly List<Action> _executeOnMainThreadExecute = new();
        public void ExecuteOnMainThread(Action action)
        {
            if (action == null)
            {
                return;
            }

            lock (_executeOnMainThreadReceive)
            {
                _executeOnMainThreadReceive.Add(action);
                _actionToExecuteOnMainThread = true;
            }
        }

        public void Update()
        {
            if (_actionToExecuteOnMainThread)
            {
                _executeOnMainThreadExecute.Clear();
                lock (_executeOnMainThreadReceive)
                {
                    _executeOnMainThreadExecute.AddRange(_executeOnMainThreadReceive);
                    _executeOnMainThreadReceive.Clear();
                    _actionToExecuteOnMainThread = false;
                }

                foreach (var action in _executeOnMainThreadExecute)
                {
                    action();
                }
            }
        }
    }
}
