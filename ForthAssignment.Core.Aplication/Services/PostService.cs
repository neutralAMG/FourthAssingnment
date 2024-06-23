

using AutoMapper;
using ForthAssignment.Core.Aplication.Core;
using ForthAssignment.Core.Aplication.Interfaces.Contracts;
using ForthAssignment.Core.Aplication.Interfaces.Repositories;
using ForthAssignment.Core.Aplication.Models.Post;
using ForthAssignment.Core.Aplication.Models.User;
using ForthAssignment.Core.Aplication.Utils.UserSessionHandler;
using ForthAssignment.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ForthAssignment.Core.Aplication.Services
{
	public class PostService : BaseService<PostSaveModel, PostModel, Post>, IPostService
	{
		private readonly IPostRepository _postRepository;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserModel _CurrentUser;

		public PostService(IPostRepository postRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(postRepository, mapper)
		{
			_postRepository = postRepository;
			_mapper = mapper;
			_httpContextAccessor = httpContextAccessor;
			_CurrentUser = _httpContextAccessor.HttpContext.Session.Get<UserModel>("user");
		}

		public async Task<Result<PostModel>> Delete(Guid id)
		{
			Result<PostModel> result = new();
			try
			{
				Post postToBeDeleted = await _postRepository.GetById(id);
				if (postToBeDeleted is null)
				{
					result.IsSuccess = false;
					result.Message = "Error deleting the post's of the user";
					return result;
				}
				bool DeleteOperationResult = await _postRepository.Delete(postToBeDeleted);

				result.Message = "Succes deleting the post";
				return result;
			}
			catch
			{
				result.IsSuccess = false;
				result.Message = "Critical error deleting the post's of the user";
				return result;
				throw;
			}
		}

		public override async Task<Result<List<PostModel>>> GetAll()
		{
			Result<List<PostModel>> result = new();
			try
			{
			    List<Post> PostsGetted = await _postRepository.GetAll(_CurrentUser.Id);
				
				result.Data = _mapper.Map<List<PostModel>>(PostsGetted);
				if (result.Data is null)
				{
					result.IsSuccess = false;
					result.Message = "Error getting the post's of the user";
					return result;
				}
				result.Message = "Post get was a success";
				return result;
			}
			catch
			{
				result.IsSuccess = false;
				result.Message = "Critical error getting the post's of the user";
				return result;
				throw;
			}
		}

		public async Task<Result<List<PostModel>>> GetAllFrindsPosts()
		{
			Result<List<PostModel>> result = new();
			try
			{
				var friendsPosts = _postRepository.GetAllFrindsPosts(_mapper.Map<List<UserFriend>>(_CurrentUser.UserFriends), _CurrentUser.Id);
				result.Data = _mapper.Map<List<PostModel>>(friendsPosts);
				result.Message = "Friends posts getted successfully";
				return result;
			}
			catch
			{
				result.IsSuccess = false;
				result.Message = "Critical error getting the current user friends posts ";
				return result;
				throw;
			}
		}
	}
}
