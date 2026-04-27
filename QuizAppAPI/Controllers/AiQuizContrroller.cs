using Microsoft.AspNetCore.Mvc;
using CodeLingoAPI.Services;

namespace CodeLingoAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AIQuizController : ControllerBase
	{
		private readonly AIQuizService _aiService;

		public AIQuizController()
		{
			_aiService = new AIQuizService();
		}

		// Call this to generate AI questions
		// Example: GET api/aiquiz/generate?topic=JavaScript&difficulty=easy
		[HttpGet("generate")]
		public async Task<IActionResult> GenerateQuiz(
			[FromQuery] string topic = "programming",
			[FromQuery] string difficulty = "medium")
		{
			var questions = await _aiService.GenerateQuizAsync(topic, difficulty);
			return Ok(questions);
		}
	}
}