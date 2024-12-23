using AutoMapper;
using FitnessApp.Domain.Model;
using FitnessApp.WebApi.DTOs.Requests;
using FitnessApp.WebApi.DTOs.Responses;

namespace FitnessApp.WebApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponseDto>().ReverseMap();
            CreateMap<UserRequestDto, User>().ReverseMap();
            CreateMap<UserUpdateRequestDto, User>().ReverseMap();

            CreateMap<Workout, WorkoutResponseDto>().ReverseMap();
            CreateMap<WorkoutRequestDto, Workout>().ReverseMap();
            CreateMap<WorkoutUpdateRequestDto, Workout>().ReverseMap();
            CreateMap<WeeklyProgress, WeeklyProgressResponseDto>().ReverseMap();

            CreateMap<ExerciseType, ExerciseTypeResponseDto>().ReverseMap();
            CreateMap<ExerciseTypeRequestDto, ExerciseType>().ReverseMap();
            CreateMap<ExerciseTypeUpdateRequestDto, ExerciseType>().ReverseMap();
        }
    }
}