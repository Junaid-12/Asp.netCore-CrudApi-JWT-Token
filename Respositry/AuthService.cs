using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApplicationApi.DataAccess;

namespace WebApplicationApi.Respositry
{
    public class AuthService : IAuthService
    {

        private readonly ApplicationDbContext _jwtService;
        private readonly IConfiguration _configuration;
        public AuthService(ApplicationDbContext jwtService, IConfiguration iconfiguration)
        {
            _jwtService = jwtService;
            _configuration = iconfiguration;
        }
        public string Login(LoginModel loginRequest)
        {
            if (loginRequest.Name != null && loginRequest.City != null)
            {
                var user = _jwtService.employees.SingleOrDefault(s => s.Name == loginRequest.Name && s.City == loginRequest.City);
                if (user != null)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("Name", user.Name),
                        new Claim("City", user.City)
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return jwtToken;
                }
                else
                {
                    throw new Exception("user is not valid");
                }
            }
            else
            {
                throw new Exception("credentials are not valid");
            }
        }
    }
}
