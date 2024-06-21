

using ForthAssignment.Core.Aplication.Interfaces.Repositories;
using ForthAssignment.Infraestructure.Persistence.Context;
using ForthAssignment.Infraestructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ForthAssignment.Infraestructure.Persistence
{
	public static class InfraestructureServiceRegistration
	{
		public static void AddInfraestructureLayer(this IServiceCollection services, IConfiguration config)
		{
			string ConnectionString = config.GetConnectionString("Default");
			services.AddDbContext<ForthAssignmentContext>(options => options.UseSqlServer(ConnectionString));
			services.AddTransient<IUserRepository, UserRepository>();
			services.AddTransient<IUserFriendRepository, UserFriendRepository>();
			services.AddTransient<IPostRepository, PostRepository>();
			services.AddTransient<ICommentRepository, CommentRepository>();
		}
	}
}
