namespace YouScan.InterviewTask.DomainLayer.DomainModel.ProductModel
{
    public class RetailPrice : IPrice
    {
        public RetailPrice(double price)
        {
            this.Price = price;
            this.Quantity = 1;

        }

        public int Quantity { get; }

        public double Price { get; }
    }
}
