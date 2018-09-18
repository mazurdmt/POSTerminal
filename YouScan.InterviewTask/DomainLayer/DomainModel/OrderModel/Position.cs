using YouScan.InterviewTask.DomainLayer.DomainModel.ProductModel;

namespace YouScan.InterviewTask.DomainLayer.DomainModel.OrderModel
{
    public class Position
    {
        public Position(Product product)
        {
            this.Product = product;
            this.Quantity = 1;
        }

        public Product Product { get; set; }

        public double TotalPrice
            => this.Product.GetTotalPrice(this.Quantity);

        public int Quantity { get; private set; }

        public void IncreaseQuantity()
            => this.Quantity++;        

        public void DecreaseQuantity()
        {
            if (this.Quantity > 0)            
                this.Quantity--;            
        }
    }
}
