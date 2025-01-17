
namespace Premier;
using Premier.Services;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.

		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		builder.Services.AddDbContext<Models.UserContext>();
		builder.Services.AddSingleton<PasswordHasher<Models.User>>();
		builder.Services.AddSingleton<OMDBService>();
		builder.Services.AddHttpClient();

		// Add JWT verification
		builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options => {
				options.TokenValidationParameters = new TokenValidationParameters {
					ClockSkew = TimeSpan.FromMinutes(10),
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidAudience = "localhost:5041",
					ValidIssuer = "localhost:5041",
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("A Very Secret Secret")),
					RoleClaimType = ClaimTypes.Role};
			});

		builder.Services.AddAuthorization();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}


		app.UseAuthentication();
		app.UseAuthorization();


		app.MapControllers();

		app.Run();
	}
}
