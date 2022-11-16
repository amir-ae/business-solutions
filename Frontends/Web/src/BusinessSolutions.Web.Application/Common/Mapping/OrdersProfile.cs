using AutoMapper;
using BusinessSolutions.Services.Ordering.API.Contract.Requests.Order;
using BusinessSolutions.Web.Application.ViewModels;
using BusinessSolutions.Web.Application.Orders.Commands.UpdateOrder;
using BusinessSolutions.Web.Domain.Models.Api.Order;

namespace BusinessSolutions.Web.Application.Common.Mapping
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<AddOrder, AddOrderRequest>();

            CreateMap<EditOrder, EditOrderRequest>();

            CreateMap<OrderViewModel, UpdateOrderCommand>();
        }
    }
}
