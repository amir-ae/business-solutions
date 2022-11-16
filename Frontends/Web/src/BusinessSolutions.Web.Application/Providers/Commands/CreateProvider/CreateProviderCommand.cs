using BusinessSolutions.Web.Domain.Models.Api.Provider;
using BusinessSolutions.Web.Domain.Services;
using MediatR;

namespace BusinessSolutions.Web.Application.Providers.Commands.CreateProvider;

public class CreateProviderCommand : IRequest<int>
{
    public string? ProviderName { get; set; }
}

public class CreateProviderCommandHandler : IRequestHandler<CreateProviderCommand, int>
{
    private readonly IOrderingService service;

    public CreateProviderCommandHandler(IOrderingService service)
    {
        this.service = service;
    }

    public async Task<int> Handle(CreateProviderCommand command, CancellationToken cancellationToken)
    {
        var request = new AddProvider();

        request.ProviderName = command.ProviderName;

        var response = await service.PostProvider(request);

        return response.ProviderId;
    }
}
