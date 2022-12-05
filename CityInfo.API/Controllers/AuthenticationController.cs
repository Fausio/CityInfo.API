using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CityInfo.API.Controllers
{
    [Route("api/Authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        public class AuthenticationRequest
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        public class CityInfoUser
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        private readonly IConfiguration _configuration;
        public AuthenticationController(IConfiguration configuration)
        {
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(IConfiguration));
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequest model)
        {
            // 1 validate user ... from db in prod
            var user = ValidateCityInfoUser(model.username, model.password);

            if (user is null)
            {
                return Unauthorized();
            }

            //create token A,B,C,D,E 

            //A get security key from config file
            var security = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForkey"]));

            //B SigningCredentials
            var signingCredentials = new SigningCredentials(security, SecurityAlgorithms.HmacSha256);

            // C Claims - info related to user
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("me", user.FirstName));
            claimsForToken.Add(new Claim("family", user.LastName));

            //D JWT 
            var jwtSecurityToken = new JwtSecurityToken(
                 _configuration["Authentication:Issuer"],
                 _configuration["Authentication:Audience"],
                 claimsForToken,
                 DateTime.UtcNow,
                 DateTime.UtcNow.AddHours(1),
                 signingCredentials
                );

            //E return Baerer Token 
            var tokenToreturn = new JwtSecurityTokenHandler()
                                    .WriteToken(jwtSecurityToken);

            return tokenToreturn; 
        }

        private CityInfoUser ValidateCityInfoUser(string username, string password)
        {
            // simulate validate and return user from db
            return new CityInfoUser
            {
                FirstName = "Fausio",
                LastName = "Matsinhe",
                Email = "fausioluis@live.com"
            };
        }
    }
}
