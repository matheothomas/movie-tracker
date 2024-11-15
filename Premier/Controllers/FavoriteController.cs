using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Premier.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Premier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Favorite : ControllerBase {
		private readonly UserContext _context;
		public FavoriteController(UserContext ctx) {
			_context = ctx;
		}

        // GET api/<Favorite>/5
        [HttpGet("{UserId}")]
        public async Task<ActionResult<IEnumerable<int>>> GetFavorite() {
            return await _context.Favorites.Select(f => f.FilmId).ToListAsync();
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
        public async Task<ActionResult<Favorite>> DeleteFavorite(int FilmId) {
			Favorite favorite = await _context.Favorites.FindAsync(FilmId);

			if (favorite == null) {
				return NotFound();
			}

			_context.Favorites.Remove(favorite);
			await _context.SaveChangesAsync();

			return StatusCode(200, "");
        }
    }
}
