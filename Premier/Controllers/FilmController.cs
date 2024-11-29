using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Premier.Models;
using Premier.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Core;

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
		public async Task<ActionResult<IEnumerable<Film>>> GetFilm(string title)
		{
			var films = await _context.Films.Where(f => f.Title == title).Select(f => f).ToListAsync();

			if (!films.Any()) {
				return NotFound();
			}

			return Ok(films);
		}

		[HttpGet("info")]
		public async Task<ActionResult<IEnumerable<Film>>> GetInfoFilms([FromQuery] int[] ids)
		{
			var films = await _context.Films.Where(f => ids.Contains(f.Id)).Select(f => f).ToListAsync();
			if (!films.Any()) {
				return NotFound();
			}

			return Ok(films);
		}
	}
}
