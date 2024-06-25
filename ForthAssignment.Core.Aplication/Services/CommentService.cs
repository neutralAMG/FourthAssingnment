using AutoMapper;
using ForthAssignment.Core.Aplication.Core;
using ForthAssignment.Core.Aplication.Interfaces.Contracts;
using ForthAssignment.Core.Aplication.Interfaces.Repositories;
using ForthAssignment.Core.Aplication.Models.Comment;
using ForthAssignment.Core.Aplication.Models.Post;
using ForthAssignment.Core.Aplication.Models.User;
using ForthAssignment.Core.Aplication.Utils.UserSessionHandler;
using ForthAssignment.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ForthAssignment.Core.Aplication.Services
{
	public class CommentService : BaseService<CommentSaveModel, CommentModel, Comment>, ICommentService
	{
		private readonly ICommentRepository _commentRepository;
		private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserModel _CurrentUser;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(commentRepository, mapper)
		{
			_commentRepository = commentRepository;
			_mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
			_CurrentUser = _httpContextAccessor.HttpContext.Session.Get<UserModel>("user");
        }

		public async Task<Result<CommentSaveModel>> RespondComment(CommentSaveModel saveModel)
		{
			Result<CommentSaveModel> result = new();
			try
			{
				if (saveModel is null)
				{
					result.IsSuccess = false;
					result.Message = "Error responding to the coment";
					return result;
				}
                saveModel.UserId = _CurrentUser.Id;

                Comment CommentToSave = _mapper.Map<Comment>(saveModel);

				Comment CommentSaved = await _commentRepository.RespondComment(CommentToSave);

				result.Data = _mapper.Map<CommentSaveModel>(CommentSaved);

				result.Message = "Comment reply was a success";

				return result;

			}
			catch 
			{
				result.IsSuccess = false;
				result.Message = "Critical error responding to the coment";
				return result;
				throw;
			}
		}

        public override async Task<Result<CommentSaveModel>> Save(CommentSaveModel saveModel)
        {
            saveModel.UserId = _CurrentUser.Id;
            return await base.Save(saveModel);
        }
    }
}
