

using ForthAssignment.Core.Aplication.Interfaces.Contracts;
using ForthAssignment.Core.Aplication.Services;
using ForthAssignment.Core.Aplication.Utils.FileHandler;
using ForthAssignment.Core.Aplication.Utils.PasswordHasher;
using ForthAssignment.Core.Aplication.Utils.UserAuth;
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
			services.AddSingleton<IPasswordHasher, PasswordHasher>();
			services.AddSingleton<IUserAuth,  UserAuth>();	
			services.AddSingleton<IFileHandler, FileHandler>();

		}
	}


}
