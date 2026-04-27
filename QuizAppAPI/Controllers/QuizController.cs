using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeLingoAPI.Data;
using CodeLingoAPI.Models;

namespace CodeLingoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuizController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/quiz/questions
        [HttpGet("questions")]
        public async Task<IActionResult> GetQuestions()
        {
            var questions = await _context.Questions.ToListAsync();
            return Ok(questions);
        }

        // GET: api/quiz/questions/{quizId}
        [HttpGet("questions/{quizId}")]
        public async Task<IActionResult> GetQuestionsByQuiz(int quizId)
        {
            var questions = await _context.Questions
                .Where(q => q.QuizId == quizId)
                .ToListAsync();
            return Ok(questions);
        }
    }
}