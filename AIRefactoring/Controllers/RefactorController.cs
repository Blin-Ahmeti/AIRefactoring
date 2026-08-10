using AIRefactoring.Database;
using AIRefactoring.Entities;
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
		private readonly ApplicationDbContext dbContext;

		public RefactorController(ICodeRefactorService codeRefactorService, ApplicationDbContext dbContext)
		{
			this.codeRefactorService = codeRefactorService;
			this.dbContext = dbContext;
		}

		[HttpPost]
		public async Task<IActionResult> Refactor([FromBody] RefactorRequest request)
		{
			var userSession = dbContext.UserSessions.FirstOrDefault(x => x.Id == request.UserSessionId)
				?? new UserSession() { GuestIdentifier = request.GuestIdentifier };

			if (userSession.Id == Guid.Empty)
			{
				dbContext.Add(userSession);
			}

			var response = await codeRefactorService.RefactorCodeAsync(request.Prompt);
			userSession.Title = response.Title;

			var codeArtifact = new CodeArtifact
			{
				CreatedAt = DateTime.UtcNow,
				OriginalCode = request.Prompt,
				UserSessionId = userSession.Id,
				RefactoredCode = response.Code
			};

			dbContext.Add(codeArtifact);
			await dbContext.SaveChangesAsync();

			return Ok(new
			{
				prompt = request.Prompt,
				response = codeArtifact.RefactoredCode,
				createdAt = codeArtifact.CreatedAt,
				userSessionTitle = userSession.Title,
				userSessionId = userSession.Id
			});
		}
	}
}