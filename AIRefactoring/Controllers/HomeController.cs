using AIRefactoring.Database;
using AIRefactoring.Models;
using Microsoft.AspNetCore.Mvc;

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

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult GetSessions(Guid guestIdentifier)
		{
			var model = new UserSessionsModel()
			{
				UserSessions = [.. dbContext.UserSessions.Where(x => x.GuestIdentifier == guestIdentifier)]
			};

			return PartialView("~/Views/Home/PartialViews/_UserSessionsPartial.cshtml", model);
		}
	}
}