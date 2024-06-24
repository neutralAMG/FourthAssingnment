
using ForthAssignment.Core.Aplication.Interfaces.Repositories;
using ForthAssignment.Core.Domain.Entities;
using ForthAssignment.Infraestructure.Persistence.Context;
using ForthAssignment.Infraestructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace ForthAssignment.Infraestructure.Persistence.Repository
{
	public class CommentRepository : BaseRepository<Comment>, ICommentRepository
	{
		private readonly ForthAssignmentContext _context;

		public CommentRepository(ForthAssignmentContext context) : base(context)
		{
			_context = context;
		}
		public override async Task<List<Comment>> GetAll()
		{
			return await _context.Comments.ToListAsync();
		}

		public override async Task<Comment> GetById(Guid id)
		{
			return await base.GetById(id);
		}

		public async Task<Comment> RespondComment(Comment entity, Guid CommentBeingRRespondedToId)
		{
			try
			{
				entity.CommentRespondingTo = CommentBeingRRespondedToId;
				await base.Save(entity);

				return entity;
			}
			catch
			{
				throw;
			}
		}

		public override async Task<Comment> Save(Comment entity)
		{
			try
			{
                entity.DateCreated = DateTime.Now;
                await base.Save(entity);

				return entity;
			}
			catch
			{
				throw;
			}
		}

		public override async Task<bool> Update(Comment entity)
		{
			try
			{
				Comment CommentToBeUpdated = await GetById(entity.Id);
				CommentToBeUpdated.CommentImgUrl = entity.CommentImgUrl;
				CommentToBeUpdated.CommentText = entity.CommentText;

				return await base.Update(CommentToBeUpdated);
			}
			catch
			{
				return false;
			}


		}
	}
}
