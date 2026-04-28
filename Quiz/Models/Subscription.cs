using Quiz.Models;   // ← THIS LINE WAS MISSING

namespace Quiz.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public string Plan { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; }
    }
}