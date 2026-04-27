namespace Quiz.Models
{
    public class QuizItem   // ← Renamed to QuizItem to avoid clashing with the Quiz namespace
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CreatedByUserId { get; set; }
        public User? CreatedBy { get; set; }
        public List<Question> Questions { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}