using BusinessSolutions.Web.Application.Providers.Commands.DeleteProvider;
using FluentValidation;

namespace BusinessSolutions.Web.Application.Providers.Commands.CreateProvider;

public class DeleteProviderCommandValidator : AbstractValidator<DeleteProviderCommand>
{
    public DeleteProviderCommandValidator()
    {
        RuleFor(x => x.Id)
                .NotEmpty();
    }
}
