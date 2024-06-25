
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
			return await _context.Posts.Include(p => p.Comments.Where(c => c.CommentRespondingTo == null))
                    .ThenInclude(c => c.Comments).ThenInclude(c => c.Comments)
                    .Include(p => p.Comments.Where(c => c.CommentRespondingTo == null)).ThenInclude(u => u.UserThatCommentetThis)
                    .Include(p => p.UserThatPostThis).Where(p => p.UserId == id).OrderByDescending( p => p.DateCreated).ToListAsync();	
		}

		public async Task<List<Post>> GetAllFrindsPosts(List<User> UsersFriends, Guid userId)
		{
			List<Post> postOfFriends = new();
			//O(n + n**2)

			foreach (User user in UsersFriends)
			{
				var currentFriendPosts = 
					await _context.Posts
					.Include(p => p.Comments.Where(c => c.CommentRespondingTo == null))
					.ThenInclude(c => c.Comments).ThenInclude(c => c.Comments)
					.Include(p => p.Comments.Where(c => c.CommentRespondingTo == null)).ThenInclude(u => u.UserThatCommentetThis)
					.Include(p => p.UserThatPostThis)
					.Where(u => u.UserId == user.Id).ToListAsync();
				foreach (Post post in currentFriendPosts)
				{
					postOfFriends.Add(post);
				}
			}

			return postOfFriends.OrderByDescending(p => p.DateCreated.Date).ToList();
		}

		public override async Task<Post> GetById(Guid id)
		{
			return await _context.Posts.Include(p => p.Comments.Where(c => c.CommentRespondingTo == null))
                    .ThenInclude(c => c.Comments).ThenInclude(c => c.Comments)
                    .Include(p => p.Comments.Where(c => c.CommentRespondingTo == null)).ThenInclude(u => u.UserThatCommentetThis)
                    .Include(p => p.UserThatPostThis).Where(p => p.Id == id).FirstOrDefaultAsync();
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
