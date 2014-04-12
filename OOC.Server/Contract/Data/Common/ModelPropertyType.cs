using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OOC.Contract.Data.Common
{
    public enum ModelPropertyType
    {
        String = 0,
        Integer = 1,
        Float = 2,
        Enum = 3,
        InputFile = 4,
        OutputFile = 5,
        Constant = 255
    }
}