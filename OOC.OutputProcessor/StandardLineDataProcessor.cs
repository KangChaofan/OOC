using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace OOC.OutputProcessor
{
    public class StandardLineDataProcessor: OutputDataProcessor
    {
        protected StreamReader _sr;

        // Set input stream of data source with specified key
        public void SetDataReader(string key, Stream stream)
        {
            // TODO: support multiple data readers
            _sr = new StreamReader(stream);
        }

        // Determinte whether there is next data record according to current input
        public bool HasNextRecord()
        {
            return !_sr.EndOfStream;
        }

        // Get next data record
        public double[] GetNextRecord()
        {
            if (_sr.EndOfStream) return null;
            string line = _sr.ReadLine();
            if (line == null || line.Length == 0) return null;
            string[] cols = Regex.Split(line, "\\D");
            double[] record = new double[cols.Length];
            for (int i = 0; i < cols.Length; i ++) {
                record[i] = Double.Parse(cols[i]);
            }
            return record;
        }

        public void SetParameters(Hashtable parameters)
        {
            return;
        }

        public string GetName()
        {
            return "Standard Output";
        }
    }
}
