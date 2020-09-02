using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AP
{
    
    public class Person
    {
        public string Name { get; set; }
        public string[] Films { get; set; }
        public string Homeworld { get; set; }
    }
}
