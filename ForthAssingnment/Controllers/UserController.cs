using ForthAssignment.Core.Aplication.Core;
using ForthAssignment.Core.Aplication.Interfaces.Contracts;
using ForthAssignment.Core.Aplication.Models.User;
using ForthAssignment.Core.Aplication.Utils.FileHandler;
using ForthAssignment.Core.Aplication.Utils.UserAuth;

using Microsoft.AspNetCore.Mvc;

namespace ForthAssingnment.Presentation.WepApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserAuth _userAuth;
        private readonly IFileHandler _fileHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string basePath = "/Images/UserProfileImages";

        public UserController(IUserService userService, IUserAuth userAuth, IFileHandler fileHandler, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _userAuth = userAuth;
            _fileHandler = fileHandler;
            _httpContextAccessor = httpContextAccessor;
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
        public async Task<IActionResult> LogIn(string username, string password)
        {
            if (_userAuth.IsUserLogin()) return RedirectToAction("Index", "Post");

            Result<UserModel> result = new();
            try
            {
                result = await _userService.Login(username, password);

                if (!result.IsSuccess)
                {
                    return View();
                }

                return RedirectToAction("Index", "Post");
            }
            catch
            {
                return View();
            }
        }

   
        public async Task<IActionResult> LogOut()
        {
            if (!_userAuth.IsUserLogin()) return RedirectToAction("LogIn", "User");
            if (!_userAuth.IsUserActivated()) return RedirectToAction("NotActivated", "Home");


            _httpContextAccessor.HttpContext.Session.Remove("user");

            return View("Login");

        }
        // GET: UserController/Create
        public async Task<IActionResult> Register()
        {
            if (_userAuth.IsUserLogin()) return RedirectToAction("Index", "Post");

            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserSaveModel saveModel)
        {
            if (_userAuth.IsUserLogin()) return RedirectToAction("Index", "Post");
            Result<UserSaveModel> result = new();
            try
            {
                if (!saveModel.Password.Equals(saveModel.ConfirmPassword))
                {
                    ViewBag.messageError = "The passwords must mach";
                    return View();
                }


                result = await _userService.Save(saveModel);


                if (!result.IsSuccess)
                {
                    ViewBag.messageError = result.Message;
                    return View();
                }


                result.Data.ProfileImageUrl = _fileHandler.UploudFile(saveModel.File, basePath, result.Data.Id);

                result = await _userService.Update(result.Data);

                if (!result.IsSuccess)
                {
                    ViewBag.messageError = result.Message;
                    return View();
                }

                ViewBag.messageSucces = "User was created without problems, now Login ";
                return RedirectToAction("LogIn", "User");
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> ChangePassword()
        {
            if (_userAuth.IsUserLogin()) return RedirectToAction("Index", "Post");

            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string username)
        {
            if (_userAuth.IsUserLogin()) return RedirectToAction("Index", "Post");
            Result<UserModel> ressult = new();
            try
            {

                ressult = await _userService.ChangePassword(username);

                if (!ressult.IsSuccess)
                {
                    ViewBag.messageError = "User Dosent exist in app";
                    return View();
                }

                ViewBag.messageSucces = "Your password has been changed, check the email sended to you to check it";
                return RedirectToAction(nameof(LogIn));
            }
            catch
            {
                return View();
            }
        }


        public async Task<IActionResult> ActivateUser()
        {
            if (_userAuth.IsUserActivated()) return RedirectToAction("Index", "Post");

            return View();
        }


        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivateUser(string code)
        {
            Result<UserModel> result = new();
            try
            {

                result = await _userService.ActivateUser(code);

                if (!result.IsSuccess)
                {
                    ViewBag.messageError = result.Message;
                    return RedirectToAction("NotActivated", "Home");
                }
                return View();
            }
            catch
            {
                throw;
            }
        }


        // GET: UserController/Edit/5
        public async Task<IActionResult> EditUser(Guid id)
        {
            if (!_userAuth.IsUserLogin()) return RedirectToAction("LogIn", "User");
            if (!_userAuth.IsUserActivated()) return RedirectToAction("NotActivated", "Home");

            Result<UserModel> result = new();
            try
            {
                result = await _userService.GetById(id);
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

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(UserSaveModel saveModel)
        {
            if (!_userAuth.IsUserLogin()) return RedirectToAction("LogIn", "User");
            if (!_userAuth.IsUserActivated()) return RedirectToAction("NotActivated", "Home");
            Result<UserSaveModel> result = new();
            try
            {
                saveModel.ProfileImageUrl = _fileHandler.UpdateFile(saveModel.File, saveModel.Id, basePath, saveModel.ProfileImageUrl);
                result = await _userService.Update(saveModel);



                if (!result.IsSuccess)
                {
                    ViewBag.messageError = "Error";
                    return RedirectToAction("EditUser", "User");
                }
                ViewBag.messageSucces = "User Updated";
                return RedirectToAction("Index", "Post");
            }
            catch
            {
                ViewBag.messageError = "Error";
                return RedirectToAction("EditUser", "User");
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
