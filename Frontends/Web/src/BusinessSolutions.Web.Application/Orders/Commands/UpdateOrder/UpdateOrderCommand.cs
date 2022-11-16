using AutoMapper;
using BusinessSolutions.Web.Application.Common.Mapping;
using BusinessSolutions.Web.Domain.Models;
using BusinessSolutions.Web.Domain.Models.Api.Order;
using BusinessSolutions.Web.Domain.Services;
using MediatR;

namespace BusinessSolutions.Web.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommand : IRequest<Order>, IMapFrom<EditOrder>
{
    public string? Number { get; set; }
    public int ProviderId { get; set; }
    public DateTimeOffset? Date { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateOrderCommand, EditOrder>();
    }
}

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Order>
{
    private readonly IOrderingService service;
    private readonly IMapper mapper;

    public UpdateOrderCommandHandler(IOrderingService service, IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
    }

    public async Task<Order> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var request = mapper.Map<EditOrder>(command);

        var response = await service.PutOrder(request);

        return response;
    }
}
