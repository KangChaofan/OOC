using System;

namespace OOC.Instance
{
    public class InstanceMain
    {
        static void Main(string[] args)
        {
            string instanceName = "default";
            string logLocation = null;
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
                }
            }
            InstanceKeeper instanceKeeper = new InstanceKeeper(instanceName);
            instanceKeeper.LogLocation = logLocation;
            instanceKeeper.Run();
        }
    }
}
