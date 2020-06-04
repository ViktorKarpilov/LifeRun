using AutoMapper;
using UserManagement.API.Requests;
using UserManagement.API.Responses;
using UserManagement.Domain.Models;

namespace UserManagement.API.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserResponse>();
            CreateMap<RegisterRequest, User>();
            CreateMap<UpdateRequest, User>();
        }
    }
}
