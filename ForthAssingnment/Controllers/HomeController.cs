using ForthAssignment.Core.Aplication.Interfaces.Contracts;
using ForthAssignment.Core.Aplication.Utils.UserAuth;
using ForthAssingnment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace ForthAssingnment.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUserAuth _userAuth;
		private readonly IEmailService _emailService;

		public HomeController(ILogger<HomeController> logger, IUserAuth userAuth, IEmailService emailService)
		{
			_logger = logger;
			_userAuth = userAuth;
			_emailService = emailService;
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

		public async Task<IActionResult> NotActivated()
		{
			return View();
		}

		// POST: UserController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> NotActivated(Guid id, string Email)
		{
			try
			{
				_emailService.SendAsync(new ForthAssignment.Core.Aplication.Dtos.EmailRequest
				{
					EmailTo = Email,
					EmailSubject = "Let's activate your user ",

					EmailBody = new StringBuilder().Append("<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Activate Your Account</title>\r\n    <style>\r\n        body {\r\n            font-family: Arial, sans-serif;\r\n            line-height: 1.6;\r\n            color: #333;\r\n        }\r\n        .container {\r\n            max-width: 600px;\r\n            margin: 0 auto;\r\n            padding: 20px;\r\n            border: 1px solid #ddd;\r\n            border-radius: 5px;\r\n        }\r\n        .button {\r\n            display: inline-block;\r\n            padding: 10px 20px;\r\n            margin-top: 20px;\r\n            font-size: 16px;\r\n            color: #fff;\r\n            background-color: #28a745;\r\n            text-align: center;\r\n            text-decoration: none;\r\n            border-radius: 5px;\r\n        }\r\n        .button:hover {\r\n            background-color: #218838;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"container\">\r\n        <h1>Welcome to Our Service</h1>\r\n        <p>Hello,</p>\r\n        <p>Thank you for registering with us. Please click the button below to activate your account:</p>\r\n        <a href=\"https://localhost:7143/ActivateUser?id=" + id + "\" class=\"button\">Activate Account</a>\r\n        <p>If you did not register for this account, please ignore this email.</p>\r\n        <p>Best regards,<br>The Team</p>\r\n    </div>\r\n</body>\r\n</html>").ToString()

				});
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
