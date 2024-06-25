using ForthAssignment.Core.Aplication.Core;
using ForthAssignment.Core.Aplication.Interfaces.Contracts;
using ForthAssignment.Core.Aplication.Models.Post;
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
        private readonly IPostService _postService;

        public UserFriendController(IUserFriendService userFriendService, IUserAuth userAuth, IPostService postService)
		{
			_userFriendService = userFriendService;
			_userAuth = userAuth;
            _postService = postService;
        }
		// GET: UserFriendController
		public async Task<IActionResult> Index()
		{
            if (!_userAuth.IsUserLogin()) return RedirectToAction("LogIn", "User");
            if (!_userAuth.IsUserActivated()) return RedirectToAction("NotActivated", "Home");
			Result<List<PostModel>> result = new();
            try
			{

				result = await _postService.GetAllFrindsPosts();
               return View(result.Data);
			}
			catch
			{
				throw;
			}
			
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
			Result<UserFriendSaveModel> result = new();
			try
			{
				result = await _userFriendService.Save(saveModel);

				if (!result.IsSuccess)
				{
					ViewBag.messageError = result.Message;	

					return RedirectToAction(nameof(Index));
                }
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


            Result<UserFriendModel> result = new();
            try
            {
                result = await _userFriendService.GetById(id);

                if (!result.IsSuccess)
                {

                }

                return View(result.Data);
            }
            catch
            {
                throw;
            }
        }

		// POST: UserFriendController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteUserFriend(Guid id, IFormCollection collection)
		{
			if (!_userAuth.IsUserLogin()) return RedirectToAction("LogIn", "User");
			if (!_userAuth.IsUserActivated()) return RedirectToAction("NotActivated", "Home");


            Result<UserFriendModel> result = new();
            try
            {

                result = await _userFriendService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
	}
}
