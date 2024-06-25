
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
			return await _context.Comments
				.Include(c => c.UserThatCommentetThis)
				.Include(c => c.ParentComment)
				.Include(c => c.Comments).Include(c => c.Post).ToListAsync();
		}

		public override async Task<Comment> GetById(Guid id)
		{
			return await _context.Comments
				.Include( c => c.UserThatCommentetThis)
				.Include(c => c.ParentComment)
				.Include(c => c.Comments).Include(c => c.Post).Where(c => c.Id == id).FirstOrDefaultAsync();
		}

		public async Task<Comment> RespondComment(Comment entity)
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

		public override async Task<Comment> Save(Comment entity)
		{
			try
			{
                entity.DateCreated = DateTime.Now;
				entity.CommentRespondingTo = default;
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
