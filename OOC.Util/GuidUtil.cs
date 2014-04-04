using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OOC.Util
{
    public class GuidUtil
    {
        public static string newGuid()
        {
            return System.Guid.NewGuid().ToString();
        }
    }
}