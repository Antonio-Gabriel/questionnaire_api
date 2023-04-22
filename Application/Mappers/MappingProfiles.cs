using AutoMapper;
using QuestionaryApp.Application.Dtos.Request;
using QuestionaryApp.Application.Dtos.Response;

namespace QuestionaryApp.Application.Mappers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserResponse>();
            CreateMap<Score, ScoreResponse>();
            CreateMap<Answer, AnswerResponse>();
            CreateMap<Category, CategoryResponse>();
            CreateMap<Question, QuestionResponse>();
            CreateMap<Questionnaire, QuestionnaireResponse>();

            // Mapper by request
            CreateMap<UserRequestDto, User>();
            CreateMap<ScoreRequestDto, Score>();
            CreateMap<AnswerRequestDto, Answer>();
            CreateMap<CategoryRequestDto, Category>();
            CreateMap<QuestionRequestDto, Question>();
            CreateMap<QuestionnaireRequestDto, Questionnaire>();
        }
    }
}