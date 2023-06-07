using AutoMapper;
using Business_Logic_Layer.DTO;
using Data_Access_Layer.Models;

namespace Business_Logic_Layer.Helper
{
    public class MappingProfiles : Profile
    {
	//Create DTOs' mappings
        public MappingProfiles()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductDto, Product>();

            CreateMap<Order, OrderDto>();
            CreateMap<OrderCreateDto, Order>();
            CreateMap<OrderDto, Order>();
        }

    }
}
