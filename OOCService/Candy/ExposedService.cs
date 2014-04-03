using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OOC.Candy
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ExposedService : Attribute
    {
        public string name { get; set; }

        public ExposedService(string name)
        {
            this.name = name;
        }
    }
}