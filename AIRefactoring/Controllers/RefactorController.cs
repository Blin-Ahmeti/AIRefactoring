using AIRefactoring.Database;
using AIRefactoring.Entities;
using AIRefactoring.Gemini;
using AIRefactoring.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

		public IActionResult Index(Guid? sessionId)
		{
			var userSession = dbContext.UserSessions
				.Include(x => x.CodeArtifacts)
				.FirstOrDefault(x => x.Id == sessionId);

			return View(new RefactorModel() { UserSession = userSession });
		}

		[HttpGet("GetSessions")]
		public IActionResult GetSessions(Guid guestIdentifier, Guid? userSessionId)
		{
			var model = new UserSessionsModel()
			{
				UserSessions = [.. dbContext.UserSessions
					.Where(x => x.GuestIdentifier == guestIdentifier)
					.OrderByDescending(x => x.CodeArtifacts
					.Max(a => (DateTime?)a.CreatedAt))],
				CurrentSessionId = userSessionId
			};

			return PartialView("~/Views/Refactor/PartialViews/_UserSessionsPartial.cshtml", model);
		}

		[HttpPost]
		public async Task<IActionResult> Refactor([FromBody] RefactorRequest request)
		{
			var userSession = dbContext.UserSessions.FirstOrDefault(x => x.Id == request.UserSessionId)
				?? new UserSession() { GuestIdentifier = request.GuestIdentifier };

			bool includeTitle = false;
			if (userSession.Id == Guid.Empty)
			{
				includeTitle = true;
				dbContext.Add(userSession);
			}

			var response = await codeRefactorService.RefactorCodeAsync(request.Prompt, includeTitle);
			if (includeTitle)
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