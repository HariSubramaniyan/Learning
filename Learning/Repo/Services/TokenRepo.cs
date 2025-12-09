using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Learning.Repo.Iservices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Learning.Repo.Services
{
    public class TokenRepo : ITokenRepo
    {
        private readonly IConfiguration configuration;
        public TokenRepo(IConfiguration configuration) { 
            this.configuration = configuration;
        }
        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["JWT:Key"]));

            var Credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(

                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: Credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
