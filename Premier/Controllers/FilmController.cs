using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Premier.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Premier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
       private readonly UserContext _context;
       public FilmController(UserContext ctx) {
          _context = ctx;
		   }

        // GET: api/<Film>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Film>>> GetAllFilms()
        {
            return await _context.Films.ToArrayAsync();
        }

        // GET api/<Film>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<string>>> GetFilm(string title)
        {
            return await _context.Films.Select(f => f.Title == title).ToListAsync();
        }

        [HttpGet("info")]
        public string GetFilms(int[] ids)
        {
            return "value";
        }
    }
}
