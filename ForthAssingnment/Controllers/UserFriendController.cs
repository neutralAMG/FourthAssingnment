using ForthAssignment.Core.Aplication.Interfaces.Contracts;
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
		public ActionResult Index()
		{
			return View();
		}

		// GET: UserFriendController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: UserFriendController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: UserFriendController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
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
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: UserFriendController/Delete/5
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
