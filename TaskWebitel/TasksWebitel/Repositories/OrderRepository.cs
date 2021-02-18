using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksWebitel.Data;
using TasksWebitel.Model;

namespace TasksWebitel.Repositories
{
    public class OrderRepository:IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IProductRepository _productRepository;
        public OrderRepository(ApplicationDbContext dbContex, IProductRepository productRepository)
        {
            _dbContext = dbContex;
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<Order>> InfoOrder()
        {
            var info =await _dbContext.Orders.ToListAsync();
            return info;
        }
        public async Task<Order> InfoOrderId(int id) 
        {
            var info = await _dbContext.Orders.Where(a => a.OrderId == id).FirstOrDefaultAsync();
            return info;
        }
        public void Create(Order order) 
        {
            Order orders = new Order();
            orders.Number = order.Number;
            orders.Amount = order.Amount;
            orders.OrderId = order.OrderId;
            orders.Products = new List<Product> { };
            if (order.Products != null)
            {
                foreach (var c in order.Products)
                {
                    var result = _dbContext.Products.Where(w => w.Name == c.Name && w.Price == c.Price).FirstOrDefault();
                    if (result != null)
                    {
                        orders.Products.Add(result);
                    }
                    else 
                    {
                        _productRepository.Create(c);
                        var product = _dbContext.Products.Where(w => w.Name == c.Name && w.Price == c.Price).FirstOrDefault();
                        orders.Products.Add(product);
                    }
                }   
            }
            _dbContext.Orders.Add(orders);
            _dbContext.SaveChanges();

        }
        public async Task<Order> FindId(int orderId) 
        {
            var find = await _dbContext.Orders.Where(s => s.OrderId == orderId).FirstOrDefaultAsync();
            
            return find;
        }
        public string DeleteOrders(int orderId) 
        {
            Order order=  _dbContext.Orders.Find(orderId) ;

            if (order != null)
            {
                _dbContext.Orders.Remove(order);
                _dbContext.SaveChanges();
                return "Delete";

            }
            return "Not Found";
        }
        public string UpdateOrders(int id, Order order) 
        {
            Order or = _dbContext.Orders.Find(id);
            if (or == null)
            {
                return "Not found";
            }
            else 
            {
                or.Number = order.Number;
                or.Amount = order.Amount;
                _dbContext.Orders.Update(or);
                _dbContext.SaveChanges();
                return "Update";
            }
        }
        public async Task<IEnumerable<Order>> GetAllReferenceOrderId(int id)
        {

           var result = await _dbContext.Orders.Include(s => s.Products).Where(o => o.OrderId == id).ToListAsync();
           return result;      
        }
    }
}
