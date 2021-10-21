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
    public class CategoriesController : ControllerBase
    {
        private MySQLContext dbContext;

        public CategoriesController(MySQLContext context)
        {
            dbContext = context;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public ActionResult<List<Category>> Get()
        {
            var list = dbContext.Categories;
            return Ok(list
                //.Select(x => new { x.ID, x.Name, x.Description, x.CreatedAt, x.UpdatedAt })
                .ToList());
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public ActionResult<Category> GetWithProducts(int id)
        {
            var data = dbContext.Categories
                .Include(e => e.Products)
                .ThenInclude(x => x.Image)
                .FirstOrDefault(e => e.ID == id);

            if (data == null) return NotFound();

            return Ok(data);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public ActionResult Post(Category newCategory)
        {
            if(dbContext.Categories.Any(x => x.Name == newCategory.Name))
            {
                return BadRequest(new { error = $"La categoría \"{newCategory.Name}\", ya existe." });
            }

            try
            {
                dbContext.Categories.Add(newCategory);
                dbContext.SaveChanges();
            }catch(Exception e)
            {
                return BadRequest(new { error = $"Error: {e.Message}" });
            }

            return Ok();            
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var data = dbContext.Categories.FirstOrDefault(x => x.ID == id);
            if(data == null) return NotFound(new { error = $"No se encontro la categoria con ID \"{id}\"." });

            dbContext.Remove(data);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}
