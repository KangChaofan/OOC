using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OOC.ServiceAttribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ExposedService : Attribute
    {
        public string Name { get; set; }

        public ExposedService(string name)
        {
            Name = name;
        }
    }
}