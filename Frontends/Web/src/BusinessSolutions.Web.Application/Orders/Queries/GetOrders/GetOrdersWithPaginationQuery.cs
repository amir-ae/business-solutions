using BusinessSolutions.Web.Application.Common.Models;
using BusinessSolutions.Web.Domain.Models;
using BusinessSolutions.Web.Domain.Services;
using MediatR;

namespace BusinessSolutions.Web.Application.Orders.Queries.GetOrders;

public class GetOrdersWithPaginationQuery : IRequest<PaginatedList<Order>>
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}

public class GetOrdersWithPaginationQueryHandler : IRequestHandler<GetOrdersWithPaginationQuery, PaginatedList<Order>>
{
    private readonly IOrderingService service;

    public GetOrdersWithPaginationQueryHandler(IOrderingService service)
    {
        this.service = service;
    }

    public async Task<PaginatedList<Order>> Handle(GetOrdersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var response = await service.GetOrders(request.PageSize, request.PageNumber, cancellationToken);

        return PaginatedList<Order>.Create(response.Data, request.PageNumber ?? response.PageIndex, request.PageSize ?? response.PageSize);
    }
}
