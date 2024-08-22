using AutoMapper;
using gebeya01.Dto;
using gebeya01.Models;

namespace gebeya01.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Person, PersonDto>();
            CreateMap<CartItem, CartItemsDto>();
        }
    }
}
