using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Premier.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Premier.Controllers
{
	[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase {
		private readonly UserContext _context;
		public FavoriteController(UserContext ctx) {
			_context = ctx;
		}

        [HttpGet("{UserId}")]
		[Authorize]
        public async Task<ActionResult<IEnumerable<int>>> GetFavorite(int UserId) {

			int realId = await GetId();
			if (realId != UserId) {
				return StatusCode(403, "You cannot get the favorites of another user");
			}

            return await _context.Favorites.Where(f => UserId == f.UserId).Select(f => f.FilmId).ToListAsync();
        }

		[HttpGet("Film/{UserId}")]
		[Authorize]
        public async Task<ActionResult<IEnumerable<Film>>> GetFavoriteFilm(int UserId) {

			int realId = await GetId();
			if (realId != UserId) {
				return StatusCode(403, "You cannot get the favorites of another user");
			}

            var favorites = await _context.Favorites.Where(f => UserId == f.UserId).Select(f => f.FilmId).ToListAsync();
			var films = await _context.Films.Where(f => favorites.Contains(f.Id)).ToListAsync();
			return films;
        }

        // POST api/<Favorite>
        [HttpPost("add")]
		[Authorize]
        public async Task<ActionResult<Favorite>> PostFavorite(int UserId, int FilmId) {

			int realId = await GetId();
			if (realId != UserId) {
				return StatusCode(403, "You cannot add a favorite to another user");
			}

			var favorite = new Favorite { FilmId = FilmId};
			favorite.UserId = UserId;
			_context.Favorites.Add(favorite);
			await _context.SaveChangesAsync();

			return StatusCode(200, "");
        }

        // DELETE api/<Favorite>/5
        [HttpDelete("remove")]
		[Authorize]
        public async Task<ActionResult<Favorite>> DeleteFavorite(int UserId, int FilmId) {

			int realId = await GetId();
			if (realId != UserId) {
				return StatusCode(403, "You cannot delete the favorite of another user");
			}

			Favorite favorite = await _context.Favorites.Where(f => UserId == f.UserId && f.FilmId == FilmId).FirstOrDefaultAsync();

			if (favorite == null) {
				return NotFound();
			}

			_context.Favorites.Remove(favorite);
			await _context.SaveChangesAsync();

			return StatusCode(200, "");
        }

		[HttpGet]
		[Authorize]
		public async Task<int> GetId() {
			int userId = int.Parse(User.FindFirst("Id").Value);
			return userId;
		}
    }
}
