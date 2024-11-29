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
	public class OMDBController : ControllerBase
	{
		private readonly UserContext _context;
		private readonly OMDBService _omdb;
		public OMDBController(UserContext ctx, OMDBService omdb) {
			_context = ctx;
			_omdb = omdb;
		}

		[HttpGet("search")]
		public async Task<ActionResult<IEnumerable<Film>>> Search(string title)
		{
			var films = await _omdb.SearchByTitle(title);

			if (!films.Any()) {
				return NotFound();
			}

			return Ok(films);
		}

		[HttpGet("import")]
		public async Task<ActionResult> Import(int id)
		{
			return Ok();
		}
	}
}
