using Microsoft.AspNetCore.Mvc;
using CodeLingoAPI.Data;
using CodeLingoAPI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLingoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SubscriptionController(AppDbContext context)
        {
            _context = context;
        }

        // GET USER SUBSCRIPTION
        [HttpGet("{userId}")]
        public IActionResult GetSubscription(int userId)
        {
            var subscription = _context.Subscriptions
                .FirstOrDefault(s => s.UserId == userId && s.IsActive);

            if (subscription == null)
                return NotFound("No active subscription.");

            return Ok(subscription);
        }

        //  UPGRADE TO PREMIUM
        [HttpPost("upgrade/{userId}")]
        public async Task<IActionResult> UpgradeToPremium(int userId)
        {
            var subscription = _context.Subscriptions
                .FirstOrDefault(s => s.UserId == userId && s.IsActive);

            if (subscription == null)
                return NotFound("Subscription not found.");

            subscription.Plan = "Premium";
            subscription.StartDate = DateTime.UtcNow;
            subscription.EndDate = DateTime.UtcNow.AddMonths(1);

            await _context.SaveChangesAsync();

            return Ok("Upgraded to Premium.");
        }

        //  CHECK IF USER IS PREMIUM
        [HttpGet("isPremium/{userId}")]
        public IActionResult IsPremium(int userId)
        {
            var subscription = _context.Subscriptions
                .FirstOrDefault(s => s.UserId == userId && s.IsActive);

            if (subscription == null)
                return Ok(false);

            return Ok(subscription.Plan == "Premium");
        }
    }
}