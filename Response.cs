using System;
using System.Collections.Generic;
using System.Text;

namespace AP
{
    public class Response<T>
    {
        public int Count { get; set; }
        public T[] Results { get; set; }
    }
}
