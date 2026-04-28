using Microsoft.AspNetCore.Mvc;
using CodeLingoAPI.Data;
using CodeLingoAPI.Models;
using System.Linq;
using System.Threading.Tasks;
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

        //  REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            // Check if email already exists
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                return BadRequest("Email already exists.");
            }

            // Hash password
            user.PasswordHash = HashPassword(user.PasswordHash);

            user.TotalPoints = 0;
            user.Level = 1;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Create FREE subscription automatically
            var subscription = new Subscription
            {
                UserId = user.UserId,
                Plan = "Free",
                IsActive = true
            };

            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }

        // LOGIN
        [HttpPost("login")]
        public IActionResult Login(User loginUser)
        {
            var hashedPassword = HashPassword(loginUser.PasswordHash);

            var user = _context.Users.FirstOrDefault(u =>
                u.Email == loginUser.Email &&
                u.PasswordHash == hashedPassword
            );

            if (user == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            return Ok(new
            {
                user.UserId,
                user.Username,
                user.TotalPoints,
                user.Level
            });
        }

        //  PASSWORD HASH FUNCTION
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}