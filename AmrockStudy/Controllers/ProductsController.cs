using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmrockStudy.Data;
using AmrockStudy.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AmrockStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ECommerceContext _context;
        // right here dependency injection takes care of passing an instance
        // of the context into our controller, therefore we DO NOT need to make an 
        // instance of the object!!!!
        public ProductsController(ECommerceContext context)
        {
            _context = context;
        }
        // GET: api/Products
        [HttpGet]
        [EnableCors("_myAllowSpecificOrigins")]
        public GeneralProduct[] Get()
        {
            var getAllEntity =  _context.GeneralProduct.OrderByDescending(s => s.timestamp);
            var arrayEntity = getAllEntity.ToArray();
            return arrayEntity;
        }

        // GET: api/Products/5
        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet("{id}", Name = "Get")]
        public string Get(string id)
        {
            var entityChanged = _context.GeneralProduct.Where(
                x => x.id == id).FirstOrDefault();
            entityChanged.likes++;
            _context.SaveChanges();
            return "value";
        }
        // click like buttons
        // we will be updating the 
        // POST: api/Products
        [HttpPost]
        [EnableCors("_myAllowSpecificOrigins")]

        public Users Post(string data)
        {

            var result = JsonConvert.DeserializeObject<Users>(data);
            var repeatUser = _context.User.Where(x => x.UserEmail == result.UserEmail);
            if (!repeatUser.Any())
            {
                _context.User.Add(result);
                _context.SaveChanges();
                result = _context.User.Where(x => x.UserEmail == result.UserEmail).FirstOrDefault();
            }
            else
            {
                result = repeatUser.FirstOrDefault();
            }
            return result;
        }
        // PUT: api/Products/5
        [HttpPut("{id}")]
        [EnableCors("_myAllowSpecificOrigins")]
        public string Put(int id, string value)
        {
            Console.WriteLine(id);
            return "success";
        }


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
