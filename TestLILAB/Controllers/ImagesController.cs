using Microsoft.AspNetCore.Mvc;
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
    public class ImagesController : ControllerBase
    {
        private MySQLContext dbContext;

        public ImagesController(MySQLContext context)
        {
            dbContext = context;
        }

        // GET: api/<ImagesController>
        [HttpGet]
        public ActionResult<List<ImageObj>> Get()
        {
            var list = dbContext.Images;
            return Ok(list.ToList());
        }

        // GET api/<ImagesController>/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var image = dbContext.Images
                .FirstOrDefault(x => x.ID == id);

            if (image == null) return NotFound();

            return Ok(image.Source);
        }

        // POST api/<ImagesController>
        [HttpPost]
        public ActionResult Post(ImageObj newImage)
        {
            try
            {
                dbContext.Images.Add(newImage);
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(new { error = $"Error: {e.Message}" });
            }

            return Ok();
        }

        // PUT api/<ImagesController>/5
        [HttpPut()]
        public ActionResult Put(ImageObj image)
        {
            var exist = dbContext.Images
                .Any(x => x.ID == image.ID);

            if (!exist) return NotFound();

            try
            {
                image.UpdatedAt = DateTime.Now;
                dbContext.Images.Update(image);
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(new { error = $"Error: {e.Message}" });
            }

            return Ok();
        }

        // DELETE api/<ImagesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var data = dbContext.Images.FirstOrDefault(x => x.ID == id);
            if (data == null) return NotFound(new { error = $"No se encontro la categoria con ID \"{id}\"." });

            dbContext.Remove(data);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}
