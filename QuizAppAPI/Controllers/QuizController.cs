using Microsoft.AspNetCore.Mvc;
using QuizAppAPI.Data;
using QuizAppAPI.DTOs;
using QuizAppAPI.Models;

namespace QuizAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        // GET /api/quiz/categories
        [HttpGet("categories")]
        public ActionResult<IEnumerable<string>> GetCategories()
        {
            var categories = InMemoryStore.Quizzes
                .Select(q => q.Category)
                .Distinct()
                .ToList();
            return Ok(categories);
        }

        // GET /api/quiz
        // GET /api/quiz?category=Math
        [HttpGet]
        public ActionResult<IEnumerable<QuizSummaryDto>> GetQuizzes([FromQuery] string? category = null)
        {
            var quizzes = InMemoryStore.Quizzes.AsEnumerable();

            if (!string.IsNullOrEmpty(category))
                quizzes = quizzes.Where(q => q.Category.Equals(category, StringComparison.OrdinalIgnoreCase));

            var result = quizzes.Select(q => new QuizSummaryDto
            {
                Id = q.Id,
                Title = q.Title,
                Category = q.Category,
                Difficulty = q.Difficulty,
                QuestionCount = q.Questions.Count,
                IsPremium = q.IsPremium
            }).ToList();

            return Ok(result);
        }

        // GET /api/quiz/1/start?userId=1
        // Returns quiz with questions, but WITHOUT correct answers
        [HttpGet("{id}/start")]
        public ActionResult<QuizWithQuestionsDto> StartQuiz(int id, [FromQuery] int userId)
        {
            var quiz = InMemoryStore.Quizzes.FirstOrDefault(q => q.Id == id);
            if (quiz == null) return NotFound($"Quiz {id} not found.");

            // Premium gate — later this will call Abie's SubscriptionController
            if (quiz.IsPremium)
            {
                var user = InMemoryStore.Users.FirstOrDefault(u => u.Id == userId);
                if (user == null) return Unauthorized("User not found.");
                if (user.SubscriptionType != "Premium")
                    return StatusCode(403, "Premium subscription required for this quiz.");
            }

            return Ok(new QuizWithQuestionsDto
            {
                Id = quiz.Id,
                Title = quiz.Title,
                Category = quiz.Category,
                Questions = quiz.Questions.Select(q => new QuestionDto
                {
                    Id = q.Id,
                    Text = q.Text,
                    OptionA = q.OptionA,
                    OptionB = q.OptionB,
                    OptionC = q.OptionC,
                    OptionD = q.OptionD
                    // CorrectOption intentionally left out
                }).ToList()
            });
        }

        // POST /api/quiz/1/submit
        [HttpPost("{id}/submit")]
        public ActionResult<QuizResultDto> SubmitQuiz(int id, [FromBody] SubmitQuizDto submission)
        {
            var quiz = InMemoryStore.Quizzes.FirstOrDefault(q => q.Id == id);
            if (quiz == null) return NotFound();

            var user = InMemoryStore.Users.FirstOrDefault(u => u.Id == submission.UserId);
            if (user == null) return NotFound("User not found.");

            int correct = 0;
            var feedback = new List<QuestionFeedbackDto>();

            foreach (var question in quiz.Questions)
            {
                var userAnswer = submission.Answers.FirstOrDefault(a => a.QuestionId == question.Id);
                var selected = userAnswer?.SelectedOption ?? string.Empty;
                bool isCorrect = selected.Equals(question.CorrectOption, StringComparison.OrdinalIgnoreCase);

                if (isCorrect) correct++;

                feedback.Add(new QuestionFeedbackDto
                {
                    QuestionId = question.Id,
                    SelectedOption = selected,
                    CorrectOption = question.CorrectOption,
                    IsCorrect = isCorrect
                });
            }

            int totalQuestions = quiz.Questions.Count;
            int xpEarned = correct * 10; // 10 XP per correct answer

            // Save the result (Noman's ResultController will read from this list)
            var newResultId = InMemoryStore.Results.Count == 0 ? 1 : InMemoryStore.Results.Max(r => r.Id) + 1;
            InMemoryStore.Results.Add(new Result
            {
                Id = newResultId,
                UserId = user.Id,
                QuizId = quiz.Id,
                Score = correct,
                TotalQuestions = totalQuestions,
                XPEarned = xpEarned,
                CompletedAt = DateTime.UtcNow
            });

            // Update user XP & level
            user.XP += xpEarned;
            user.Level = 1 + (user.XP / 100); // Level up every 100 XP

            return Ok(new QuizResultDto
            {
                Score = correct,
                TotalQuestions = totalQuestions,
                XPEarned = xpEarned,
                Feedback = feedback
            });
        }
    }
}