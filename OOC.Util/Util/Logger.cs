using System;
using System.Collections.Generic;
using System.IO;

namespace OOC.Util
{
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

        private string path;

        private StreamWriter writer;

        public Logger(string path)
        {
        }
    }
}