using AutoMapper;
using Dtos;
using Entities;

namespace Services
{

    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}
