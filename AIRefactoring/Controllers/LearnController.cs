using AIRefactoring.Database;
using Microsoft.AspNetCore.Mvc;

namespace AIRefactoring.Controllers
{
	public class LearnController : Controller
	{
		private readonly ILogger<LearnController> _logger;
		private readonly ApplicationDbContext dbContext;

		public LearnController(ILogger<LearnController> logger, ApplicationDbContext dbContext)
		{
			_logger = logger;
			this.dbContext = dbContext;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet("/learn/{name}")]
		public IActionResult RefactoringCategory(string name)
		{
			var category = dbContext.RefactoringCategories.First(c => c.Name == name);

			ViewBag.PageContent = category.Content ?? "";
			ViewBag.Category = category.Name ?? "";

			return View();
		}
	}
}
