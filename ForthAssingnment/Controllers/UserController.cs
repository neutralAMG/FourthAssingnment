using ForthAssignment.Core.Aplication.Interfaces.Contracts;
using ForthAssignment.Core.Aplication.Models.User;
using ForthAssignment.Core.Aplication.Utils.FileHandler;
using ForthAssignment.Core.Aplication.Utils.UserAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForthAssingnment.Presentation.WepApp.Controllers
{
	public class UserController : Controller
	{
		private readonly IUserService _userService;
		private readonly IUserAuth _userAuth;
		private readonly IFileHandler _fileHandler;

		public UserController(IUserService userService, IUserAuth userAuth, IFileHandler fileHandler)
        {
			_userService = userService;
			_userAuth = userAuth;
			_fileHandler = fileHandler;
		}
        // GET: UserController
        public async Task<IActionResult> Index()
		{
			return View();
		}

		// GET: UserController/Details/5
		public async Task<IActionResult> Details(int id)
		{
			return View();
		}
		// GET: UserController/Create
		public ActionResult LogIn()
		{
			return View();
		}

		// POST: UserController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> LogIn(string Username, string Password)
		{
			if (_userAuth.IsUserLogin()) return RedirectToAction("Index", "Home");

			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
		// GET: UserController/Create
		public async Task<IActionResult> Register()
		{
			if (_userAuth.IsUserLogin()) return RedirectToAction("Index", "Home");

			return View();
		}

		// POST: UserController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(UserSaveModel saveModel)
		{
			if (_userAuth.IsUserLogin()) return RedirectToAction("Index", "Home");

			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// POST: UserController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ActivateUser(Guid id)
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
		// GET: UserController/Edit/5
		public async Task<IActionResult> EditUser(Guid id)
		{
			if (!_userAuth.IsUserLogin()) return RedirectToAction("LogIn", "User");
			if (!_userAuth.IsUserActivated()) return RedirectToAction("NotActivated", "Home");

			return View();
		}

		// POST: UserController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditUser(UserSaveModel saveModel)
		{
			if (!_userAuth.IsUserLogin()) return RedirectToAction("LogIn", "User");
			if (!_userAuth.IsUserActivated()) return RedirectToAction("NotActivated", "Home");

			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: UserController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: UserController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
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
	}
}
