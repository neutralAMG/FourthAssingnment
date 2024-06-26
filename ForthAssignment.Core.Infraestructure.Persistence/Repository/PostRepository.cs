
using ForthAssignment.Core.Aplication.Interfaces.Repositories;
using ForthAssignment.Core.Domain.Entities;
using ForthAssignment.Infraestructure.Persistence.Context;
using ForthAssignment.Infraestructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace ForthAssignment.Infraestructure.Persistence.Repository
{
	public class PostRepository : BaseRepository<Post>, IPostRepository
	{
		private readonly ForthAssignmentContext _context;

		public PostRepository(ForthAssignmentContext context) : base(context)
		{
			_context = context;
		}

		public override async Task<List<Post>> GetAll()
		{
			return await _context.Posts.ToListAsync();
		}
		public async Task<List<Post>> GetAll(Guid id)
		{
			return await _context.Posts.Include(p => p.Comments)
                    .ThenInclude(c => c.Comments)
                        .ThenInclude(c => c.Comments)
                            .ThenInclude(c => c.Comments)
                                .ThenInclude(u => u.UserThatCommentetThis)
                .Include(p => p.Comments)
                    .ThenInclude(u => u.UserThatCommentetThis)
                .Include(p => p.UserThatPostThis).OrderByDescending( p => p.DateCreated).Where( p => p.UserThatPostThis.Id == id).ToListAsync();	
		}

		public async Task<List<Post>> GetAllFrindsPosts(List<User> UsersFriends, Guid userId)
		{
            var friendIds = UsersFriends.Select(user => user.Id).ToList();

          
            var postOfFriends = await _context.Posts
                .Include(p => p.Comments)
                    .ThenInclude(c => c.Comments)
                        .ThenInclude(c => c.Comments)
                            .ThenInclude(c => c.Comments)
                                .ThenInclude(u => u.UserThatCommentetThis)
                .Include(p => p.Comments)
                    .ThenInclude(u => u.UserThatCommentetThis)
                .Include(p => p.UserThatPostThis)
                .Where(u => friendIds.Contains(u.UserId))
                .OrderByDescending(p => p.DateCreated)
                .ToListAsync();

            return postOfFriends;
        }

		public override async Task<Post> GetById(Guid id)
		{
			return await _context.Posts.Include(p => p.Comments)
                    .ThenInclude(c => c.Comments)
                        .ThenInclude(c => c.Comments)
                            .ThenInclude(c => c.Comments)
                                .ThenInclude(u => u.UserThatCommentetThis)
                .Include(p => p.Comments)
                    .ThenInclude(u => u.UserThatCommentetThis)
                .Include(p => p.UserThatPostThis)
                .Where(p => p.Id == id).FirstOrDefaultAsync();
		}

		public override async Task<Post> Save(Post entity)
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

		public override async Task<bool> Update(Post entity)
		{
			try
			{
				Post PostToBeUpdated = await GetById(entity.Id);
				PostToBeUpdated.PostText = entity.PostText;
				PostToBeUpdated.PostImgUrl = entity.PostImgUrl;
				PostToBeUpdated.VideoUrl = entity.VideoUrl;

				return await base.Update(PostToBeUpdated);
			}
			catch
			{
				return false;
			}


		}

		public async Task<bool> Delete(Post entity)
		{
			try
			{
				Post PostToBeDeleted = await GetById(entity.Id);
				 _context.Posts.Remove(entity);

				IQueryable<Comment> CommentToDelete = _context.Comments.Where(c => c.PostId == entity.Id);
				_context.Comments.RemoveRange(CommentToDelete);
				
				
				await _context.SaveChangesAsync();	
				return true;
			}
			catch
			{
				return false;
				throw;
			}
		}


	}
}
