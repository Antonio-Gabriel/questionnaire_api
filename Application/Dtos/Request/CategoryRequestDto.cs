using System.ComponentModel.DataAnnotations;

namespace QuestionaryApp.Application.Dtos.Request
{
    public class CategoryRequestDto
    {
        [Required(ErrorMessage = "Field name is required")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "This field must contain between 3 and 40 characters")]
        public string Name { get; set; }
    }
}