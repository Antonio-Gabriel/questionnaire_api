using System.ComponentModel.DataAnnotations;

namespace QuestionaryApp.Application.Dtos.Request
{
    public class QuestionnaireRequestDto
    {
        [Required(ErrorMessage = "Field name is required")]        
        public string Title { get; set; }

        [Required(ErrorMessage = "Field categoryid is required")]
        public Guid CategoryId { get; set; }
    }
}