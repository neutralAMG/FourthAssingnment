﻿

namespace ForthAssignment.Core.Aplication.Models.User
{
	public class UserSaveModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string? ProfileImageUrl { get; set; }
		public string Password { get; set; }
		public bool IsActive { get; set; }
	}
}
