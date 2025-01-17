using Microsoft.AspNetCore.Mvc;
using Premier.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Core;

using System.Text;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Premier.Services {

	public class JWTService {
		private readonly string _apiSecret;

		public JWTService(IConfiguration configuration) {
			_apiSecret = configuration["JWTSecret"];
		}

		public async Task<string> GetJwt(string pseudo, string role, int id) {

			var claims = new[]
			{
				new Claim("Id", id.ToString()),
				new Claim(ClaimTypes.Name, pseudo),
				new Claim(ClaimTypes.Role, role)
			};

			SymmetricSecurityKey key = new SymmetricSecurityKey(
					Encoding.UTF8.GetBytes(_apiSecret)
					);
			SigningCredentials credentials = new SigningCredentials(
					key, 
					SecurityAlgorithms.HmacSha256
					);

			JwtSecurityToken token = new JwtSecurityToken(
					issuer: "localhost:5041",
					audience: "localhost:5041",
					claims: claims,
					expires: DateTime.Now.AddMinutes(3000),
					signingCredentials: credentials
					);

			string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

			return tokenString;
		}
	}
}
