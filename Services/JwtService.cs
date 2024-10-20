using library_api.Models;
using library_api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace library_api.Services
{
	public class JwtService : IJwtService
	{
		private IConfiguration _configuration;

		public JwtService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string GenerateToken(User user, TimeSpan expiration)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
			new Claim(ClaimTypes.Name, user.Username),
			new Claim(ClaimTypes.Role, user.Role.ToString())
		}),
				Expires = DateTime.UtcNow.Add(expiration),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
				Audience = _configuration["Jwt:Audience"],
				Issuer = _configuration["Jwt:Issuer"]
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

		public ClaimsPrincipal ValidateToken(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
			var validationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero
			};

			return tokenHandler.ValidateToken(token, validationParameters, out _);
		}

		public ClaimsPrincipal? ValidateRefreshToken(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

			var validationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero
			};

			try
			{
				var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
				return principal;
			}
			catch
			{
				return null;
			}
		}
	}
}