using System.ComponentModel.DataAnnotations;

namespace QuestionaryApp.Application.Dtos.Request
{
    public class AuthRequestDto
    {
        [Required(ErrorMessage = "Field email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field password is required")]
        public string Password { get; set; }
    }
}