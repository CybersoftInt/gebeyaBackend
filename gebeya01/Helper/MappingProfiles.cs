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
            CreateMap<ProductDto, Product>(); // Ensure two-way mapping if needed

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>(); // Ensure two-way mapping if needed

            CreateMap<Person, PersonDto>();
            CreateMap<PersonDto, Person>(); // Ensure two-way mapping if needed

            CreateMap<CartItem, CartItemsDto>();
            CreateMap<CartItemsDto, CartItem>();

            CreateMap<WishlistDto, Wishlist>();
            CreateMap<WishlistItemDto, WishlistItem>();

            CreateMap<Wishlist, WishlistDto>();
            CreateMap< WishlistItem, WishlistItemDto > ();


        }
    }
}
