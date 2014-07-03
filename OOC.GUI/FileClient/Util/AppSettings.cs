using System.Configuration;
using System.ServiceModel.Configuration;

namespace FileClient.Util
{
    public static class AppSettings
    {
        public static string GetEndpointAddress(string endpointName)
        {
            ClientSection clientSection = ConfigurationManager.GetSection("system.serviceModel/client") as ClientSection;
            if (clientSection != null)
            {
                foreach (ChannelEndpointElement item in clientSection.Endpoints)
                {
                    if (item.Name == endpointName)
                        return item.Address.ToString();
                }
            }
            return string.Empty;
        }
    }
}