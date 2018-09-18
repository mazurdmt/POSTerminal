using System;
using YouScan.InterviewTask.DomainLayer.DomainModel.OrderModel;
using YouScan.InterviewTask.DomainLayer.DomainModel.ProductModel;
using YouScan.InterviewTask.DomainLayer.Repository;
using YouScan.InterviewTask.Exceptions;

namespace YouScan.InterviewTask.DomainLayer.Service
{
    public class OrderingService : IOrderingService
    {
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;

        public OrderingService(
            IOrderRepository orderRepository, IProductRepository productRepository)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
        }

        public Guid CreateNewOrder()
        {
            var order = orderRepository.CreateOrder();
            return order.OrdertId;
        }

        public void AddPosition(string productName, Guid orderId)
        {
            if (!this.orderRepository.TryGetOrder(orderId, out Order order))
                throw new OrderNotFoundException(orderId);

            if (!this.productRepository.TryGetProduct(productName, out Product product))
                throw new ProductNotFoundException(productName);

            // If an order and a product are found, add product to the order
            order.AddProduct(product);
        }

        public double CalculateTotal(Guid orderId)
        {
            if (!this.orderRepository.TryGetOrder(orderId, out Order order))
                throw new OrderNotFoundException(orderId);

            return order.CalculateTotalPrice();
        }

        public void SetProductWholesalePrice(string productName, int quantity, double priceForQuantity)
        {
            if (!this.productRepository.TryGetProduct(productName, out Product product))
                throw new ProductNotFoundException(
                    productName,
                    "Wholesale price is optional, set retail price before.");

            product.SetWholesalePrice(quantity, priceForQuantity);
        }

        public void SetProductRetailPrice(string productName, double price)
        {
            if (!this.productRepository.TryGetProduct(productName, out Product product))
            {
                product = this.productRepository.CreateProduct(productName);
                product.SetRetailPrice(price);
            }
            else
            {
                product.SetRetailPrice(price);
            }
        }
    }
}
