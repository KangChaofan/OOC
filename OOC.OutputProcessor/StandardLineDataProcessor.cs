<<<<<<< HEAD
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace OOC.OutputProcessor
{
    public class StandardLineDataProcessor : OutputDataProcessor
    {
        protected StreamReader _sr;

        protected string streamKey;

        // Set input stream of data source with specified key
        public virtual void SetDataReader(string key, Stream stream)
        {
            if (streamKey == null || streamKey == key)
                _sr = new StreamReader(stream);
        }

        // Determinte whether there is next data record according to current input
        public virtual bool HasNextRecord()
        {
            if (_sr == null) return false;
            return !_sr.EndOfStream;
        }

        // Get next data record
        public virtual string[] GetNextRecord()
        {
            if (!HasNextRecord()) return null;
            string line = _sr.ReadLine();
            if (line == null || line.Length == 0) return null;
            string[] cols = Regex.Split(line, "\\W");
            return cols;
        }

        public virtual void SetProperties(Dictionary<string, string> properties)
        {
            return;
        }

        public virtual string GetName()
        {
            return "Standard Output Processor";
        }
    }
}
=======
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace OOC.OutputProcessor
{
    public class StandardLineDataProcessor : OutputDataProcessor
    {
        protected StreamReader _sr;

        protected string streamKey;

        // Set input stream of data source with specified key
        public virtual void SetDataReader(string key, Stream stream)
        {
            if (streamKey == null || streamKey == key)
                _sr = new StreamReader(stream);
        }

        // Determinte whether there is next data record according to current input
        public virtual bool HasNextRecord()
        {
            if (_sr == null) return false;
            return !_sr.EndOfStream;
        }

        // Get next data record
        public virtual string[] GetNextRecord()
        {
            if (!HasNextRecord()) return null;
            string line = _sr.ReadLine();
            if (line == null || line.Length == 0) return null;
            string[] cols = Regex.Split(line, "\\W");
            return cols;
        }

        public virtual void SetProperties(Dictionary<string, string> properties)
        {
            return;
        }

        public virtual string GetName()
        {
            return "Standard Output Processor";
        }
    }
}
>>>>>>> 0daec768afcc757c83c424118f28374d34e3dc64
