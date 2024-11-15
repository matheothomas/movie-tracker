using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Premier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Film : ControllerBase
    {
        // GET: api/<Film>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Film>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Film>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Film>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Film>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
