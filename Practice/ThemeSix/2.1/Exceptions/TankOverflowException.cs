using System;
using System.Collections.Generic;
using System.Text;

namespace _2._1.Exceptions
{
    class TankOverflowException: Exception
    {
        public TankOverflowException(string message) : base(message) { }
    }
}
