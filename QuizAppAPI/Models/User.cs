namespace QuizAppAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int XP { get; set; }
        public int Level { get; set; } = 1;
        public int Streak { get; set; }
        public DateTime LastActivityDate { get; set; } = DateTime.UtcNow;
        public string SubscriptionType { get; set; } = "Free"; // "Free" or "Premium"
    }
}