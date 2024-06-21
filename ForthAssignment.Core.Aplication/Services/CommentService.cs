using AutoMapper;
using ForthAssignment.Core.Aplication.Core;
using ForthAssignment.Core.Aplication.Interfaces.Contracts;
using ForthAssignment.Core.Aplication.Interfaces.Repositories;
using ForthAssignment.Core.Aplication.Models.Comment;
using ForthAssignment.Core.Domain.Entities;

namespace ForthAssignment.Core.Aplication.Services
{
	public class CommentService : BaseService<CommentSaveModel, CommentModel, Comment>, ICommentService
	{
		private readonly ICommentRepository _commentRepository;
		private readonly IMapper _mapper;

		public CommentService(ICommentRepository commentRepository, IMapper mapper) : base(commentRepository, mapper)
		{
			_commentRepository = commentRepository;
			_mapper = mapper;
		}

		public async Task<Result<CommentSaveModel>> RespondComment(CommentSaveModel comment, Guid CommentBeingRRespondedToId)
		{
			Result<CommentSaveModel> result = new();
			try
			{
				if (comment is null)
				{
					result.IsSuccess = false;
					result.Message = "Error responding to the coment";
					return result;
				}
				Comment CommentToSave = _mapper.Map<Comment>(comment);

				Comment CommentSaed = await _commentRepository.RespondComment(CommentToSave, CommentBeingRRespondedToId);

				result.Data = _mapper.Map<CommentSaveModel>(CommentSaed);

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
	}
}
