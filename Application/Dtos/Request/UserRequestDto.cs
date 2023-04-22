using System.ComponentModel.DataAnnotations;

namespace QuestionaryApp.Application.Dtos.Request
{
    public class UserRequestDto
    {
        [Required(ErrorMessage = "Field name is required")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "This field must contain between 3 and 40 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field codeName is required")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "This field must contain between 3 and 20 characters")]
        public string CodeName { get; set; }

        [Required(ErrorMessage = "Field password is required")]
        public string Password { get; set; }
    }
}