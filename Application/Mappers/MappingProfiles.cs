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
            CreateMap<Category, CategoryResponse>();

            // Mapper by request
            CreateMap<UserRequestDto, User>();
            CreateMap<CategoryRequestDto, Category>();
        }
    }
}