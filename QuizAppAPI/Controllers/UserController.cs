using Microsoft.AspNetCore.Mvc;
using QuizAppAPI.Data;
using QuizAppAPI.DTOs;

namespace QuizAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        // GET /api/user/1
        [HttpGet("{id}")]
        public ActionResult<UserProfileDto> GetProfile(int id)
        {
            var user = InMemoryStore.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound($"User {id} not found.");

            return Ok(new UserProfileDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                XP = user.XP,
                Level = user.Level,
                Streak = user.Streak,
                SubscriptionType = user.SubscriptionType
            });
        }

        // GET /api/user/1/stats
        [HttpGet("{id}/stats")]
        public ActionResult<UserStatsDto> GetStats(int id)
        {
            var user = InMemoryStore.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            int xpForNextLevel = (user.Level + 1) * 100;

            return Ok(new UserStatsDto
            {
                XP = user.XP,
                Level = user.Level,
                Streak = user.Streak,
                XPToNextLevel = xpForNextLevel - user.XP
            });
        }
    }
}