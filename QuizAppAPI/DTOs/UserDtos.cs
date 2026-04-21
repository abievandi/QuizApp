namespace QuizAppAPI.DTOs
{
    public class UserProfileDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int XP { get; set; }
        public int Level { get; set; }
        public int Streak { get; set; }
        public string SubscriptionType { get; set; } = "Free";
    }

    public class UserStatsDto
    {
        public int XP { get; set; }
        public int Level { get; set; }
        public int Streak { get; set; }
        public int XPToNextLevel { get; set; }
    }
}