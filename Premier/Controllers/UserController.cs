using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Premier.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Premier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
	
		private readonly UserContext _context;
		private readonly PasswordHasher<User> _hasher;
		public UserController(UserContext ctx, PasswordHasher<User> hasher)
		{
			_context = ctx;
			_hasher = hasher;
		}

		[HttpGet("all")]
		public async Task<ActionResult<IEnumerable<User>>> GetAllUser() {
			return await _context.Users.ToArrayAsync();
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<User>> GetUser(int id)
		{
			Console.WriteLine(id);
			var user = await _context.Users.FindAsync(id);

			if (user == null)
			{
				return NotFound();
			}
			Console.WriteLine($"{user.Pseudo} {user.Id}");
			return Ok(user);
		}

		[HttpPost("register")]
		public async Task<ActionResult<User>> PostUser(UserCreation userCreation) {
			var user = new User { Password = "" };

			user.Password = _hasher.HashPassword(user, userCreation.Password);
			user.Pseudo = userCreation.Pseudo;

			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
		}

		[HttpPost("login")]
		public async Task<ActionResult<User>> LoginUser(UserCreation userLogin) {
			User? user = await _context.Users.FirstOrDefaultAsync(u => u.Pseudo==userLogin.Pseudo);
			var result = _hasher.VerifyHashedPassword(user, user.Password, userLogin.Password);

			if (user == null) {
				return NotFound();
			}

			if (result == PasswordVerificationResult.Success) {
				return Ok(user);
			}

			return StatusCode(400, "Password do not match");
		}


		[HttpPut("{id}")]
		public async Task<ActionResult<User>> PutUser(User userUpdate)
		{
			User? user = await _context.Users.FindAsync(userUpdate.Id);
			if (user == null)
			{
				return NotFound();
			}

			user.Pseudo = userUpdate.Pseudo;
			user.Password = userUpdate.Password;
			_context.Entry(user).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				return StatusCode(500, "Erreur de concurrence");
			}
			return Ok(user);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			// on récupère la user que l'on souhaite supprimer
			User? user = await _context.Users.FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			// on indique a notre contexte que l'objet a été supprimé
			_context.Users.Remove(user);
			// on enregistre les modifications
			await _context.SaveChangesAsync();
			// on retourne un code 204 pour indiquer que la suppression a bien eu lieu
			return NoContent();
		}

	}
}
