using System;
using System.Collections.Generic;
using OOC.Util;

namespace OOC.Instance
{
    public class InstanceMain
    {
        static void Main(string[] args)
        {
            string instanceName = GuidUtil.newGuid();
            string logLocation = null;
            int slots = 1;
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-n":
                    case "--name":
                        instanceName = args[++i];
                        break;
                    case "-l":
                    case "--log":
                        logLocation = args[++i];
                        break;
                    case "-s":
                    case "--slots":
                        slots = Int32.Parse(args[++i]);
                        break;
                }
            }
            InstanceKeeper instanceKeeper = new InstanceKeeper(instanceName, slots);
            instanceKeeper.LogLocation = logLocation;
            instanceKeeper.Run();
        }
    }
}
