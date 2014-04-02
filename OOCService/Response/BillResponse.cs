using System.Runtime.Serialization;
using OOC.ORM;

namespace OOC.Response
{
    [DataContract]
    public class BillResponse : GenericResponse
    {
        Bill bill;

        [DataMember]
        public Bill Bill
        {
            get { return bill; }
            set { bill = value; }
        }

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