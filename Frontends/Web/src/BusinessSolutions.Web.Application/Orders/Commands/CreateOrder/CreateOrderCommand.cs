using AutoMapper;
using BusinessSolutions.Web.Application.Common.Mapping;
using BusinessSolutions.Web.Domain.Models;
using BusinessSolutions.Web.Domain.Models.Api.Order;
using BusinessSolutions.Web.Domain.Services;
using MediatR;

namespace BusinessSolutions.Web.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<Order>, IMapFrom<AddOrder>
{
    public int ProviderId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateOrderCommand, AddOrder>();
    }
}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
{
    private readonly IOrderingService service;
    private readonly IMapper mapper;

    public CreateOrderCommandHandler(IOrderingService service, IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
    }

    public async Task<Order> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var request = mapper.Map<AddOrder>(command);

        var response = await service.PostOrder(request);

        return response;
    }
}
