using System.Runtime.Serialization;

namespace OOC.Contract.Data.Response
{
    [DataContract]
    public class GenericResponse
    {
        public GenericResponse(bool success) : this(success, 0, null)
        {
        }

        public GenericResponse(bool success, int errorCode, string errorInfo)
        {
            Success = success;
            ErrorCode = errorCode;
            ErrorInfo = errorInfo;
        }

        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public int ErrorCode { get; set; }

        [DataMember]
        public string ErrorInfo { get; set; }
    }
}