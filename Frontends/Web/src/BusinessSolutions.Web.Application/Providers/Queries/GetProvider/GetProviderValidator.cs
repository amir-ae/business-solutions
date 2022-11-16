using FluentValidation;

namespace BusinessSolutions.Web.Application.Providers.Queries.GetProvider;

public class GetProviderQueryValidator : AbstractValidator<GetProviderQuery>
{
    public GetProviderQueryValidator()
    {
        RuleFor(x => x.ProviderId)
            .NotEmpty();
    }
}
