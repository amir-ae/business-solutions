using FluentValidation;

namespace BusinessSolutions.Services.Ordering.Domain.Requests.Provider.Validators
{
    public class AddProviderRequestValidator : AbstractValidator<AddProviderRequest>
    {
        public AddProviderRequestValidator()
        {
            RuleFor(x => x.ProviderName).NotEmpty();
        }
    }
}
