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
    public class UserController : ControllerBase
    {
        private MySQLContext dbContext;

        public UserController(MySQLContext context)
        {
            dbContext = context;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public ActionResult Post(User newUser)
        {
            if (dbContext.Users.Any(x => x.Name == newUser.Name))
            {
                return BadRequest(new { error = $"El Usuario \"{newUser.Name}\", ya existe." });
            }

            try
            {
                newUser.Token = Guid.NewGuid().ToString();
                dbContext.Users.Add(newUser);
                dbContext.SaveChanges();

                var user = dbContext.Users.FirstOrDefault(x => x.Name == newUser.Name);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(new { error = $"Error: {e.Message}" });
            }

        }

        // PUT api/<UserController>/5
        [HttpPut("")]
        public ActionResult Put(User updateUser)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.ID == updateUser.ID);
            if (user == null)
            {
                return BadRequest(new { error = $"El Usuario no existe." });
            }


            try
            {
                dbContext.Users.Update(updateUser);
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
