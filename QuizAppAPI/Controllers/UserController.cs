using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeLingoAPI.Data;

namespace CodeLingoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET USER PROFILE
        [HttpGet("{id}")]
        public IActionResult GetProfile(int id)
        {
            var user = _context.Users
                .Include(u => u.Subscription)
                .FirstOrDefault(u => u.UserId == id);

            if (user == null)
                return NotFound();

            return Ok(new
            {
                user.UserId,
                user.Username,
                user.Email,
                user.TotalPoints,
                user.Level,
                user.CurrentStreak,
                Subscription = user.Subscription != null ? user.Subscription.Plan : "Free"
            });
        }

        //  GET USER STATS
        [HttpGet("{id}/stats")]
        public IActionResult GetStats(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);

            if (user == null)
                return NotFound();

            int xpNext = (user.Level + 1) * 100;

            return Ok(new
            {
                user.TotalPoints,
                user.Level,
                user.CurrentStreak,
                XPToNextLevel = xpNext - user.TotalPoints
            });
        }
    }
}