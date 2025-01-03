using FLG.Cs.Datamodel.Framework;
using FLG.Cs.Datamodel.Logger;
using FLG.Cs.Datamodel.Networking;

using FLG.Cs.Framework;
using FLG.Cs.ServiceLocator;


namespace ServerExecutable {
    class ProgramServer {
        private static bool stop = false;

        static void Main(string[] _)
        {
            Console.Title = "Server";

            PreferencesFramework prefs = new()
            {
                logs = new() { identifier = "server sample", types = [ELoggerType.CONSOLE, ELoggerType.WRITE_FILE], dir = "../../../../../_logs" },
                networking = new() { clientType = ENetworkingClientType.SERVER }
            };
            var result = FrameworkManager.Instance.Initialize(prefs);
            if (!result)
            {
                Console.WriteLine(result.GetMessage());
            }

            var server = Locator.Instance.Get<INetworkingManagerServer>();
            server.Start(26501);

            Thread updateThread = new(Update);
            updateThread.Start();

            Console.WriteLine("Press a key to stop the server");
            Console.ReadKey();
            stop = true;
            server.Stop();
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
