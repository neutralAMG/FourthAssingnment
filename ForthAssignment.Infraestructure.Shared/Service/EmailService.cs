
using ForthAssignment.Core.Aplication.Dtos;
using ForthAssignment.Core.Aplication.Interfaces.Contracts;
using ForthAssignment.Core.Domain.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ForthAssignment.Infraestructure.Shared.Service
{
	public class EmailService : IEmailService
	{
		public MailSettings  _mailSettings { get; set; }

		public EmailService(IOptions<MailSettings> mailSettings)
		{
			_mailSettings = mailSettings.Value;
		}
		public async Task SendAsync(EmailRequest emailRequest)
		{
			try
			{
				MimeMessage email = new();
				email.Sender = MailboxAddress.Parse($" {_mailSettings.DisplayName}  < {_mailSettings.EmailFrom} >");
				email.From.Add( MailboxAddress.Parse(_mailSettings.EmailFrom));
				email.To.Add(MailboxAddress.Parse(emailRequest.EmailTo));
				email.Subject = emailRequest.EmailSubject;

				BodyBuilder builder = new();

				builder.HtmlBody = emailRequest.EmailBody;

				email.Body = builder.ToMessageBody();

				using(SmtpClient smtp = new())
				{
					smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
					smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
					await smtp.SendAsync(email);
					smtp.Disconnect(true);
				}
			}
			catch
			{
				throw;
			}
		}
	}
}
