using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestLILAB.DBContexts;
using TestLILAB.Models;

namespace TestLILAB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private MySQLContext dbContext;

        public ShoppingController(MySQLContext context)
        {
            dbContext = context;
        }

        [HttpGet("{id}")]
        public ActionResult<ShopCart> Get(int id)
        {
            var element = dbContext.ShopCarts.FirstOrDefault(x => x.ID == id);
            return Ok(element);
        }

        [HttpPost]
        public ActionResult<ShopCart> Add(ShopCart cart)
        {
            dbContext.ShopCarts.Add(cart);

            foreach(var item in cart.Items)
            {
                var product = dbContext.Products
                    .FirstOrDefault(x => x.ID == item.ProductId);
                product.Quantity -= item.Quantity;
                product.UpdatedAt = DateTime.Now;

                if (product.Quantity < 0) return BadRequest("Inventario Insuficiente");

                dbContext.Products.Update(product);
            }

            dbContext.SaveChanges();

            return Ok(cart);
        }


    }
}
