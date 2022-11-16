using AutoMapper;
using BusinessSolutions.Web.Application.Common.Exceptions;
using BusinessSolutions.Web.Application.ViewModels;
using BusinessSolutions.Web.Domain.Models;
using BusinessSolutions.Web.Domain.Services;
using MediatR;
namespace BusinessSolutions.Web.Application.Orders.Queries.GetOrder;

public class GetOrderQuery : IRequest<Order>
{
    public string? Number { get; set; }
}

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Order>
{
    private readonly IOrderingService service;

    public GetOrderQueryHandler(IOrderingService service, IMapper mapper)
    {
        this.service = service;
    }

    public async Task<Order> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await service.GetOrderByNumber(request.Number!);

        if (order == null)
        {
            throw new NotFoundException(nameof(Order), request.Number!);
        }

        return order;
    }
}
