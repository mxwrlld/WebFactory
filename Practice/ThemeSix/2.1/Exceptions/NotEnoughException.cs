using System;
using System.Collections.Generic;
using System.Text;

namespace _2._1.Exceptions
{
    class NotEnoughException: Exception
    {
        public NotEnoughException(string message) : base(message) { }
    }
}
