
namespace Premier;
using Premier.Services;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

using Microsoft.OpenApi.Models;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.

		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(option => {
				option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
						{
						Name = "Authorization",
						Type = SecuritySchemeType.ApiKey,
						Scheme = "Bearer",
						BearerFormat = "JWT",
						In = ParameterLocation.Header,
						Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
						});
				option.AddSecurityRequirement(new OpenApiSecurityRequirement
						{
						{
						new OpenApiSecurityScheme
						{
						Reference = new OpenApiReference
						{
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"}}, new string[] {}}}); 
		});


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
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("A Very Secret SecretA Very Secret Secret")),
					RoleClaimType = ClaimTypes.Role};
					});

		builder.Services.AddSingleton<JWTService>();

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
