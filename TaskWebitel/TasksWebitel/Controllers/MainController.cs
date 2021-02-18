using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TasksWebitel.Data;
using TasksWebitel.Model;
using TasksWebitel.Repositories;
using TasksWebitel.ViewModel;

namespace TasksWebitel.Controllers
{
    [Route("api/[controller]")]
    public class MainController : Controller
    {
        ApplicationDbContext _dbContext;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
       

        public MainController(ApplicationDbContext applicationDbContext, IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _dbContext = applicationDbContext;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            
        }

        [HttpGet("Order")]

        public async Task<IEnumerable<Order>> GetOrder()
        {
            var result = await _orderRepository.InfoOrder();
            return result;
        }

        [HttpGet("Order/{id}")]

        public async Task<Order> GetOrderId(int id)
        {
            try
            {
                var result = await _orderRepository.InfoOrderId(id);
                return result;
            }
            catch (Exception)
            {

                throw new Exception("Not Found");
            }
            

        }
        [HttpGet("Product")]
        public async Task<IEnumerable<Product>> GetProduct()
        {
            var result = await _productRepository.InfoProduct();
            return result;
        }

        [HttpGet("Product/{id}")]
        public async Task<Product> GetProductId(int id)
        {
            try
            {
                var result = await _productRepository.InfoProductId(id);
                return result;
            }
            catch (Exception)
            {

                throw new Exception("Not Found");
            }
            

        }
        [HttpPost("Order")]
        public ActionResult PostOrder([FromBody] Order order)
        {
           
            if (order == null)
            {
                return BadRequest("Not Date");
            }
            _orderRepository.Create(order);
            return new ObjectResult("AddOrder");
        }
        [HttpPost("Product")]
        public ActionResult PostProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Not Date");
            }
            _productRepository.Create(product);
            return new ObjectResult("AddProduct");
        }
        [HttpDelete("Order/{id}")]
        public ActionResult DeleteOrder(int id)
        {
            var deleted = _orderRepository.DeleteOrders(id);

            if (deleted == null)
            {
                return BadRequest("Not Found to Delete");
            }
            return new ObjectResult(deleted);
        }
        [HttpPut("Order/{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] Order order)
        {

            try
            {
                var result = _orderRepository.UpdateOrders(id, order);
                return new ObjectResult("Update");
            }
            catch (Exception)
            {

                throw new Exception("Not Update");
            }
           
        }
        [HttpDelete("Product/{id}")]

        public ActionResult DeleteProduct(int id)
        {
            var deleted = _productRepository.DeleteProducts(id);

            if (deleted == null)
            {
                return BadRequest("Not Found to Delete");
            }
            return new ObjectResult(deleted);
        }
        [HttpPut("Product/{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            try
            {
                _productRepository.UpdateProducts(id, product);
                return new ObjectResult("Update");
            }
            catch (Exception)
            {

                throw new Exception("Not Upgrade");
            }
           
        }
       
        [HttpGet("OrderProductInOrderId/{id}")]
        public async Task<IActionResult> GetAllInfo(int id)
        {
            
           var result = await _orderRepository.GetAllReferenceOrderId(id);
           if (result.Count() == 0)
            {
                return NotFound("Not found order with this Id!!");
            }
            return Ok(result);


        }
        [HttpGet("OrderProductInProductId/{id}")]
        public async Task<IActionResult> GetAllInformation(int id)
        {
           
            
            var result = await _productRepository.GetAllReferenceProductId(id);
            if(result.Count() == 0)
            {
                return NotFound("Not found product with this Id!!");
            }
            return Ok(result);

        }
    }
}
