using BusinessSolutions.Web.Domain.Services;
using MediatR;

namespace BusinessSolutions.Web.Application.Items.Commands.DeleteItem;

public class DeleteItemCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand>
{
    private readonly IOrderingService service;

    public DeleteItemCommandHandler(IOrderingService service)
    {
        this.service = service;
    }

    public async Task<Unit> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        await service.DeleteItem(request.Id);

        return Unit.Value;
    }
}
