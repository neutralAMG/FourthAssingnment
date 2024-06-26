

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ForthAssignment.Core.Aplication.Models.User
{
	public class UserSaveModel
	{
		public Guid Id { get; set; }

		


        [Required(ErrorMessage = "Name is a require fild")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Last Name is a require fild")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "UserName is a require fild")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Phone Number is a require fild")]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is a require fild")]
        public string Email { get; set; }
       
        public string? ProfileImageUrl { get; set; }

        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }

        public bool IsActive { get; set; }

		public IFormFile? File { get; set; }
	}
}
