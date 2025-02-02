using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Premier.Models;
using Premier.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Core;

using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Premier.Controllers
{
	/*[Authorize]*/
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
		/*[Authorize]*/
		public async Task<ActionResult<IEnumerable<Film>>> GetAllFilms()
		{
			return await _context.Films.ToArrayAsync();
		}

		// GET api/<Film>
		[HttpGet("search")]
		/*[Authorize]*/
		public async Task<ActionResult<IEnumerable<Film>>> GetFilm(string title)
		{
			var films = await _context.Films.Where(f => f.Title == title).Select(f => f).ToListAsync();

			if (!films.Any()) {
				return NotFound();
			}

			return Ok(films);
		}

		[HttpGet("info")]
		/*[Authorize]*/
		public async Task<ActionResult<IEnumerable<Film>>> GetInfoFilms([FromQuery] int[] ids)
		{
			var films = await _context.Films.Where(f => ids.Contains(f.Id)).Select(f => f).ToListAsync();
			if (!films.Any()) {
				return NotFound();
			}

			return Ok(films);
		}
		[HttpDelete("delete")]
		public async Task<IActionResult> DeleteFilm(int id) {
			Film? film = await _context.Films.FindAsync(id);
			if (film == null) {
				return NotFound();
			}

			_context.Films.Remove(film);
			await _context.SaveChangesAsync();
			return StatusCode(204, "Film deleted");
		}
	}
}
