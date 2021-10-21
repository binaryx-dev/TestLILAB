using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestLILAB.DBContexts;
using TestLILAB.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestLILAB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private MySQLContext dbContext;

        public ProductsController(MySQLContext context)
        {
            dbContext = context;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public ActionResult<List<Product>> Get()
        {
            var list = dbContext.Products
                .Include(x => x.Category)
                .Include(x => x.Image);
            return Ok(list.ToList());
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public ActionResult<Product> GetWithProducts(int id)
        {
            var data = dbContext.Products
                .Include(x => x.Category)
                .Include(x => x.Image)
                .FirstOrDefault(e => e.ID == id);

            if (data == null) return NotFound();

            return Ok(data);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public ActionResult Post(Product newProduct)
        {
            if (dbContext.Products.Any(x => x.Name == newProduct.Name))
            {
                return BadRequest(new { error = $"La categoría \"{newProduct.Name}\", ya existe." });
            }

            try
            {
                dbContext.Products.Add(newProduct);
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(new { error = $"Error: {e.Message}" });
            }

            return Ok();
        }
    }
}
