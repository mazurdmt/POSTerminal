using System;

namespace YouScan.InterviewTask.DomainLayer.DomainModel.ProductModel
{
    public class Product
    {
        private IPrice retailPricing = null;
        private IPrice wholesalePrice = null;

        public Product(string name)
            :this(Guid.NewGuid(), name)
        {            
        }

        public Product(Guid productId, string name)
        {
            this.ProductId = productId;
            this.Name = name;
        }

        public void SetRetailPrice(double price)
        {
            if (price <= 0)
                throw new ArgumentException("Parameter cannot be less than or equal to zero", nameof(price));

            this.retailPricing = new RetailPrice(price);
        }
        

        public void SetWholesalePrice(int quantity, double price)
        {
            if (price <= 0)
                throw new ArgumentException("Parameter cannot be less than or equal to zero", nameof(price));

            if (quantity <= 0)            
                throw new ArgumentException("Parameter cannot be less than or equal to zero", nameof(quantity));

            if (quantity == 1)
            {
                SetRetailPrice(price);
                return;
            }

            this.wholesalePrice = new WholesalePrice(quantity, price);
        }

        public Guid ProductId { get; }

        public string Name { get; }

        public double GetTotalPrice(int quantity)
        {
            double total = 0;

            if (wholesalePrice != null)
            {
                total = (quantity / wholesalePrice.Quantity) * wholesalePrice.Price;
                total += (quantity % wholesalePrice.Quantity) * retailPricing.Price;
            }
            else
            {
                total = quantity * retailPricing.Price;
            }

            return total;
        }
    }
}
