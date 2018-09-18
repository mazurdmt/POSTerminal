using System;
using System.Collections.Generic;
using YouScan.InterviewTask.DomainLayer.DomainModel.OrderModel;

namespace YouScan.InterviewTask.DomainLayer.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDictionary<Guid, Order> orders = new Dictionary<Guid, Order>();
        public Order CreateOrder()
        {
            var order = new Order();
            orders.Add(order.OrdertId, order);

            return order;
        }

        public Order GetOrder(Guid orderId)
        {
            orders.TryGetValue(orderId, out Order order);
            return order;
        }

        public bool TryGetOrder(Guid orderId, out Order order)
            => orders.TryGetValue(orderId, out order);
    }
}
