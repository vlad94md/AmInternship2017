using System;

namespace EnFlights.ApplicationCore.Exceptions
{
    public class OrderFailException : Exception
    {
        public OrderFailException(string message) : base(message)
        {
            
        }
    }
}
