using BusinessSolutions.Web.Domain.Services;
using FluentValidation;

namespace BusinessSolutions.Web.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    private readonly IOrderingService service;

    public CreateOrderCommandValidator(IOrderingService service)
    {
        this.service = service;

        RuleFor(x => x.ProviderId)
                .NotEmpty()
                .MustAsync(ProviderExists).WithMessage("Provider must exists");
    }

    private async Task<bool> ProviderExists(int providerId, CancellationToken cancellationToken)
    {
        var provider = await service.GetProviderById(providerId);

        return provider != null;
    }
}
