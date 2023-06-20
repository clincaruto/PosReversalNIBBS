using Microsoft.IdentityModel.Tokens;
using PosReversalNIBBS_API.Models.Domain;
using PosReversalNIBBS_API.Repositories.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PosReversalNIBBS_API.Repositories.Repository
{
    public class TokenHandlerRepo : ITokenHandlerRepo
    {
        private readonly IConfiguration configuration;

        public TokenHandlerRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Task<string> CreateTokenAsync(User user)
        {
            // Create Claims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName));
            claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
            claims.Add(new Claim(ClaimTypes.Email, user.EmailAddress));

            // Loop into roles of users
            //user.Roles.ForEach((role) =>
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role));
            //});

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
