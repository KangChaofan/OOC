using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace OOC.Util
{
    public class RemoteUtil
    {
        public static string GetClientIPAddress()
        {
            return ((RemoteEndpointMessageProperty)OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name]).Address;
        }
    }
}