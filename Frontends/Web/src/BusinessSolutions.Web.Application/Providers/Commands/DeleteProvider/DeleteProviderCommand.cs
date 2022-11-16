using BusinessSolutions.Web.Domain.Services;
using MediatR;

namespace BusinessSolutions.Web.Application.Providers.Commands.DeleteProvider;

public class DeleteProviderCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteProviderCommandHandler : IRequestHandler<DeleteProviderCommand>
{
    private readonly IOrderingService service;

    public DeleteProviderCommandHandler(IOrderingService service)
    {
        this.service = service;
    }

    public async Task<Unit> Handle(DeleteProviderCommand request, CancellationToken cancellationToken)
    {
        await service.DeleteProvider(request.Id);

        return Unit.Value;
    }
}
