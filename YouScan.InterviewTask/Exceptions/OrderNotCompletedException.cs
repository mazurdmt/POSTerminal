using System;

namespace YouScan.InterviewTask.Exceptions
{
    public class OrderNotCompletedException : Exception
    {
        private readonly Guid _orderId;

        public OrderNotCompletedException(Guid orderId)
        {
            _orderId = orderId;
        }

        public override string Message => $"Order with id '{_orderId}' not completed";
    }
}
