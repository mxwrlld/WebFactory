using System;
using System.Collections.Generic;
using System.Text;

namespace _1._2
{
    class AlreadyExistsException: Exception
    {
        public string Value { get; set; }
        
        public int Position { get; set; }

        public AlreadyExistsException(string message, string value, int position): base(message) 
        {
            Value = value;
            Position = position;
        }
    }
}
