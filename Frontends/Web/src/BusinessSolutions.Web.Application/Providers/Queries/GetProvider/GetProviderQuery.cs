using BusinessSolutions.Web.Application.Common.Exceptions;
using BusinessSolutions.Web.Domain.Models;
using BusinessSolutions.Web.Domain.Services;
using MediatR;
namespace BusinessSolutions.Web.Application.Providers.Queries.GetProvider;

public class GetProviderQuery : IRequest<Provider>
{
    public int ProviderId { get; set; }
}

public class GetProviderQueryHandler : IRequestHandler<GetProviderQuery, Provider>
{
    private readonly IOrderingService service;

    public GetProviderQueryHandler(IOrderingService service)
    {
        this.service = service;
    }

    public async Task<Provider> Handle(GetProviderQuery request, CancellationToken cancellationToken)
    {
        var provider = await service.GetProviderById(request.ProviderId);

        if (provider == null)
        {
            throw new NotFoundException(nameof(Provider), request.ProviderId);
        }

        return provider;
    }
}
