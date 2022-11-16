using BusinessSolutions.Web.Application.Common.Models;
using BusinessSolutions.Web.Domain.Models;
using BusinessSolutions.Web.Domain.Services;
using MediatR;

namespace BusinessSolutions.Web.Application.Providers.Queries.GetProviders;

public class GetProvidersWithPaginationQuery : IRequest<PaginatedList<Provider>>
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}

public class GetProvidersWithPaginationQueryHandler : IRequestHandler<GetProvidersWithPaginationQuery, PaginatedList<Provider>>
{
    private readonly IOrderingService service;

    public GetProvidersWithPaginationQueryHandler(IOrderingService service)
    {
        this.service = service;
    }

    public async Task<PaginatedList<Provider>> Handle(GetProvidersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var response = await service.GetProviders(request.PageSize, request.PageNumber, cancellationToken);

        return PaginatedList<Provider>.Create(response.Data, request.PageNumber ?? response.PageIndex, request.PageSize ?? response.PageSize);
    }
}
