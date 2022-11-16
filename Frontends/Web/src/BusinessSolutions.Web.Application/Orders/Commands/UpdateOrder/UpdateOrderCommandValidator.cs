using BusinessSolutions.Web.Application.Orders.Commands.UpdateOrder;
using BusinessSolutions.Web.Domain.Services;
using FluentValidation;

namespace BusinessSolutions.Web.Application.Orders.Commands.CreateOrder;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    private readonly IOrderingService service;

    public UpdateOrderCommandValidator(IOrderingService service)
    {
        this.service = service;

        RuleFor(x => x.Number)
                .NotEmpty()
                .MustAsync(OrderExists).WithMessage("Order must exists");
        RuleFor(x => x.ProviderId)
                .NotEmpty()
                .MustAsync(ProviderExists).WithMessage("Provider must exists");
    }

    private async Task<bool> OrderExists(string? orderNumber, CancellationToken cancellationToken)
    {
        var order = await service.GetOrderByNumber(orderNumber!);

        return order != null;
    }
    private async Task<bool> ProviderExists(int providerId, CancellationToken cancellationToken)
    {
        var provider = await service.GetProviderById(providerId);

        return provider != null;
    }
}
