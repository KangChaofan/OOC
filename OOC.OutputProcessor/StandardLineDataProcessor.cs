using System;
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
        public void SetDataReader(string key, Stream stream)
        {
            if (streamKey == null || streamKey == key)
                _sr = new StreamReader(stream);
        }

        // Determinte whether there is next data record according to current input
        public bool HasNextRecord()
        {
            if (_sr == null) return false;
            return !_sr.EndOfStream;
        }

        // Get next data record
        public double[] GetNextRecord()
        {
            if (!HasNextRecord()) return null;
            string line = _sr.ReadLine();
            if (line == null || line.Length == 0) return null;
            string[] cols = Regex.Split(line, "\\D");
            double[] record = new double[cols.Length];
            for (int i = 0; i < cols.Length; i++)
            {
                record[i] = Double.Parse(cols[i]);
            }
            return record;
        }

        public void SetProperties(Dictionary<string, string> properties)
        {
            return;
        }

        public string GetName()
        {
            return "Standard Output Processor";
        }
    }
}
