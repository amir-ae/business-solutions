using FluentValidation;

namespace BusinessSolutions.Web.Application.Orders.Queries.GetOrders;

public class GetOrdersByFilterQueryValidator : AbstractValidator<GetOrdersByFilterQuery>
{
    public GetOrdersByFilterQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
