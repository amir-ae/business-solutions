using BusinessSolutions.Services.Ordering.Domain.Requests.Provider;
using BusinessSolutions.Services.Ordering.Domain.Services;
using FluentValidation;

namespace BusinessSolutions.Services.Ordering.Domain.Requests.Order.Validators
{
    public class EditOrderRequestValidator : AbstractValidator<EditOrderRequest>
    {
        IProviderService _providerService;

        public EditOrderRequestValidator(IProviderService providerService)
        {
            _providerService = providerService;

            RuleFor(x => x.ProviderId)
                .NotEmpty()
                .MustAsync(ProviderExists).WithMessage("Provider must exists");
        }

        private async Task<bool> ProviderExists(int providerId, CancellationToken cancellationToken)
        {
            return await _providerService.CheckProviderAsync(new GetProviderRequest { ProviderId = providerId });
        }
    }
}
