using Business.Services.Interfaces;
using DTO.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace HospitalAppointmentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDto)
        {
            var response = await _authService.LoginUser(loginUserDto.EmailOrPhone, loginUserDto.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            var user = response.Data; // User nesnesi dönülüyor
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.Role, user.UserRole.ToString()) // UserRole'ü doğrudan user nesnesinden alın
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Role = user.UserRole
            });
        }



        // Kullanıcı Kayıt İşlemi
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerUserDto)
        {
            var response = await _authService.RegisterUser(registerUserDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
