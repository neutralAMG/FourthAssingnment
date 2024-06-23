
namespace ForthAssignment.Core.Aplication.Dtos
{
	public record EmailRequest
	{
		public string  EmailTo { get; set; }	
		public string  EmailSubject { get; set; }	
		public string  EmailBody { get; set; }	
		public string  EmailFrom { get; set; }	
	}
}
