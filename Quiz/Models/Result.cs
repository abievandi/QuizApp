namespace Quiz.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int QuizItemId { get; set; }          // ← updated
        public QuizItem? QuizItem { get; set; }      // ← updated
        public int Score { get; set; }
        public DateTime TakenAt { get; set; } = DateTime.UtcNow;
    }
}