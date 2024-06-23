using ForthAssignment.Core.Aplication.Interfaces.Contracts;
using ForthAssignment.Core.Aplication.Models.UserFriend;
using ForthAssignment.Core.Aplication.Utils.UserAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForthAssingnment.Presentation.WepApp.Controllers
{
	public class UserFriendController : Controller
	{
		private readonly IUserFriendService _userFriendService;
		private readonly IUserAuth _userAuth;

		public UserFriendController(IUserFriendService userFriendService, IUserAuth userAuth)
		{
			_userFriendService = userFriendService;
			_userAuth = userAuth;
		}
		// GET: UserFriendController
		public async Task<IActionResult> Index()
		{
			return View();
		}

		// GET: UserFriendController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: UserFriendController/Create
		public async Task<IActionResult> SaveANewFriend()
		{
			if (!_userAuth.IsUserLogin()) return RedirectToAction("LogIn", "User");
			if (!_userAuth.IsUserActivated()) return RedirectToAction("NotActivated", "Home");

			return View();
		}

		// POST: UserFriendController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SaveANewFriend(string username, UserFriendSaveModel saveModel)
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

		// GET: UserFriendController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: UserFriendController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
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

		// GET: UserFriendController/Delete/5
		public async Task<IActionResult> DeleteUserFriend(Guid id)
		{
			if (!_userAuth.IsUserLogin()) return RedirectToAction("LogIn", "User");
			if (!_userAuth.IsUserActivated()) return RedirectToAction("NotActivated", "Home");

			return View();
		}

		// POST: UserFriendController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteUserFriend(Guid id, IFormCollection collection)
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
	}
}
