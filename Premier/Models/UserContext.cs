using Microsoft.EntityFrameworkCore;

namespace Premier.Models;

public class UserContext : DbContext {
	public UserContext(DbContextOptions<UserContext> options) : base(options) {}

	protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Connexion a la base sqlite
        options.UseSqlite("Data Source=Users.db");
    }

	public DbSet<User> Users {get; set; } = null!;
	public DbSet<Favorite> Favorites {get; set; } = null!;
}
