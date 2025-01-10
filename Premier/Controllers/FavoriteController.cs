using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Premier.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Premier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase {
		private readonly UserContext _context;
		public FavoriteController(UserContext ctx) {
			_context = ctx;
		}

        // GET api/<Favorite>/5
        [HttpGet("{UserId}")]
        public async Task<ActionResult<IEnumerable<int>>> GetFavorite(int UserId) {
            return await _context.Favorites.Where(f => UserId == f.UserId).Select(f => f.FilmId).ToListAsync();
        }

		[HttpGet("Film/{UserId}")]
        public async Task<ActionResult<IEnumerable<Film>>> GetFavoriteFilm(int UserId) {
            var favorites = await _context.Favorites.Where(f => UserId == f.UserId).Select(f => f.FilmId).ToListAsync();
			var films = await _context.Films.Where(f => favorites.Contains(f.Id)).ToListAsync();
			return films;
        }

        // POST api/<Favorite>
        [HttpPost("add")]
        public async Task<ActionResult<Favorite>> PostFavorite(int FilmIdSuppr) {
			var favorite = new Favorite { FilmId = FilmIdSuppr};
			favorite.UserId = 0;
			_context.Favorites.Add(favorite);
			await _context.SaveChangesAsync();

			return StatusCode(200, "");
        }

        // DELETE api/<Favorite>/5
        [HttpDelete("remove")]
        public async Task<ActionResult<Favorite>> DeleteFavorite(int UserId, int FilmId) {
			Favorite favorite = await _context.Favorites.Where(f => UserId == f.UserId && f.FilmId == FilmId).FirstOrDefaultAsync();

			if (favorite == null) {
				return NotFound();
			}

			_context.Favorites.Remove(favorite);
			await _context.SaveChangesAsync();

			return StatusCode(200, "");
        }
    }
}
