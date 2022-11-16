using BusinessSolutions.Web.Application.Orders.Commands.DeleteOrder;
using BusinessSolutions.Web.Domain.Services;
using FluentValidation;

namespace BusinessSolutions.Web.Application.Orders.Commands.CreateOrder;

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    private readonly IOrderingService service;

    public DeleteOrderCommandValidator(IOrderingService service)
    {
        this.service = service;

        RuleFor(x => x.Number)
                .NotEmpty()
                .MustAsync(OrderExists).WithMessage("Order must exists");
    }

    private async Task<bool> OrderExists(string? orderNumber, CancellationToken cancellationToken)
    {
        var provider = await service.GetOrderByNumber(orderNumber!);

        return provider != null;
    }
}
