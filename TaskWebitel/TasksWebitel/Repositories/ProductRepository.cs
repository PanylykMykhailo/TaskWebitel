using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksWebitel.Data;
using TasksWebitel.Model;

namespace TasksWebitel.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly ApplicationDbContext _dbContext;
        public ProductRepository(ApplicationDbContext dbContex)
        {
            _dbContext = dbContex;
        }
        public async Task<IEnumerable<Product>> InfoProduct() 
        {
            var info = await _dbContext.Products.ToListAsync();
            return info;
        }
        public async Task<Product> InfoProductId(int id) 
        {
            var info = await _dbContext.Products.Where(a => a.ProductId == id).FirstOrDefaultAsync();
            return info;
        }
        public void Create(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }
        public async Task<Product> FindId(int productId)
        {
            var find = await _dbContext.Products.Where(s => s.ProductId== productId).FirstOrDefaultAsync();
            return find;
        }
        public Product DeleteProducts(int productId)
        {
            Product product =  _dbContext.Products.Find(productId);

            if (product != null)
            {
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();

            }
            return product;
        }
        public void UpdateProducts(int id,Product product) 
        {
            Product prod = _dbContext.Products.Find(id);
            prod.Name = product.Name;
            prod.Price = product.Price;
            _dbContext.Products.Update(prod);
            _dbContext.SaveChanges();
            
        }
        public async Task<IEnumerable<Product>> GetAllReferenceProductId(int id)
        {
            var result = await _dbContext.Products.Include(s => s.Orders).Where(o => o.ProductId == id).ToListAsync();
            return result;
        }  
    }
}
