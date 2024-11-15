using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Premier.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Premier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Favorite : ControllerBase
    {
        // GET: api/<Favorite>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Favorite>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Favorite>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Favorite>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Favorite>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
