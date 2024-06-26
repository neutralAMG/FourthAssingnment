using ForthAssignment.Core.Aplication.Core;
using ForthAssignment.Core.Aplication.Interfaces.Contracts;
using ForthAssignment.Core.Aplication.Models.Comment;
using ForthAssignment.Core.Aplication.Models.User;
using ForthAssignment.Core.Aplication.Services;
using ForthAssignment.Core.Aplication.Utils.FileHandler;
using ForthAssignment.Core.Aplication.Utils.UserAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ForthAssingnment.Presentation.WepApp.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IUserAuth _userAuth;
        private readonly IFileHandler _fileHandler;
        private const string baseUrl = "/Images/CommentImages";

        public CommentController(ICommentService commentService, IUserAuth userAuth, IFileHandler fileHandler)
        {
            _commentService = commentService;
            _userAuth = userAuth;
            _fileHandler = fileHandler;
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
        public async Task<IActionResult> PostANewComment()
        {
            if (!_userAuth.IsUserLogin()) return RedirectToAction("LogIn", "User");
            if (!_userAuth.IsUserActivated()) return RedirectToAction("NotActivated", "Home");

            return View();
        }

        // POST: CommentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostANewComment(string num, CommentSaveModel saveModel)
        {
            if (!_userAuth.IsUserLogin()) return RedirectToAction("LogIn", "User");
            if (!_userAuth.IsUserActivated()) return RedirectToAction("NotActivated", "Home");
            Result<CommentSaveModel> result = new();
            try
            {
                result = await _commentService.Save(saveModel);



                if (!result.IsSuccess)
                {

                }
                if (saveModel.File != null)
                {
                    result.Data.CommentImgUrl = _fileHandler.UploudFile(saveModel.File, baseUrl, result.Data.Id);
                    result = await _commentService.Update(result.Data);
                    if (num.Equals("1"))
                    {
                        return RedirectToAction("Index", "Post");
                    }
                    else if (num.Equals("2"))
                    {
                        return RedirectToAction("Index", "UserFriend");
                    }

                }
                if (num.Equals("1"))
                {
                    return RedirectToAction("Index", "Post");
                }

                return RedirectToAction("Index", "UserFriend");

            }
            catch
            {
                return View();
            }
        }


        public async Task<IActionResult> ReplyToAComment(Guid id)
        {
            if (!_userAuth.IsUserLogin()) return RedirectToAction("LogIn", "User");
            if (!_userAuth.IsUserActivated()) return RedirectToAction("NotActivated", "Home");
            Result<CommentModel> result = new();
            try
            {
                result = await _commentService.GetById(id);

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

        // POST: CommentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReplyToAComment(CommentSaveModel saveModel)
        {
            if (!_userAuth.IsUserLogin()) return RedirectToAction("LogIn", "User");
            if (!_userAuth.IsUserActivated()) return RedirectToAction("NotActivated", "Home");


            Result<CommentSaveModel> result = new();

            try
            {
                saveModel.Id = default;
                result = await _commentService.RespondComment(saveModel);



                if (!result.IsSuccess)
                {

                }
                if (saveModel.File != null)
                {
                    result.Data.CommentImgUrl = _fileHandler.UploudFile(saveModel.File, baseUrl, result.Data.Id);
                    result = await _commentService.Update(result.Data);
                    return RedirectToAction("Index", "Post");
                }

                return RedirectToAction("Index", "Post");
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
