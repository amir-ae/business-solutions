using FluentValidation;

namespace BusinessSolutions.Web.Application.Orders.Queries.GetOrder;

public class GetOrderQueryValidator : AbstractValidator<GetOrderQuery>
{
    public GetOrderQueryValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty();
    }
}
