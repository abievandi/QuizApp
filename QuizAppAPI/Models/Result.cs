namespace QuizAppAPI.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuizId { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public int XPEarned { get; set; }
        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
    }
}