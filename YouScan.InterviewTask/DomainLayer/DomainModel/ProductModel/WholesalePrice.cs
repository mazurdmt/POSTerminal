namespace YouScan.InterviewTask.DomainLayer.DomainModel.ProductModel
{
    public class WholesalePrice : IPrice
    {
        public WholesalePrice(int quantity, double price)
        {
            this.Quantity = quantity;
            this.Price = price;

        }

        public int Quantity { get; }

        public double Price { get; }
    }
}
