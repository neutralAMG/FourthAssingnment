

using ForthAssignment.Core.Aplication.Interfaces.Contracts;
using ForthAssignment.Core.Aplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ForthAssignment.Core.Aplication
{
	public static class AplicationServiceRegistration
	{

		public static void AddAplicationLayer(this IServiceCollection services)
		{
			services.AddTransient<IUserService, UserService>();
			services.AddTransient<IUserFriendService, UserFriendService>();
			services.AddTransient<IPostService, PostService>();
			services.AddTransient<ICommentService, CommentService>();

			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

		}
	}


}
