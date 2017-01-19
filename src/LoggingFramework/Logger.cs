using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace LoggingFramework
{
    public class Logger : IDisposable
    {
        private readonly Dictionary<int, List<Level>> _configurations;
        private readonly Level _minimumLevel;
        private readonly string _logPath;
        // Flag: Has Dispose already been called?
        private bool disposed = false;
        public enum Level
        {
            [DisplayName("DEB")]
            Debug = 0,
            [DisplayName("INF")]
            Info,
            [DisplayName("ERR")]
            Error
        }

        public Logger(string logPath, Level level)
        {
            _minimumLevel = level;
            _configurations = new Dictionary<int, List<Level>>();
            _configurations.Add((int)Level.Debug, new List<Level> { Level.Debug, Level.Info, Level.Error });
            _configurations.Add((int)Level.Info, new List<Level> { Level.Info, Level.Error });
            _configurations.Add((int)Level.Error, new List<Level> { Level.Error });
            _logPath = logPath;
            const string message = "Logger Initialised";
            Log(message);
            AdjustLoggers();
            InitialiseFileLogger();
        }

        private void AdjustLoggers()
        {
            var levelLoggers = _configurations.FirstOrDefault(x => x.Key == (int)_minimumLevel).Value;
            foreach (var configuration in _configurations)
            {
                configuration.Value.RemoveAll(x => !levelLoggers.Contains(x));
            }
        }

        private void InitialiseFileLogger()
        {
            if (!File.Exists(_logPath))
                File.CreateText(_logPath);
        }

        public void Debug(string message)
        {
            const Level level = Level.Debug;
            var isLevelAllowed = IsLevelAllowed(level);
            if (!isLevelAllowed) return;
            Log($"{level.GetDisplayName()} {message}");
        }

        public void Info(string message)
        {
            const Level level = Level.Info;
            var isLevelAllowed = IsLevelAllowed(level);
            if (!isLevelAllowed) return;
            Log($"{level.GetDisplayName()} {message}");
        }

        public void Error(string message)
        {
            const Level level = Level.Error;
            var isLevelAllowed = IsLevelAllowed(level);
            if (!isLevelAllowed) return;
            Log($"{level.GetDisplayName()} {message}");
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                const string message = "Logger Destroyed";
                Log(message);
            }
            disposed = true;
        }

        private bool IsLevelAllowed(Level level)
        {
            return _configurations.Any(x => x.Key == (int)level && x.Value.Any(a => a == level));
        }
        private void Log(string message)
        {
            using (StreamWriter sw = File.AppendText(_logPath))
            {
                sw.WriteLine($"{DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss")} {message}");
            }
        }

    }
}
