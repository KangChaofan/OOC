using System;
using System.Collections.Generic;
using System.IO;

namespace OOC.Util
{
    public enum LogLevel
    {
        DEBUG = 0, INFO = 1, WARN = 2, ERROR = 3, CRIT = 4
    }

    public class Logger : IDisposable
    {
        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                if (path == null) writer = null;
                else writer = new StreamWriter(path);
            }
        }

        public LogLevel MinimumLevel { get; set; }

        private readonly object mutex = new object();

        private string path;

        private StreamWriter writer;

        public Logger() { }

        public Logger(string path)
        {
            MinimumLevel = LogLevel.DEBUG;
            Path = path;
        }

        public void Write(LogLevel level, string message)
        {
            if (level < MinimumLevel) return;
            string line = "[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "] ";
            line += level.ToString() + " | " + message;
            lock (mutex)
            {
                Console.WriteLine(line);
                if (writer != null)
                {
                    writer.WriteLine(line);
                    writer.Flush();
                }
            }
        }

        public void Debug(string message) { Write(LogLevel.DEBUG, message); }

        public void Info(string message) { Write(LogLevel.INFO, message); }

        public void Warn(string message) { Write(LogLevel.WARN, message); }

        public void Error(string message) { Write(LogLevel.ERROR, message); }

        public void Crit(string message) { Write(LogLevel.CRIT, message); }

        public void Dispose()
        {
            if (writer != null)
            {
                writer.Close();
                writer.Dispose();
            }
        }
    }
}