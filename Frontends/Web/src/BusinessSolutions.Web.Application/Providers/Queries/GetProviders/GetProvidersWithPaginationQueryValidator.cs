using FluentValidation;

namespace BusinessSolutions.Web.Application.Providers.Queries.GetProviders;

public class GetProvidersWithPaginationQueryValidator : AbstractValidator<GetProvidersWithPaginationQuery>
{
    public GetProvidersWithPaginationQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
