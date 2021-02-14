using AutoMapper;
using CleanArchCRUD.Domain.DTOs;
using CleanArchCRUD.Domain.Entities;

namespace CleanArchCRUD.Infrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
