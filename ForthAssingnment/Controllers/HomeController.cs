using ForthAssignment.Core.Aplication.Utils.UserAuth;
using ForthAssingnment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ForthAssingnment.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUserAuth _userAuth;

		public HomeController(ILogger<HomeController> logger, IUserAuth userAuth)
		{
			_logger = logger;
			_userAuth = userAuth;
		}

		public IActionResult Index()
		{
			if (!_userAuth.IsUserLogin()) return RedirectToAction("LogIn", "User");
			if (!_userAuth.IsUserActivated()) return RedirectToAction("NotActivated", "Home");

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult NotActivated()
		{
			return View();
		}

		// POST: UserController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> NotActivated(Guid id)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
