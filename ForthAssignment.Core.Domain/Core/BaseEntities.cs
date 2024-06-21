

namespace ForthAssignment.Core.Domain.Core
{
	public class BaseEntity
	{
		public Guid Id = Guid.NewGuid();
		
	}
	public class BaseDateRegisterdEntity : BaseEntity
	{
		public DateTime DateCreated { get; set; }
	}
}
