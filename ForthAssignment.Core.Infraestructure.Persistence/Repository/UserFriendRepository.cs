

using ForthAssignment.Core.Aplication.Interfaces.Repositories;
using ForthAssignment.Core.Domain.Entities;
using ForthAssignment.Infraestructure.Persistence.Context;
using ForthAssignment.Infraestructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace ForthAssignment.Infraestructure.Persistence.Repository
{
	public class UserFriendRepository : BaseRepository<UserFriend>, IUserFriendRepository
	{
		private readonly ForthAssignmentContext _context;

		public UserFriendRepository(ForthAssignmentContext context) : base(context)
		{
			_context = context;
		}
		public override async Task<List<UserFriend>> GetAll()
		{
			return await _context.UsersFriends.ToListAsync();
		}

		public override async Task<UserFriend> GetById(Guid id)
		{
			return await base.GetById(id);
		}

		public override async Task<UserFriend> Save(UserFriend entity)
		{
			try
			{
				await base.Save(entity);

				return entity;
			}
			catch
			{
				throw;
			}
		}

		public override async Task<bool> Update(UserFriend entity)
		{
			try
			{
				return await base.Update(entity);
			}
			catch
			{
				return false;
			}


		}
		public async Task<bool> Delete(UserFriend entity)
		{
			try
			{
				_context.UsersFriends.Remove(entity);
			 await 	_context.SaveChangesAsync();
				return true;
			}
			catch
			{
				return false;
				throw;
			}
		}

		public Task<List<User>> GetUserFriends(Guid id)
		{
			try
			{
				return _context.UsersFriends.Include(u => u.UsersFriend).Where(u => u.UserId == id).Select(u => u.UsersFriend).ToListAsync();
			}
			catch
			{
				throw;
			}
		}

		public Task<List<User>> GetUserPosibleFriends(Guid id)
		{
			try
			{
				return _context.UsersFriends.Include(u => u.User).ThenInclude(u => u.FriendsOfthUser).Select(u => u.UsersFriend).ToListAsync();
			}
			catch
			{
				throw;
			}
		}
	}
}
