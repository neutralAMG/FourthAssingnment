
using ForthAssignment.Core.Aplication.Interfaces.Repositories;
using ForthAssignment.Core.Domain.Entities;
using ForthAssignment.Infraestructure.Persistence.Context;
using ForthAssignment.Infraestructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace ForthAssignment.Infraestructure.Persistence.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ForthAssignmentContext _context;

        public UserRepository(ForthAssignmentContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public override async Task<User> GetById(Guid id)
        {
            return await _context.Users.Include(u => u.UserFriends).ThenInclude(u => u.UsersFriend).ThenInclude(c => c.UserPosts)
                    .Include(u => u.FriendsOfthUser).ThenInclude(u => u.UsersFriend).ThenInclude(c => c.UserPosts)
                .Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public override async Task<User> Save(User entity)
        {

            try
            {
                if (await Exits(u => u.UserName == entity.UserName)) return null;

                await base.Save(entity);

                return entity;
            }
            catch
            {
                throw;
            }
        }

        public override async Task<bool> Update(User entity)
        {
            try
            {
                User UserToBeUpdated = _context.Users.Find(entity.Id);
                UserToBeUpdated.Name = entity.Name;
                UserToBeUpdated.LastName = entity.LastName;
                UserToBeUpdated.Email = entity.Email;
                UserToBeUpdated.Password = entity.Password;
                UserToBeUpdated.PhoneNumber = entity.PhoneNumber;
                UserToBeUpdated.ProfileImageUrl = entity.ProfileImageUrl;

                return await base.Update(UserToBeUpdated);
            }
            catch
            {
                return false;
            }


        }
        public async Task<User> Login(string username)
        {
            try
            {

                return await _context.Users.Include(u => u.UserFriends).ThenInclude(u => u.UsersFriend).ThenInclude(c => c.UserPosts)
                    .Include(u => u.FriendsOfthUser).ThenInclude(u => u.UsersFriend).ThenInclude(c => c.UserPosts)
					.Where(u => u.UserName.Equals(username)).FirstOrDefaultAsync();
            }
            catch
            {

                throw;
            }
        }

        public async Task ActivateUser(Guid id)
        {
            try
            {
                User UserToBeUpdated = await GetById(id);
                UserToBeUpdated.IsActive = true;

                await base.Update(UserToBeUpdated);
            }
            catch
            {

            }
        }

        public async Task<User> GetUserByUserName(string UserName)
        {
            try
            {
                return await _context.Users.Include(u => u.UserPosts)
                .Include(u => u.UserFriends).Where(u => u.UserName.Equals(UserName)).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<User> ChangePassword(string username, string password)
        {
            try
            {
                if (!await Exits(u => u.UserName == username)) return null; 

                User UserToChangePassword = await GetUserByUserName(username);

                UserToChangePassword.Password = password;

                await base.Update(UserToChangePassword);

                return UserToChangePassword;
            }
            catch
            {
                throw;
            }
        }
    }
}
