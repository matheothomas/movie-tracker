using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Premier.Models;
using Microsoft.EntityFrameworkCore;

namespace Premier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
	
		private readonly UserContext _context;
		public UserController(UserContext ctx)
		{
			_context = ctx;
		}

		[HttpGet("All")]
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

		[HttpPost("Add a user")]
		public async Task<ActionResult<User>> PostUser(UserCreation userCreation) {
			User user = new User {
				Pseudo = userCreation.Pseudo,
				Password = userCreation.Password,
			};
			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
		}
	}
}
