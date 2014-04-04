using System;
using System.Collections.Generic;
using System.IO;

namespace OOC.Util
{
    public enum LogLevel {
        DEBUG, INFO, WARN, CRIT
    }

    public class Logger
    {
        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                writer = new StreamWriter(path);
            }
        }

        private object mutex = new object();

        private string path;

        private StreamWriter writer;

        public Logger(string path)
        {
            Path = path;
        }

        public void Write(LogLevel level, string message)
        {
            string line = "[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "] ";
            line += level.ToString() + " | " + message;
            lock (mutex)
            {
                writer.WriteLine(line);
                writer.Flush();
            }
        }
    }
}