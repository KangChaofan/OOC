using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace OOC.Response
{
    [DataContract]
    public class GenericResponse
    {
        bool success = false;
        int errorCode = 0;
        string errorInfo = null;

        public GenericResponse(bool success)
        {
            Success = success;
        }

        public GenericResponse(bool success, int errorCode, string errorInfo)
        {
            Success = success;
            ErrorCode = errorCode;
            ErrorInfo = errorInfo;
        }

        [DataMember]
        public bool Success
        {
            get { return success; }
            set { success = value; }
        }

        [DataMember]
        public int ErrorCode
        {
            get { return errorCode; }
            set { errorCode = value; }
        }

        [DataMember]
        public string ErrorInfo
        {
            get { return errorInfo; }
            set { errorInfo = value; }
        }

    }

}