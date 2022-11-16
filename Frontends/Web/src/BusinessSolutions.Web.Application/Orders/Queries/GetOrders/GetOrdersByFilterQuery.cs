using AutoMapper;
using BusinessSolutions.Web.Application.Common.Mapping;
using BusinessSolutions.Web.Application.Common.Models;
using BusinessSolutions.Web.Application.ViewModels;
using BusinessSolutions.Web.Domain.Models;
using BusinessSolutions.Web.Domain.Services;
using MediatR;

namespace BusinessSolutions.Web.Application.Orders.Queries.GetOrders;

public class GetOrdersByFilterQuery : IRequest<PaginatedList<Order>>, IMapFrom<FilterViewModel>
{
    public int? ProviderId { get; set; }
    public string? ProviderName { get; set; }
    public string? OrderNumber { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string? ItemName { get; set; }
    public string? ItemUnit { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<FilterViewModel, GetOrdersByFilterQuery>()
            .ForMember(p => p.StartTime, op => op.MapFrom(v => v.StartDate))
            .ForMember(p => p.EndTime, op => op.MapFrom(v => v.EndDate));
    }
}

public class GetOrdersFilteredByDateQueryHandler : IRequestHandler<GetOrdersByFilterQuery, PaginatedList<Order>>
{
    private readonly IOrderingService service;

    public GetOrdersFilteredByDateQueryHandler(IOrderingService service)
    {
        this.service = service;
    }

    public async Task<PaginatedList<Order>> Handle(GetOrdersByFilterQuery request, CancellationToken cancellationToken)
    {
        var response = await service.GetOrdersByFilter(
            request.ProviderId, request.ProviderName, request.OrderNumber,
            request.StartTime.ToString(), request.EndTime.ToString(),
            request.ItemName, request.ItemUnit,
            request.PageSize, request.PageNumber, cancellationToken);

        return PaginatedList<Order>.Create(response.Data, request.PageNumber ?? response.PageIndex, request.PageSize ?? response.PageSize);
    }
}
