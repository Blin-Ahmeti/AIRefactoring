using AIRefactoring.Gemini;
using AIRefactoring.Models;
using Microsoft.AspNetCore.Mvc;

namespace AIRefactoring.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class RefactorController : Controller
	{
		private readonly ICodeRefactorService codeRefactorService;

		public RefactorController(ICodeRefactorService codeRefactorService)
        {
			this.codeRefactorService = codeRefactorService;
		}

        [HttpPost]
		public async Task<IActionResult> Refactor([FromBody] RefactorRequest request)
		{
			//var response = await codeRefactorService.RefactorCodeAsync(request.Prompt);
			//reached rate limit today
			return Ok(new
			{
				message = "Refactoring request received",
				prompt = request.Prompt,
				response = "good response"
				//response
			});
		}
	}
}