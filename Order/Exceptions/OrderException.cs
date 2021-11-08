using System;

namespace Order
{
    public class OrderException : Exception
    {
        public OrderException(string message) : base(message) {}
    }
}
