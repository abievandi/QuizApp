namespace QuizAppAPI.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Difficulty { get; set; } = "Easy";
        public bool IsPremium { get; set; }
        public List<Question> Questions { get; set; } = new();
    }
}