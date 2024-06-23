

using ForthAssignment.Core.Aplication.Dtos;

namespace ForthAssignment.Core.Aplication.Interfaces.Contracts
{
	public interface IEmailService
	{
		Task SendAsync(EmailRequest emailRequest);
	}
}
