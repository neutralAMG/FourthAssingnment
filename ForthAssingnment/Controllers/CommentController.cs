using ForthAssignment.Core.Aplication.Interfaces.Contracts;
using ForthAssignment.Core.Aplication.Utils.UserAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForthAssingnment.Presentation.WepApp.Controllers
{
	public class CommentController : Controller
	{
		private readonly ICommentService _commentService;
		private readonly IUserAuth _userAuth;

		public CommentController(ICommentService commentService, IUserAuth userAuth)
		{
			_commentService = commentService;
			_userAuth = userAuth;
		}
		// GET: CommentController
		public ActionResult Index()
		{
			return View();
		}

		// GET: CommentController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: CommentController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: CommentController/Create
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

		// GET: CommentController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: CommentController/Edit/5
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

		// GET: CommentController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: CommentController/Delete/5
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
