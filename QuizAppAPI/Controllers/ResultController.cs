using Quiz.Models;
using System.Collections.Generic;
using System.Linq;

namespace Quiz.Controllers
{
    public class ResultController
    {
        private readonly AppDbContext _context;

        public ResultController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Calculate Score
        public int CalculateScore(int correctAnswers, int totalQuestions)
        {
            if (totalQuestions == 0) return 0;
            return (correctAnswers * 100) / totalQuestions;
        }

        // ✅ Save Result
        public void SaveResult(int userId, int quizItemId, int score)
        {
            var result = new Result
            {
                UserId = userId,
                QuizItemId = quizItemId,
                Score = score,
                TakenAt = DateTime.UtcNow
            };

            _context.Results.Add(result);
            _context.SaveChanges();
        }

        // ✅ Get Results by User (History Page)
        public List<Result> GetResultsByUser(int userId)
        {
            return _context.Results
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.TakenAt)
                .ToList();
        }
    }
}