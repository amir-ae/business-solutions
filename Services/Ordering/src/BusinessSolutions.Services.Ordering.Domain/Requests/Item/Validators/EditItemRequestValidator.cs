using BusinessSolutions.Services.Ordering.Domain.Requests.Order;
using FluentValidation;

namespace BusinessSolutions.Services.Ordering.Domain.Requests.Item.Validators
{
    public class EditItemRequestValidator : AbstractValidator<EditItemRequest>
    {
        public EditItemRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Quantity).Must(x => x > 0);
            RuleFor(x => x.Unit).NotEmpty();
        }
    }
}
