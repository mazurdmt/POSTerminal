using System.Collections.Generic;
using YouScan.InterviewTask.DomainLayer.DomainModel.ProductModel;

namespace YouScan.InterviewTask.DomainLayer.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDictionary<string, Product> _products = new Dictionary<string, Product>();

        public Product CreateProduct(string name)
        {
            if (this.TryGetProduct(name, out var existingProduct))            
                return existingProduct;            

            var newProduct = new Product(name);
            this._products.Add(name, newProduct);

            return newProduct;
        }

        public Product GetProduct(string name)
        {
            _products.TryGetValue(name, out Product product);
            return product;
        }
        

        public bool TryGetProduct(string name, out Product product)
            => _products.TryGetValue(name, out product);
    }
}
