using Microsoft.AspNetCore.Mvc;
using CodeLingoAPI.Data;
using CodeLingoAPI.Models;
using System.Security.Cryptography;
using System.Text;

namespace CodeLingoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var hashedPassword = HashPassword(request.Password);

            var user = _context.Users.FirstOrDefault(u =>
                u.Email == request.Email &&
                u.PasswordHash == hashedPassword);

            if (user == null)
                return Unauthorized(new { message = "Invalid email or password" });

            return Ok(new
            {
                userId = user.UserId,
                username = user.Username,
                email = user.Email,
                level = user.Level,
                totalPoints = user.TotalPoints
            });
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}