using AutoMapper;
using Domain.DataTransferObjects;
using Domain.Entities;

namespace Domain.CrmMapper
{
    public class CrmMappings : Profile
    {
         public CrmMappings()
        {
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<Client, CreateClientDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, ListProductDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, CreateOrderDto>().ReverseMap();
            CreateMap<Order, CreateOrderDto>().ReverseMap();
        }
        
    }
}
