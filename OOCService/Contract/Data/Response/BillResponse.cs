using System.Runtime.Serialization;
using OOC.ORM;

namespace OOC.Contract.Data.Response
{
    [DataContract]
    public class BillResponse : GenericResponse
    {
        [DataMember]
        public Bill Bill { get; set; }

        public BillResponse(Bill bill)
            : base(true)
        {
            Bill = bill;
        }

        public BillResponse(bool success, int errorCode, string errorInfo)
            : base(success, errorCode, errorInfo)
        {
        }
    }
}