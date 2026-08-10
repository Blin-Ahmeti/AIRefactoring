using AIRefactoring.Database;
using AIRefactoring.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIRefactoring.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext dbContext;

		public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
		{
			_logger = logger;
			this.dbContext = dbContext;
		}

		public IActionResult Index(Guid? sessionId)
		{
			var userSession = dbContext.UserSessions
				.Include(x => x.CodeArtifacts)
				.FirstOrDefault(x => x.Id == sessionId);

			return View(new HomeModel() { UserSession = userSession });
		}

		[HttpGet]
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

			return PartialView("~/Views/Home/PartialViews/_UserSessionsPartial.cshtml", model);
		}
	}
}