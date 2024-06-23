
using AutoMapper;
using ForthAssignment.Core.Aplication.Core;
using ForthAssignment.Core.Aplication.Interfaces.Contracts;
using ForthAssignment.Core.Aplication.Interfaces.Repositories;
using ForthAssignment.Core.Aplication.Models.Post;
using ForthAssignment.Core.Aplication.Models.User;
using ForthAssignment.Core.Aplication.Models.UserFriend;
using ForthAssignment.Core.Domain.Entities;
using System.Collections.Generic;

namespace ForthAssignment.Core.Aplication.Services
{
	public class UserFriendService : BaseService<UserFriendSaveModel, UserFriendModel, UserFriend>, IUserFriendService
	{
		private readonly IUserFriendRepository _userFriendRepository;
		private readonly IMapper _mapper;

		public UserFriendService(IUserFriendRepository userFriendRepository, IMapper mapper) : base(userFriendRepository, mapper)
		{
			_userFriendRepository = userFriendRepository;
			_mapper = mapper;
		}

		public async Task<Result<UserFriendModel>> Delete(Guid id)
		{
			Result<UserFriendModel> result = new();
			try
			{
				UserFriend userToToBeDeleted = await _userFriendRepository.GetById(id);
				if (userToToBeDeleted is null)
				{
					result.IsSuccess = false;
					result.Message = "Error deleting the friend of the user";
					return result;
				}
				bool DeleteOperationResult = await _userFriendRepository.Delete(userToToBeDeleted);

				result.Message = "Succes deleting the user's friends";
				return result;
			}
			catch
			{
				result.IsSuccess = false;
				result.Message = "Critical error deleting the friend of the user";
				return result;
				throw;
			}
		}


		public async Task<Result<List<UserModel>>> GetUserFriends(Guid id)
		{
			Result<List<UserModel>> result = new();
			try
			{
				List<User> UsersFriends = await _userFriendRepository.GetUserFriends(id);

				if (UsersFriends is null)
				{
					result.IsSuccess = false;
					result.Message = "Error while getting the user's friend's";
					return result;
				}

				result.Data = _mapper.Map<List<UserModel>>(UsersFriends);

				if (result.Data is null)
				{
					result.IsSuccess = false;
					result.Message = "Error while getting the user's friend's";
					return result;

				}

				result.Message = "User's friend's getted with success";
				return result;
			}
			catch
			{
				result.IsSuccess = false;
				result.Message = "Critcal error while getting the user's friend's";
				return result;
				throw;
			}
		}

		public async Task<Result<List<UserModel>>> GetUserPosibleFriends(Guid id)
		{
			Result<List<UserModel>> result = new();
			try
			{
				List<User> UserFriendRecomendations = await _userFriendRepository.GetUserPosibleFriends(id);

				if (UserFriendRecomendations is null)
				{
					result.IsSuccess = false;
					result.Message = "Error while getting the user's friend's recomendation's";
					return result;
				}

				result.Data = _mapper.Map<List<UserModel>>(UserFriendRecomendations);

				if (result.Data is null)
				{
					result.IsSuccess = false;
					result.Message = "Error while getting the user's friend's recomendation's";
					return result;
				}

				result.Message = "User's Friend recomendation's getted succesfully";
				return result;
			}
			catch
			{
				result.IsSuccess = false;
				result.Message = "Error while getting the user's friend's recomendation's";
				return result;
				throw;
			}
		}

	}
}
