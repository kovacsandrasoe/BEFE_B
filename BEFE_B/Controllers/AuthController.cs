using AutoMapper;
using BEFE_B.Data;
using BEFE_B.Dtos.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BEFE_B.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly BookDbContext _ctx;
        private readonly IMapper _mapper;

        public AuthController(BookDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("NagyonhosszútitkosítókulcsNagyonhosszútitkosítókulcsNagyonhosszútitkosítókulcsNagyonhosszútitkosítókulcsNagyonhosszútitkosítókulcsNagyonhosszútitkosítókulcs"));
            return Ok(
                new JwtSecurityToken(
                  issuer: "localhost",
                  audience: "localhost",
                  claims: [],
                  expires: DateTime.Now.AddMinutes(60),
                  signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                ));
        }
    }
}
