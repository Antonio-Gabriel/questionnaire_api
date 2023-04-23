namespace QuestionaryApp.Application.Dtos.Response
{
    public class AuthResponse
    {
        public string token { get; set; }
        public UserResponse User { get; set; }
    }
}