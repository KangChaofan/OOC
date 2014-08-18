<<<<<<< HEAD
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace OOC.OutputProcessor
{
    public interface OutputDataProcessor
    {
        // Set model properties
        void SetProperties(Dictionary<string, string> properties);

        // Get data processor name
        string GetName();

        // Set input stream of data source with specified key
        void SetDataReader(string key, Stream stream);

        // Determinte whether there is next data record according to current input
        bool HasNextRecord();

        // Get next data record
        string[] GetNextRecord();
    }
}
=======
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace OOC.OutputProcessor
{
    public interface OutputDataProcessor
    {
        // Set model properties
        void SetProperties(Dictionary<string, string> properties);

        // Get data processor name
        string GetName();

        // Set input stream of data source with specified key
        void SetDataReader(string key, Stream stream);

        // Determinte whether there is next data record according to current input
        bool HasNextRecord();

        // Get next data record
        string[] GetNextRecord();
    }
}
>>>>>>> 0daec768afcc757c83c424118f28374d34e3dc64
