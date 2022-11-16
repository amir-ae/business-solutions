using BusinessSolutions.Web.Domain.Services;
using FluentValidation;

namespace BusinessSolutions.Web.Application.Items.Commands.CreateItem;

public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
{
    private readonly IOrderingService service;

    public CreateItemCommandValidator(IOrderingService service)
    {
        this.service = service;

        RuleFor(x => x.OrderId)
                .NotEmpty()
                .MustAsync(OrderExists).WithMessage("Order must exists");
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Quantity).Must(x => x > 0);
        RuleFor(x => x.Unit).NotEmpty();
    }

    private async Task<bool> OrderExists(int orderId, CancellationToken cancellationToken)
    {
        var order = await service.GetOrderById(orderId);

        return order != null;
    }
}
