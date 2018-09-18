using System;
using YouScan.InterviewTask.DomainLayer.DomainModel.OrderModel;

namespace YouScan.InterviewTask.DomainLayer.Repository
{
    public interface IOrderRepository
    {
        Order CreateOrder();
        Order GetOrder(Guid orderId);
        bool TryGetOrder(Guid orderId, out Order order);
    }
}