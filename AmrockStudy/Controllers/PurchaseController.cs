using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmrockStudy.Data;
using AmrockStudy.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AmrockStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly ECommerceContext _context;

        public PurchaseController(ECommerceContext context)
        {
            _context = context;
        }
        private class OrderViews
        {
            private string UserName { set; get; }
            private List<GeneralProduct> OrderedProduct { set; get; }
        }
        // GET: api/Purchase
        //public IEnumerable<string> Get()
        //{
        //    Console.WriteLine("got get");
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Purchase/5
        // return all the order details
        [HttpGet]
        [EnableCors("_myAllowSpecificOrigins")]
        public List<GeneralProduct> Get(string id)
        {
            List<GeneralProduct> orderedProducts = _context
                .Orders
                .Include(item => item.GeneralProduct)
                .Where(o => o.UserID == id)
                .Select( o => o.GeneralProduct)
                .ToList();
            Console.WriteLine(id);

            return orderedProducts;
        }

        // POST: api/Purchase
        [HttpPost]
        [EnableCors("_myAllowSpecificOrigins")]

        public bool Post(string data)
        {
            var result = JsonConvert.DeserializeObject<OrderCast>(data);
            var userId = result.UserID;
            var productIds = result.ProductID;
            var returnValue = false;
            var orders = _context.Orders.ToList();
            IList<Orders> existingItem = _context.Orders
                .Where(u => productIds.Contains(u.id))
                .Where(o => o.UserID == userId).ToList();

            if(existingItem.Count == 0)
            {
                returnValue = true;
                foreach (var productId in productIds)
                {
                    Orders newOrder = new Orders
                    {
                        GeneralProduct = _context.GeneralProduct.SingleOrDefault(g => g.id == productId),
                        Users = _context.User.SingleOrDefault(u => u.UserID == userId),
                        id = _context.GeneralProduct.SingleOrDefault(g => g.id == productId).id,
                        UserID = _context.User.SingleOrDefault(u => u.UserID == userId).UserID

                    };
                    _context.Orders.Add(newOrder);
                }
                _context.SaveChanges();
            }

            //var productPurchaseAdd = 
            Console.WriteLine(result);
            return returnValue;
        }

        // PUT: api/Purchase/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
