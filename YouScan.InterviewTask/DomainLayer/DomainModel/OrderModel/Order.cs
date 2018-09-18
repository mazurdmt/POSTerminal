using System;
using System.Collections.Generic;
using System.Linq;
using YouScan.InterviewTask.DomainLayer.DomainModel.ProductModel;

namespace YouScan.InterviewTask.DomainLayer.DomainModel.OrderModel
{
    public class Order
    {
        private readonly IDictionary<Guid, Position> _positions = new Dictionary<Guid, Position>();

        public Order()
            : this(Guid.NewGuid())
        {
        }

        public Order(Guid orderId)
        {
            this.OrdertId = orderId;
        }

        public Guid OrdertId { get; }

        public void AddProduct(Product product)
        {
            if (this._positions.TryGetValue(product.ProductId, out Position position))
            {
                position.IncreaseQuantity();
            }
            else
            {
                position = new Position(product);
                this._positions.Add(product.ProductId, position);
            }
        }

        public double CalculateTotalPrice()
            => this._positions.Values.Sum(pos => pos.TotalPrice);
    }
}
