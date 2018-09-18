using System;

namespace YouScan.InterviewTask.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        private readonly Guid _orderId;

        public OrderNotFoundException(Guid orderId)
        {
            _orderId = orderId;
        }

        public override string Message => $"Order with id '{_orderId}' not found";
    }
}
