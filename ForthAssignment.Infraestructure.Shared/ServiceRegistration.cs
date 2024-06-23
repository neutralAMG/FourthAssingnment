

using ForthAssignment.Core.Aplication.Interfaces.Contracts;
using ForthAssignment.Core.Domain.Settings;
using ForthAssignment.Infraestructure.Shared.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ForthAssignment.Infraestructure.Shared
{
	public static class ServiceRegistration
	{
		public static void AddSharedInfraestructureLayer(this IServiceCollection services, IConfiguration config)
		{
			services.Configure<MailSettings>(config.GetSection( "MailSettings"));
			services.AddTransient<IEmailService, EmailService>();
		}
	}
}
