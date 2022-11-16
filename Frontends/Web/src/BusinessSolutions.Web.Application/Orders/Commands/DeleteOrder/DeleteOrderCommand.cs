using BusinessSolutions.Web.Domain.Services;
using MediatR;

namespace BusinessSolutions.Web.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderCommand : IRequest
{
    public string? Number { get; set; }
}

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IOrderingService service;

    public DeleteOrderCommandHandler(IOrderingService service)
    {
        this.service = service;
    }

    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        await service.DeleteOrder(request.Number!);

        return Unit.Value;
    }
}
