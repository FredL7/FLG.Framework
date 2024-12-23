using System.Xml.Serialization;

using FLG.Cs.Datamodel.Framework;
using FLG.Cs.Datamodel.Logger;
using FLG.Cs.Datamodel.Networking;

using FLG.Cs.Framework;
using FLG.Cs.ServiceLocator;


namespace LogsViewer {
    public class ProgramLogsViewer {
        private static bool stop = false;

        private const string _logsDir = @"../../../../../_logs"; // TODO: command with callback to get the value from the server

        static void Main(string[] _)
        {
            Console.Title = "Logs Viewer";

            PreferencesFramework prefs = new()
            {
                logs = new() { identifier = "logs watcher", types = [ELoggerType.NETWORKING] },
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

            string file = GetFileToWatch();
            Console.WriteLine(file);
            LogsWatcher logsWatcher = new(file);
            FileWatcher fileWatcher = new(_logsDir, Path.GetFileName(file));
            fileWatcher.FileWritten += (sender, e) =>
            {
                logsWatcher.OnFileChanged();
            };
            fileWatcher.StartWatching();

            Console.WriteLine("Press a key to stop the logs viewer");
            Console.ReadKey();
            stop = true;
            client.Disconnect();
            fileWatcher.StopWatching();
        }

        private static string GetFileToWatch()
        {
            var files = Directory.GetFiles(_logsDir);
            var latest = files.OrderBy(x => Path.GetFileNameWithoutExtension(x)).Reverse().FirstOrDefault();
            return latest ?? string.Empty;
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

        private class FileWatcher {
            private readonly FileSystemWatcher _watcher;

            public event EventHandler<FileSystemEventArgs>? FileWritten;

            public FileWatcher(string dir, string file)
            {
                _watcher = new(dir)
                {
                    Filter = file,
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.Size
                };

                _watcher.Changed += OnFileChanged;
            }

            public void StartWatching()
            {
                _watcher.EnableRaisingEvents = true;
            }

            public void StopWatching()
            {
                _watcher.EnableRaisingEvents = false;
            }

            public void OnFileChanged(object sender, FileSystemEventArgs e)
            {
                FileWritten?.Invoke(this, e);
            }

            public void Dispose()
            {
                _watcher.Dispose();
            }
        }

        private class LogsWatcher {
            private readonly string _fileToWatch;
            private readonly XmlSerializer _serializer;
            private LogEntries _entries;

            public LogsWatcher(string file)
            {
                _fileToWatch = file;
                _serializer = new(typeof(LogEntries));
                _entries = ReadFileContent();
                WriteEntries(_entries, 0);
            }

            public void OnFileChanged()
            {
                lock (_entries)
                {
                    var content = ReadFileContent();
                    var lenPrevious = _entries.Entries.Count;
                    WriteEntries(content, lenPrevious);
                    _entries = content;
                }
            }

            private static void WriteEntries(LogEntries content, int lenPrevious)
            {
                var lenNew = content.Entries.Count;
                for (int i = lenPrevious; i < lenNew; ++i)
                {
                    Console.WriteLine(content.Entries[i].ToPrettyString());
                }
            }

            private LogEntries ReadFileContent()
            {
                using FileStream fs = new(_fileToWatch, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using StreamReader reader = new(fs);
                return _serializer.Deserialize(reader) as LogEntries ?? new();
            }
        }
        /*
        private class LogsWatcher {
            private const string _logsDir = @"../../../../../_logs"; // TODO: command with callback to get the value from the server
            private readonly string _fileToWatch;

            private readonly FileSystemWatcher _watcher;
            private readonly XmlSerializer _serializer;
            private LogEntries _entries;

            private static string GetFileToWatch()
            {
                var files = Directory.GetFiles(_logsDir);
                var latest = files.OrderBy(x => Path.GetFileNameWithoutExtension(x)).FirstOrDefault();
                return latest ?? string.Empty;
            }

            public LogsWatcher()
            {
                _fileToWatch = GetFileToWatch();
                if (string.IsNullOrEmpty(_fileToWatch))
                {
                    Console.WriteLine("No file to watch");
                }

                _serializer = new(typeof(LogEntries));
                _entries = ReadFileContent();
                WriteEntries(_entries, 0);

                _watcher = new(_logsDir)
                {
                    Filter = _fileToWatch,
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.Size
                };

                _watcher.Changed += OnFileChanged;
                // _watcher.Created += OnFileChanged;
                // _watcher.Deleted += OnFileChanged;

                _watcher.EnableRaisingEvents = true;
            }

            public void Stop()
            {
                _watcher.EnableRaisingEvents = false;
            }

            private void OnFileChanged(object sender, FileSystemEventArgs e)
            {
                var content = ReadFileContent();
                var lenPrevious = _entries.Entries.Count;
                WriteEntries(content, lenPrevious);
                _entries = content;
            }

            private static void WriteEntries(LogEntries content, int lenPrevious)
            {
                var lenNew = content.Entries.Count;
                for (int i = lenPrevious; i < lenNew; ++i)
                {
                    Console.WriteLine(content.Entries[i].ToPrettyString());
                }
            }

            private LogEntries ReadFileContent()
            {
                if (File.Exists(_fileToWatch))
                {
                    using StreamReader reader = new(_fileToWatch);
                    _entries = _serializer.Deserialize(reader) as LogEntries ?? new();
                    return _entries;
                }

                return new();
            }
        }
        */
    }
}
