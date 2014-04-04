using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using OOC.ORM;

namespace OOC.Contract.Data.Response
{
    [DataContract]
    public class BillListResponse : GenericResponse
    {
        [DataMember]
        public List<Bill> Bills { get; set; }

        public BillListResponse(List<Bill> bills)
            : base(true)
        {
            Bills = bills;
        }

        public BillListResponse(bool success, int errorCode, string errorInfo)
            : base(success, errorCode, errorInfo)
        {
        }
    }
}