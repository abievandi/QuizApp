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

        //  GET ALL CATEGORIES
        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            var categories = _context.Quizzes
                .Select(q => q.Category)
                .Distinct()
                .ToList();

            return Ok(categories);
        }

        //  GET QUIZZES (OPTIONAL FILTER)
        [HttpGet]
        public IActionResult GetQuizzes(string? category = null)
        {
            var quizzes = _context.Quizzes.Include(q => q.Questions).AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                quizzes = quizzes.Where(q => q.Category == category);
            }

            var result = quizzes.Select(q => new
            {
                q.QuizId,
                q.Title,
                q.Category,
                q.Difficulty,
                QuestionCount = q.Questions.Count,
                q.IsPremium
            }).ToList();

            return Ok(result);
        }

        //  START QUIZ
        [HttpGet("{quizId}/start")]
        public IActionResult StartQuiz(int quizId, int userId)
        {
            var quiz = _context.Quizzes
                .Include(q => q.Questions)
                .FirstOrDefault(q => q.QuizId == quizId);

            if (quiz == null)
                return NotFound("Quiz not found.");

            //  Check subscription
            if (quiz.IsPremium)
            {
                var subscription = _context.Subscriptions
                    .FirstOrDefault(s => s.UserId == userId && s.IsActive);

                if (subscription == null || subscription.Plan != "Premium")
                {
                    return StatusCode(403, "Premium required.");
                }
            }

            return Ok(new
            {
                quiz.QuizId,
                quiz.Title,
                quiz.Category,
                Questions = quiz.Questions.Select(q => new
                {
                    q.QuestionId,
                    q.QuestionText,
                    q.OptionA,
                    q.OptionB,
                    q.OptionC,
                    q.OptionD
                })
            });
        }

        //  SUBMIT QUIZ
        [HttpPost("{quizId}/submit")]
        public async Task<IActionResult> SubmitQuiz(int quizId, [FromBody] Dictionary<int, string> answers, int userId)
        {
            var quiz = _context.Quizzes
                .Include(q => q.Questions)
                .FirstOrDefault(q => q.QuizId == quizId);

            if (quiz == null)
                return NotFound();

            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
                return NotFound("User not found.");

            int correct = 0;

            foreach (var question in quiz.Questions)
            {
                if (answers.TryGetValue(question.QuestionId, out string selected))
                {
                    if (selected == question.CorrectOption)
                    {
                        correct++;
                    }
                }
            }

            int total = quiz.Questions.Count;
            int score = correct * 10;

            //  Save result
            var result = new Result
            {
                UserId = user.UserId,
                QuizId = quiz.QuizId,
                Score = score,
                TotalQuestions = total,
                CorrectAnswers = correct
            };

            _context.Results.Add(result);

            //  Update user stats
            user.TotalPoints += score;
            user.Level = 1 + (user.TotalPoints / 100);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Score = correct,
                TotalQuestions = total,
                XP = score
            });
        }
    }
}        
    
