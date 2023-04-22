using System.ComponentModel.DataAnnotations;

namespace QuestionaryApp.Application.Dtos.Request
{
    public class AnswerRequestDto
    {
        [Required(ErrorMessage = "Field text is required")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Field isCorrect is required")]
        public bool isCorrect { get; set; }

        [Required(ErrorMessage = "Field questionId is required")]
        public Guid QuestionId { get; set; }
    }
}