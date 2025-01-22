using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Premier.Models;
using Premier.Services;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


namespace Premier.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[Controller]
	public class UserController : ControllerBase {

		private readonly UserContext _context;
		private readonly PasswordHasher<User> _hasher;
		private readonly JWTService _jwt;
		public UserController(UserContext ctx, PasswordHasher<User> hasher, JWTService jwt)
		{
			_context = ctx;
			_hasher = hasher;
			_jwt = jwt;
		}

		[HttpGet("all")]
		/*[Authorize(Roles = "Admin")]*/
		[AllowAnonymous]
		public async Task<ActionResult<IEnumerable<User>>> GetAllUser() {
			return await _context.Users.ToArrayAsync();
		}


		[HttpGet("{id}")]
		[Authorize(Roles = "Admin")]
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
		[AllowAnonymous]
		public async Task<ActionResult<User>> PostUser(UserCreation userCreation) {
			var user = new User { Password = "" };

			user.Password = _hasher.HashPassword(user, userCreation.Password);
			user.Pseudo = userCreation.Pseudo;

			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
		}

		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<ActionResult<User>> LoginUser(UserCreation userLogin) {
			User? user = await _context.Users.FirstOrDefaultAsync(u => u.Pseudo==userLogin.Pseudo);
			if (user == null) {
				return NotFound();
			}
			var result = _hasher.VerifyHashedPassword(user, user.Password, userLogin.Password);
			if (result == PasswordVerificationResult.Success) {
				var token = await _jwt.GetJwt(user.Pseudo, "User", user.Id);
				return Ok(token);
			}

			return StatusCode(400, "Password do not match");
		}


		[HttpPut("{id}")]
		[Authorize]
		public async Task<ActionResult<User>> PutUser(User userUpdate)
		{
			User? user = await _context.Users.FindAsync(userUpdate.Id);
			if (user == null)
			{
				return NotFound();
			}

			int realId = await GetId();
			if (realId != user.Id) {
				return StatusCode(403, "You cannot change another user");
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
			return StatusCode(203, "User modified");
		}

		[HttpDelete("{id}")]
		[Authorize]
		public async Task<IActionResult> DeleteUser(int id)
		{
			User? user = await _context.Users.FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			int realId = await GetId();
			if (realId != user.Id) {
				return StatusCode(403, "You cannot delete another user");
			}

			_context.Users.Remove(user);
			await _context.SaveChangesAsync();
			return StatusCode(204, "User deleted");
		}

		[HttpGet]
		[Authorize]
		public async Task<int> GetId() {
			int userId = int.Parse(User.FindFirst("Id").Value);
			return userId;
		}

	}
}
