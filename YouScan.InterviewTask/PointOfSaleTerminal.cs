using System;
using YouScan.InterviewTask.DomainLayer.Service;
using YouScan.InterviewTask.Exceptions;

namespace YouScan.InterviewTask
{
    public class PointOfSaleTerminal : IPointOfSaleTerminal
    {
        private readonly IOrderingService orderingService;

        private Guid _currentOrder = Guid.Empty;

        public void NewOrder()
        {
            if (_currentOrder != Guid.Empty)
                throw new OrderNotCompletedException(_currentOrder);

             _currentOrder = this.orderingService.CreateNewOrder();
        }

        public PointOfSaleTerminal(IOrderingService orderingService)
        {
            this.orderingService = orderingService;

            NewOrder();
        }

        public double CompleteOrder()
        {
            var total = this.orderingService.CalculateTotal(_currentOrder);
            _currentOrder = Guid.Empty;

            return total;
        }

        public void Scan(string productName)
            => orderingService.AddPosition(productName, _currentOrder);
        
        public void SetProductRetailPrice(string productName, double priceForOne)
            => this.orderingService.SetProductRetailPrice(productName, priceForOne);

        public void SetProductWholesalePrice(string productName, int quantity, double priceForQuantity)
            => this.orderingService.SetProductWholesalePrice(productName, quantity, priceForQuantity);
        
    }
}
