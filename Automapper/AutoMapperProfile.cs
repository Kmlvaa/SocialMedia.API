using AutoMapper;
using SocialMediaAPİ.Common.DTOs.Auth;
using SocialMediaAPİ.DB.Entities;

namespace SocialMediaAPİ.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterDTO, User>();

        }
    }
}
 