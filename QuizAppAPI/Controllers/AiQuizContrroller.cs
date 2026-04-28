using Microsoft.AspNetCore.Mvc;
using CodeLingoAPI.Services;
using CodeLingoAPI.Data;
using System.Linq;

namespace CodeLingoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIQuizController : ControllerBase
    {
        private readonly AIQuizService _aiService;
        private readonly AppDbContext _context;

        public AIQuizController(AIQuizService aiService, AppDbContext context)
        {
            _aiService = aiService;
            _context = context;
        }

        //  AI QUIZ GENERATION (PREMIUM ONLY)
        [HttpGet("generate")]
        public async Task<IActionResult> GenerateQuiz(
            int userId,
            [FromQuery] string topic = "programming",
            [FromQuery] string difficulty = "medium")
        {
            // Check subscription
            var subscription = _context.Subscriptions
                .FirstOrDefault(s => s.UserId == userId && s.IsActive);

            if (subscription == null || subscription.Plan != "Premium")
            {
                return StatusCode(403, "Premium subscription required.");
            }

            var questions = await _aiService.GenerateQuizAsync(topic, difficulty);

            return Ok(questions);
        }
    }
}