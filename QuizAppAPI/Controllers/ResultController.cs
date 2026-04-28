using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeLingoAPI.Data;
using CodeLingoAPI.Models;

namespace CodeLingoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ResultController(AppDbContext context)
        {
            _context = context;
        }

        //  GET ALL RESULTS FOR A USER
        [HttpGet("user/{userId}")]
        public IActionResult GetUserResults(int userId)
        {
            var results = _context.Results
                .Include(r => r.Quiz)
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.CompletedAt)
                .Select(r => new
                {
                    r.ResultId,
                    QuizTitle = r.Quiz.Title,
                    r.Score,
                    r.TotalQuestions,
                    r.CorrectAnswers,
                    r.CompletedAt
                })
                .ToList();

            return Ok(results);
        }

        //  GET SINGLE RESULT DETAILS
        [HttpGet("{resultId}")]
        public IActionResult GetResult(int resultId)
        {
            var result = _context.Results
                .Include(r => r.Quiz)
                .FirstOrDefault(r => r.ResultId == resultId);

            if (result == null)
                return NotFound();

            return Ok(new
            {
                result.ResultId,
                QuizTitle = result.Quiz.Title,
                result.Score,
                result.TotalQuestions,
                result.CorrectAnswers,
                result.CompletedAt
            });
        }
    }
}