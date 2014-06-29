using System;
using System.Collections;
using System.IO;

namespace OOC.OutputProcessor
{
    public interface OutputDataProcessor
    {
        // Set model parameters
        void SetParameters(Hashtable parameters);

        // Get data processor name
        string GetName();

        // Set input stream of data source with specified key
        void SetDataReader(string key, Stream stream);

        // Determinte whether there is next data record according to current input
        bool HasNextRecord();

        // Get next data record
        double[] GetNextRecord();
    }
}
