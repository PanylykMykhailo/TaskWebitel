using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksWebitel.Model;

namespace TasksWebitel.Repositories
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<Order>> InfoOrder();
        public  Task<Order> InfoOrderId(int id);
        public void Create(Order order);
        public Task<Order> FindId(int orderId);
        public string DeleteOrders(int orderId);
        public string UpdateOrders(int id, Order order);
        public Task<IEnumerable<Order>> GetAllReferenceOrderId(int Id);
    }
}
