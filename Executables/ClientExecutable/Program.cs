using FLG.Cs.Datamodel.Framework;
using FLG.Cs.Datamodel.Logger;
using FLG.Cs.Datamodel.Networking;

using FLG.Cs.Framework;
using FLG.Cs.ServiceLocator;


namespace ClientExecutable {
    class ProgramClient {
        private static bool stop = false;

        static void Main(string[] args)
        {
            Console.Title = "Client";

            PreferencesFramework prefs = new()
            {
                logs = new() { identifier = "client sample", types = [ELoggerType.NETWORKING, ELoggerType.CONSOLE] },
                networking = new() { clientType = ENetworkingClientType.CLIENT }
            };
            var result = FrameworkManager.Instance.Initialize(prefs);
            if (!result)
            {
                Console.WriteLine(result.GetMessage());
            }

            var client = Locator.Instance.Get<INetworkingManagerClient>();
            client.Connect("127.0.0.1", 26501);

            Thread updateThread = new(Update);
            updateThread.Start();

            Console.WriteLine("Press a key to stop the client");
            Console.ReadKey();
            stop = true;
            client.Disconnect();
            updateThread.Join();
        }

        private static void Update()
        {
            const int targetFPS = 30;
            const int frameTime = 1000 / targetFPS;

            while (!stop)
            {
                DateTime frameStartTime = DateTime.Now;

                FrameworkManager.Instance.Update();

                int workTime = (int)(DateTime.Now - frameStartTime).TotalMilliseconds;
                int sleepTime = frameTime - workTime;

                if (sleepTime > 0)
                {
                    Thread.Sleep(sleepTime);
                }
            }
        }
    }
}
