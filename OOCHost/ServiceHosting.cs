using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Reflection;
using OOC.Candy;
using OOC.Service;
using System.Configuration;

namespace OOC.Host
{
    class ServiceHosting
    {
        private static string httpEndPoint = ConfigurationSettings.AppSettings["httpEndPoint"];

        private static void buildAllServices()
        {
            Assembly serviceAssembly = Assembly.GetAssembly(typeof(ExposedService));
            Uri baseAddress = new Uri(httpEndPoint);
            foreach (Type type in serviceAssembly.GetTypes())
            {
                ExposedService[] attributes = (ExposedService[])type.GetCustomAttributes(typeof(ExposedService), true);
                if (attributes.Length > 0)
                {
                    string name = attributes[0].name;
                    using (ServiceHost host = new ServiceHost(type, baseAddress))
                    {
                        System.Console.WriteLine("Initializing " + name);
                        host.Opened += delegate
                        {
                            Console.WriteLine("Started " + name);
                        };
                        Type contract = type.GetInterfaces()[0];
                        host.AddServiceEndpoint(contract, new WSHttpBinding(), httpEndPoint + name);
                        host.Open();
                    }
                }
            }
        }

        public static void Main(string[] args)
        {
            buildAllServices();
            Console.WriteLine();
            Console.WriteLine("Press <ENTER> to terminate Host");
            Console.ReadLine();
        }
    }
}
