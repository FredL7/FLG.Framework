using FLG.Cs.Datamodel.Framework;
using FLG.Cs.Datamodel.Logger;

using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Networking {
    internal class ThreadManager : IGameLoopObject {
        private readonly List<Action> _actionQueue;
        private readonly List<Action> _actionBuffer;
        private bool _actionsAvailable = false;

        public ThreadManager()
        {
            _actionQueue = [];
            _actionBuffer = [];
        }

        public void ExecuteOnMainThread(Action action)
        {
            if (action == null)
            {
                Locator.Instance.Get<ILogManager>().Debug("No action to execute on main thread");
                return;
            }

            lock (_actionQueue)
            {
                _actionQueue.Add(action);
                _actionsAvailable = true;
            }
        }

        public void Update()
        {
            if (_actionsAvailable)
            {
                _actionBuffer.Clear();
                lock (_actionQueue)
                {
                    _actionBuffer.AddRange(_actionQueue);
                    _actionQueue.Clear();
                    _actionsAvailable = false;
                }

                foreach (var action in _actionBuffer)
                {
                    action();
                }
            }
        }
    }
}
