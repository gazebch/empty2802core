using empty2802core.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace empty2802core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public DBContext DBContext { get; set; }
        public LoginController(DBContext context)
        {
            DBContext = context;
        }

        [HttpPost]
        public string Login(string login, string password)
        {
            var person = DBContext.Users.Where(p => (p.Name == login && p.Password == password)
                                                    || ((p.Email == login && p.Password == password)));
            if (person.Any())
            {

                var claims = new List<Claim> 
                { 
                    new Claim(ClaimTypes.Name, login),
                    new Claim("UserId", person.FirstOrDefault().Id.ToString())
                };

                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        claims: claims,
                        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)), // время действия 2 минуты
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

                var jwtToken =  new JwtSecurityTokenHandler().WriteToken(jwt);
                /*// формируем ответ
                var response = new
                {
                    access_token = jwtToken,
                    username = person.FirstOrDefault().Id
                };*/

                return jwtToken;
            }
            return "";
        }
    }
}
