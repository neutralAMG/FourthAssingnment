

using AutoMapper;
using ForthAssignment.Core.Aplication.Core;
using ForthAssignment.Core.Aplication.Interfaces.Contracts;
using ForthAssignment.Core.Aplication.Interfaces.Repositories;
using ForthAssignment.Core.Aplication.Models.User;
using ForthAssignment.Core.Aplication.Utils.PasswordGenerator;
using ForthAssignment.Core.Aplication.Utils.PasswordHasher;
using ForthAssignment.Core.Aplication.Utils.UserSessionHandler;
using ForthAssignment.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace ForthAssignment.Core.Aplication.Services
{
    public class UserService : BaseService<UserSaveModel, UserModel, User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IEmailService _emailService;
        private readonly IPasswordGenerator _passwordGenerator;

        public UserService(IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IPasswordHasher passwordHasher, IEmailService emailService, IPasswordGenerator passwordGenerator) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
            _passwordGenerator = passwordGenerator;
        }

        public async Task<Result<UserModel>> ActivateUser(Guid id)
        {
            Result<UserModel> result = new();
            try
            {
                await _userRepository.ActivateUser(id);
                result.Message = "User Activated";
                return result;
            }
            catch
            {
                result.IsSuccess = false;
                result.Message = "Error activating the user";
                return result;
                throw;
            }
        }

        public async Task<Result<UserModel>> ChangePassword(string username)
        {
            Result<UserModel> result = new();
            try
            {
                string password = _passwordGenerator.GenereatePassword();

                string hashPassword = _passwordHasher.HashPassword(password);
                User UserOpdated = await _userRepository.ChangePassword(username, hashPassword);

                if (UserOpdated is null) {
                    result.IsSuccess = false;
                    result.Message = "Error Changin the user's password";
                    return result;
                }
                result.Data = _mapper.Map<UserModel>(UserOpdated);

                _emailService.SendAsync(new Dtos.EmailRequest
                {
                    EmailTo = result.Data.Email,
                    EmailSubject = "Password change",
                    EmailBody = "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Password Change Notification</title>\r\n    <style>\r\n        body {\r\n            font-family: Arial, sans-serif;\r\n            line-height: 1.6;\r\n            color: #333;\r\n        }\r\n        .container {\r\n            max-width: 600px;\r\n            margin: 0 auto;\r\n            padding: 20px;\r\n            border: 1px solid #ddd;\r\n            border-radius: 5px;\r\n        }\r\n        .footer {\r\n            margin-top: 20px;\r\n            font-size: 12px;\r\n            color: #666;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"container\">\r\n        <h1>Password Change Notification</h1>\r\n        <p>Hello,</p>\r\n        <p>We wanted to inform you that your password was successfully changed. Your new password is "+ password +"</p>\r\n            <p>Best regards,<br>The Team</p>\r\n        <div class=\"footer\">\r\n            <p>If you have any questions or need further assistance, please contact our support team at support@example.com.</p>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>"
                });

                result.Message = "the Password was cahnge succewsfully";
                return result;
            }
            catch
            {
                result.IsSuccess = false;
                result.Message = "Error Changin the user's password";
                return result;
                throw;
            }
        }

        public async Task<Result<UserModel>> GetUserByUserName(string UserName)
        {
            Result<UserModel> result = new();
            try
            {
                if (UserName == null)
                {
                    result.IsSuccess = false;
                    result.Message = "Error getting the user";
                    return result;
                }
                User UserGetted = await _userRepository.GetUserByUserName(UserName);

                if (UserGetted == null)
                {
                    result.IsSuccess = false;
                    result.Message = "Error getting the user";
                    return result;
                }

                result.Data = _mapper.Map<UserModel>(UserGetted);

                if (result.Data == null)
                {
                    result.IsSuccess = false;
                    result.Message = "Error getting the user";
                    return result;
                }

                result.Message = "User get was successfull";
                return result;

            }
            catch
            {
                result.IsSuccess = false;
                result.Message = "Critical error getting the user";
                return result;
                throw;
            }
        }

        public async Task<Result<UserModel>> Login(string userName, string password)
        {
            Result<UserModel> result = new();
            try
            {
                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                {
                    result.IsSuccess = false;
                    result.Message = "username or password is empty";
                    return result;
                }

                User UserGetted = await _userRepository.Login(userName);

                if (UserGetted == null)
                {
                    result.IsSuccess = false;
                    result.Message = "Error getting the user";
                    return result;
                }

                // check that user password is legit
                bool IsTheCorrectPassword = _passwordHasher.VerifyPassword(UserGetted.Password, password);

                result.Data = _mapper.Map<UserModel>(UserGetted);

                if (result.Data == null)
                {
                    result.IsSuccess = false;
                    result.Message = "Error getting the user";
                    return result;
                }

                _httpContextAccessor.HttpContext.Session.Set<UserModel>("user", result.Data);
                result.Message = "Login was a success";
                return result;
            }
            catch
            {
                result.IsSuccess = false;
                result.Message = "Critical error login the user";
                return result;
                throw;
            }
        }

        public override async Task<Result<UserSaveModel>> Save(UserSaveModel saveModel)
        {
            // hash password
            saveModel.Password = _passwordHasher.HashPassword(saveModel.Password);
            Result<UserSaveModel> SaveOperation = await base.Save(saveModel);

            if (SaveOperation.IsSuccess)
            {
                _emailService.SendAsync(new Dtos.EmailRequest
                {
                    EmailTo = SaveOperation.Data.Email,
                    EmailSubject = "Let's activate your user ",

                    EmailBody = new StringBuilder().Append("<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Activate Your Account</title>\r\n    <style>\r\n        body {\r\n            font-family: Arial, sans-serif;\r\n            line-height: 1.6;\r\n            color: #333;\r\n        }\r\n        .container {\r\n            max-width: 600px;\r\n            margin: 0 auto;\r\n            padding: 20px;\r\n            border: 1px solid #ddd;\r\n            border-radius: 5px;\r\n        }\r\n        .button {\r\n            display: inline-block;\r\n            padding: 10px 20px;\r\n            margin-top: 20px;\r\n            font-size: 16px;\r\n            color: #fff;\r\n            background-color: #28a745;\r\n            text-align: center;\r\n            text-decoration: none;\r\n            border-radius: 5px;\r\n        }\r\n        .button:hover {\r\n            background-color: #218838;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"container\">\r\n        <h1>Welcome to Our Service</h1>\r\n        <p>Hello,</p>\r\n        <p>Thank you for registering with us. Please click the button below to activate your account:</p>\r\n        <form action=\"ActivateUser\" method=\"POST\">\r\n            <input type=\"hidden\" name=\"id\" value= "+ SaveOperation.Data.Id+">           <button type=\"submit\" class=\"button\">Activate Account</button>\r\n        </form>\r\n        <p>If you did not register for this account, please ignore this email.</p>\r\n        <p>Best regards,<br>The Team</p>\r\n    </div>\r\n</body>\r\n</html>").ToString()

                });
            }

            return SaveOperation;
        }
    }
}
