

using AutoMapper;
using ForthAssignment.Core.Aplication.Core;
using ForthAssignment.Core.Aplication.Interfaces.Contracts;
using ForthAssignment.Core.Aplication.Interfaces.Repositories;
using ForthAssignment.Core.Aplication.Models.User;
using ForthAssignment.Core.Aplication.Utils.PasswordHasher;
using ForthAssignment.Core.Aplication.Utils.UserSessionHandler;
using ForthAssignment.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ForthAssignment.Core.Aplication.Services
{
	public class UserService : BaseService<UserSaveModel, UserModel, User>, IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IPasswordHasher _passwordHasher;

		public UserService(IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IPasswordHasher passwordHasher) : base(userRepository, mapper)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_httpContextAccessor = httpContextAccessor;
			_passwordHasher = passwordHasher;
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

		public async Task<Result<UserModel>> GetUserByUserName(string UserName)
		{
			Result<UserModel> result = new();
			try
			{
				if (UserName == null) {
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
			return await base.Save(saveModel);
		}
	}
}
