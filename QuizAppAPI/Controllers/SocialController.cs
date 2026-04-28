using Microsoft.AspNetCore.Mvc;
using CodeLingoAPI.Data;

namespace CodeLingoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SocialController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SocialController(AppDbContext context)
        {
            _context = context;
        }

        // GET LEADERBOARD (TOP USERS)
        [HttpGet("leaderboard")]
        public IActionResult GetLeaderboard()
        {
            var leaderboard = _context.Users
                .OrderByDescending(u => u.TotalPoints)
                .Take(10)
                .Select(u => new
                {
                    u.UserId,
                    u.Username,
                    u.TotalPoints,
                    u.Level
                })
                .ToList();

            return Ok(leaderboard);
        }

        //  GET USER RANK
        [HttpGet("rank/{userId}")]
        public IActionResult GetUserRank(int userId)
        {
            var users = _context.Users
                .OrderByDescending(u => u.TotalPoints)
                .ToList();

            var rank = users.FindIndex(u => u.UserId == userId) + 1;

            if (rank == 0)
                return NotFound("User not found");

            return Ok(new
            {
                UserId = userId,
                Rank = rank
            });
        }
    }
}