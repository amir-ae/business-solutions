using AutoMapper;
using BusinessSolutions.Services.Ordering.Domain.Entities;
using BusinessSolutions.Services.Ordering.Domain.Requests.Item;
using BusinessSolutions.Services.Ordering.Domain.Requests.Order;
using BusinessSolutions.Services.Ordering.Domain.Requests.Provider;
using BusinessSolutions.Services.Ordering.Domain.Responses;

namespace BusinessSolutions.Services.Ordering.Domain.Mappers
{
    public class OrderingProfile : Profile
    {
        public OrderingProfile()
        {
            CreateMap<AddItemRequest, Item>().ReverseMap();
            CreateMap<AddOrderRequest, Order>().ReverseMap();
            CreateMap<AddProviderRequest, Provider>().ReverseMap();

            CreateMap<EditItemRequest, Item>().ReverseMap();
            CreateMap<EditOrderRequest, Order>().ReverseMap();

            CreateMap<Item, ItemResponse>().ReverseMap();
            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<Provider, ProviderResponse>().ReverseMap();
        }
    }
}
