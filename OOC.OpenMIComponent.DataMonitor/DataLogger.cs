<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using OpenMI.Standard;

namespace OOC.OpenMIComponent.DataMonitor
{
    public class DataLogger : DataMonitor
    {
        public string FileName { get; set; }

        private StreamWriter sw;

        private object wlock = new object();

        public override void Initialize(IArgument[] properties)
        {
            base.Initialize(properties);
            foreach (IArgument argument in properties)
            {
                if (argument.Key == "FileName")
                {
                    FileName = argument.Value;
                }
            }
        }

        public override void Prepare()
        {
            sw = new StreamWriter(FileName);
            sw.AutoFlush = true;
            writeRow(new string[] { "SimulationTime", "ModelID", "ElementSet", "Quantity", "ScalarSet" });
        }

        public override void Finish()
        {
            sw.Close();
            sw.Dispose();
        }

        private void writeRow(string[] cols)
        {
            string tmp = "";
            for (int i = 0; i < cols.Length; i++)
            {
                tmp += cols[i];
                if (i < cols.Length - 1) tmp += "\t";
            }
            lock (wlock)
            {
                sw.WriteLine(tmp);
            }
        }

        protected override void DataArrived(DateTime simulationTime, string modelId, string quantity, string elementSet, IScalarSet scalarSet)
        {
            string simTime = simulationTime.ToShortDateString() + " " + simulationTime.ToLongTimeString();
            string scalar = "";
            for (int i = 0; i < scalarSet.Count; i++)
            {
                scalar += scalarSet.GetScalar(i);
                if (i < scalarSet.Count - 1) scalar += ",";
            }
            writeRow(new string[] { simTime, modelId, elementSet, quantity, scalar });
        }

    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using OpenMI.Standard;

namespace OOC.OpenMIComponent.DataMonitor
{
    public class DataLogger : DataMonitor
    {
        public string FileName { get; set; }

        private StreamWriter sw;

        private object wlock = new object();

        public override void Initialize(IArgument[] properties)
        {
            base.Initialize(properties);
            foreach (IArgument argument in properties)
            {
                if (argument.Key == "FileName")
                {
                    FileName = argument.Value;
                }
            }
        }

        public override void Prepare()
        {
            sw = new StreamWriter(FileName);
            sw.AutoFlush = true;
            writeRow(new string[] { "SimulationTime", "ModelID", "ElementSet", "Quantity", "ScalarSet" });
        }

        public override void Finish()
        {
            sw.Close();
            sw.Dispose();
        }

        private void writeRow(string[] cols)
        {
            string tmp = "";
            for (int i = 0; i < cols.Length; i++)
            {
                tmp += cols[i];
                if (i < cols.Length - 1) tmp += "\t";
            }
            lock (wlock)
            {
                sw.WriteLine(tmp);
            }
        }

        protected override void DataArrived(DateTime simulationTime, string modelId, string quantity, string elementSet, IScalarSet scalarSet)
        {
            string simTime = simulationTime.ToShortDateString() + " " + simulationTime.ToLongTimeString();
            string scalar = "";
            for (int i = 0; i < scalarSet.Count; i++)
            {
                scalar += scalarSet.GetScalar(i);
                if (i < scalarSet.Count - 1) scalar += ",";
            }
            writeRow(new string[] { simTime, modelId, elementSet, quantity, scalar });
        }

    }
}
>>>>>>> 0daec768afcc757c83c424118f28374d34e3dc64
