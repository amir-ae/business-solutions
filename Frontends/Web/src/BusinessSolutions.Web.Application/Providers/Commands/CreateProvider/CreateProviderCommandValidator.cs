using FluentValidation;

namespace BusinessSolutions.Web.Application.Providers.Commands.CreateProvider;

public class CreateProviderCommandValidator : AbstractValidator<CreateProviderCommand>
{
    public CreateProviderCommandValidator()
    {
        RuleFor(x => x.ProviderName)
                .NotEmpty();
                
    }
}
