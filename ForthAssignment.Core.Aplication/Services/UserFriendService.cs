
using AutoMapper;
using ForthAssignment.Core.Aplication.Core;
using ForthAssignment.Core.Aplication.Interfaces.Contracts;
using ForthAssignment.Core.Aplication.Interfaces.Repositories;
using ForthAssignment.Core.Aplication.Models.Post;
using ForthAssignment.Core.Aplication.Models.User;
using ForthAssignment.Core.Aplication.Models.UserFriend;
using ForthAssignment.Core.Aplication.Utils.UserSessionHandler;
using ForthAssignment.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ForthAssignment.Core.Aplication.Services
{
    public class UserFriendService : BaseService<UserFriendSaveModel, UserFriendModel, UserFriend>, IUserFriendService
    {
        private readonly IUserFriendRepository _userFriendRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserModel _currentUser;

        public UserFriendService(IUserFriendRepository userFriendRepository, IMapper mapper, IHttpContextAccessor httpContext) : base(userFriendRepository, mapper)
        {
            _userFriendRepository = userFriendRepository;
            _mapper = mapper;
            _httpContext = httpContext;
            _currentUser = _httpContext.HttpContext.Session.Get<UserModel>("user");
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
        public override async Task<Result<UserFriendSaveModel>> Save(UserFriendSaveModel saveModel)
        {
            Result<UserFriendSaveModel> result = new();
            saveModel.UserId = _currentUser.Id;
            if (await _userFriendRepository.Exits(u => u.UserFriendId == saveModel.UserFriendId && u.UserId == saveModel.UserId))
            {
                result.IsSuccess = false;
                result.Message = "Your are already friend's with this user";
                return result;
            }
            result = await base.Save(saveModel);

            return result;
        }
    }


}
