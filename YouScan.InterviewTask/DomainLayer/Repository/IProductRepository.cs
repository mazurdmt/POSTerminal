using YouScan.InterviewTask.DomainLayer.DomainModel.ProductModel;

namespace YouScan.InterviewTask.DomainLayer.Repository
{
    public interface IProductRepository
    {
        Product CreateProduct(string name);
        Product GetProduct(string name);
        bool TryGetProduct(string name, out Product product);
    }
}