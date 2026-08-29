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

		public IActionResult Index()
		{
			return View(new HomeModel());
		}
	}
}