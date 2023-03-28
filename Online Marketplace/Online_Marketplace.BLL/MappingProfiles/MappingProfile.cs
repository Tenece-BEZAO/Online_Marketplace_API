using AutoMapper;
using Online_Marketplace.DAL.Entities;
using Online_Marketplace.DAL.Entities.Models;
using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.BLL.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, User>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<Product, ProductCreateDto>();

        }
    }
}
