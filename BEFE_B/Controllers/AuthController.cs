using AutoMapper;
using BEFE_B.Data;
using BEFE_B.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
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
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IConfiguration _configuration;

        public AuthController(BookDbContext ctx, IMapper mapper, UserManager<IdentityUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _ctx = ctx;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task Register(UserCreateDto dto)
        {
            var user = new IdentityUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                EmailConfirmed = true,
            };
            await _userManager.CreateAsync(user, dto.Password);

            if (_userManager.Users.Count() == 1)
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _userManager.AddToRoleAsync(user, "Admin");
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            var result = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!result)
            {
                return BadRequest("Password is incorrect");
            }

            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            foreach (var role in await _userManager.GetRolesAsync(user))
            {
                claim.Add(new Claim(ClaimTypes.Role, role));
            }

            var accessToken = GenerateAccessToken(claim);
            return Ok(new JwtSecurityTokenHandler().WriteToken(accessToken));
                
        }

        private JwtSecurityToken GenerateAccessToken(IEnumerable<Claim>? claims)
        {
            var signinKey = new SymmetricSecurityKey(
                  Encoding.UTF8.GetBytes(_configuration["jwt:key"] ?? throw new Exception("jwt:key not found in appsettings.json")));

            return new JwtSecurityToken(
                  issuer: _configuration["jwt:issuer"],
                  audience: _configuration["jwt:audience"],
                  claims: claims?.ToArray(),
                  expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["jwt:access_expiry_minutes"])),
                  signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                );
        }

    }
}
