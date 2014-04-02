using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Collections;
using OOC.OpenMIWrapper;
using OOC.Response;

namespace OOC.Service
{
    public class LocalOPRService : ILocalOPRService
    {
        public SimulationRuntimeResult RunOpr(string oprFilename, bool verboseOff)
        {
            SimulationRuntimeResult result = new SimulationRuntimeResult();
            try
            {
                // check whether opr file exists
                if (oprFilename == null)
                {
                    result.ExitCode = 3;
                    result.Error = "No opr file is specified.";
                    return result;
                }

                FileInfo fileInfo = new FileInfo(oprFilename);
                if (!fileInfo.Exists)
                {
                    result.ExitCode = 4;
                    result.Error = "Cannot find input file " + oprFilename;
                    return result;
                }


                // open OPR
                CompositionManager composition = new CompositionManager();

                if (!verboseOff)
                    Console.WriteLine("Loading project file " + fileInfo.FullName + "...");
                composition.LoadFromFile(fileInfo.FullName);


                // prepare listeners
                if (!verboseOff)
                    Console.WriteLine("Preparing listener(s)...");
                ArrayList listOfListeners = new ArrayList();

                // logfile listener
                if (!string.IsNullOrEmpty(composition.LogToFile))
                {
                    // get composition file's directory to logfile is saved in same directory
                    string logFileName = Utils.GetFileInfo(fileInfo.DirectoryName, composition.LogToFile).FullName;
                    LogFileListener logFileListener = new LogFileListener(composition.ListenedEventTypes, logFileName);
                    listOfListeners.Add(logFileListener);
                }

                // console listener
                if (!verboseOff)
                {
                    ConsoleListener consoleListener = new ConsoleListener(composition.ListenedEventTypes);
                    listOfListeners.Add(consoleListener);
                }

                // create proxy listener
                ProxyListener proxyListener = new ProxyListener();
                proxyListener.Initialize(listOfListeners);

                // run simulation
                if (!verboseOff)
                    Console.WriteLine("Starting composition run...");
                composition.Run(proxyListener, true);

                if (!verboseOff)
                    Console.WriteLine("Closing composition...");
                composition.Release();

                result.ExitCode = 0;
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occured: " + e.ToString());
                result.ExitCode = -2;
                result.Error = e.ToString();
                return result;
            }
        }
    }
}
