using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksWebitel.Model;

namespace TasksWebitel.Repositories
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> InfoProduct();
        public Task<Product> InfoProductId(int id);
        public void Create(Product product);
        public Task<Product> FindId(int productId);
        public Product DeleteProducts(int productId);
        public void UpdateProducts(int id,Product product);
        public Task<IEnumerable<Product>> GetAllReferenceProductId(int id);
    }
}
