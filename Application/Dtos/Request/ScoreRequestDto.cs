using System.ComponentModel.DataAnnotations;

namespace QuestionaryApp.Application.Dtos.Request
{
    public class ScoreRequestDto
    {
        [Required(ErrorMessage = "Field name is required")]
        public int Correct { get; set; }

        [Required(ErrorMessage = "Field wrong is required")]
        public int Wrong { get; set; }

        [Required(ErrorMessage = "Field userid is required")]
        public Guid UserId { get; set; }

    }
}