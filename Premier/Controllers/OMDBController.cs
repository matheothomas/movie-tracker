using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Premier.Models;
using Premier.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Core;

namespace Premier.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class OMDBController : ControllerBase
	{
		private readonly UserContext _context;
		private readonly OMDBService _omdb;
		public OMDBController(UserContext ctx, OMDBService omdb) {
			_context = ctx;
			_omdb = omdb;
		}

		[HttpGet("search")]
		[Authorize]
		public async Task<ActionResult<IEnumerable<Film>>> Search(string title)
		{
			var films = await _omdb.SearchByTitle(title);

			if (!films.Any()) {
				return NotFound();
			}

			return Ok(films);
		}

		[HttpGet("import")]
		[Authorize]
		public async Task<ActionResult> Import(string imdbID)
		{
			
			var film = await _omdb.GetByImdbId(imdbID);

			if (await _context.Films.Where(f => f.Imdb == imdbID).CountAsync() != 0) {
				return StatusCode(500, "This film was already added");
			};

			await _context.Films.AddAsync(film);
			await _context.SaveChangesAsync();

			return Ok(film);
		}
	}
}
