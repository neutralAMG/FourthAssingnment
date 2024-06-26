using ForthAssignment.Core.Aplication.Core;
using ForthAssignment.Core.Aplication.Interfaces.Contracts;
using ForthAssignment.Core.Aplication.Models.Post;
using ForthAssignment.Core.Aplication.Utils.FileHandler;
using ForthAssignment.Core.Aplication.Utils.UserAuth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForthAssingnment.Presentation.WepApp.Controllers
{
	public class PostController : Controller
	{
		private readonly IPostService _postService;
		private readonly IUserAuth _userAuth;
		private readonly IFileHandler _fileHandler;
		private const string baseUrl = "/Images/PostImages";

		public PostController(IPostService postService, IUserAuth userAuth, IFileHandler fileHandler)
		{
			_postService = postService;
			_userAuth = userAuth;
			_fileHandler = fileHandler;
		}
		// GET: PostController
		public async Task<IActionResult> Index()
		{
            if (!_userAuth.IsUserLogin()) return RedirectToAction("LogIn", "User");
            if (!_userAuth.IsUserActivated()) return RedirectToAction("NotActivated", "Home");

            Result<List<PostModel>> result = new();
			try
			{
				result = await _postService.GetAll();


				if (!result.IsSuccess)
				{

				}
				return View(result.Data);
			}
			catch
			{

			}
			return View();
		}

		// GET: PostController/Details/5
		public async Task<IActionResult> Details(Guid id)
		{
			Result<PostModel> result = new();
			try
			{


			}
			catch
			{

			}
			return View();
		}

		// GET: PostController/Create
		public async Task<IActionResult> PostANewPublication()
		{
			if (!_userAuth.IsUserLogin()) return RedirectToAction("LogIn", "User");
			if (!_userAuth.IsUserActivated()) return RedirectToAction("NotActivated", "Home");

			return View();
		}

		// POST: PostController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> PostANewPublication(PostSaveModel saveModel)
		{
			if (!_userAuth.IsUserLogin()) return RedirectToAction("LogIn", "User");
			if (!_userAuth.IsUserActivated()) return RedirectToAction("NotActivated", "Home");
			Result<PostSaveModel> result = new();
			try
			{

				if (saveModel.File != null && saveModel.VideoUrl != null)
				{
					ViewBag.messageError = "You cant uploud a post with an image and a video chouse one or make two post's if it is what you want";
					return RedirectToAction(nameof(Index));
				}

				result = await _postService.Save(saveModel);

				if (!result.IsSuccess)
				{

				}
				if (saveModel.File != null)
				{
					result.Data.PostImgUrl = _fileHandler.UploudFile(saveModel.File, baseUrl, result.Data.Id);
					result = await _postService.Update(result.Data);
					return RedirectToAction(nameof(Index));
				}
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: PostController/Edit/5
		public async Task<IActionResult> EditPost(Guid id)
		{
			if (!_userAuth.IsUserLogin()) return RedirectToAction("LogIn", "User");
			if (!_userAuth.IsUserActivated()) return RedirectToAction("NotActivated", "Home");

			Result<PostModel> result = new();
			try
			{
				result = await _postService.GetById(id);

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

		// POST: PostController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditPost(PostSaveModel saveModel)
		{
			if (!_userAuth.IsUserLogin()) return RedirectToAction("LogIn", "User");
			if (!_userAuth.IsUserActivated()) return RedirectToAction("NotActivated", "Home");
            Result<PostSaveModel> result = new();

            try
			{

				saveModel.PostImgUrl = _fileHandler.UpdateFile(saveModel.File, saveModel.Id, baseUrl, saveModel.PostImgUrl);
				result = await _postService.Update(saveModel);

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut(PostSaveModel saveModel)
        {
            if (!_userAuth.IsUserLogin()) return RedirectToAction("LogIn", "User");
            if (!_userAuth.IsUserActivated()) return RedirectToAction("NotActivated", "Home");
            Result<PostSaveModel> result = new();

            try
            {

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Delete/5
        public async Task<IActionResult> DeletePost(Guid id)
		{
			if (!_userAuth.IsUserLogin()) return RedirectToAction("LogIn", "User");
			if (!_userAuth.IsUserActivated()) return RedirectToAction("NotActivated", "Home");

            Result<PostModel> result = new();
            try
            {
                result = await _postService.GetById(id);

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

		// POST: PostController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeletePost(Guid id, IFormCollection collection)
		{
			if (!_userAuth.IsUserLogin()) return RedirectToAction("LogIn", "User");
			if (!_userAuth.IsUserActivated()) return RedirectToAction("NotActivated", "Home");

            Result<PostModel> result = new();
            try
            {
                
                result = await _postService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
	}
}
