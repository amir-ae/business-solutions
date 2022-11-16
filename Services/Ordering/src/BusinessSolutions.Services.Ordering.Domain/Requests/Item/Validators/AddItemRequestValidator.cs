using BusinessSolutions.Services.Ordering.Domain.Requests.Order;
using BusinessSolutions.Services.Ordering.Domain.Services;
using FluentValidation;

namespace BusinessSolutions.Services.Ordering.Domain.Requests.Item.Validators
{
    public class AddItemRequestValidator : AbstractValidator<AddItemRequest>
    {
        private readonly IOrderService _orderService;

        public AddItemRequestValidator(IOrderService orderService)
        {
            _orderService = orderService;

            RuleFor(x => x.OrderId)
                .NotEmpty()
                .MustAsync(OrderExists).WithMessage("Order must exists");
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Quantity).Must(x => x > 0);
            RuleFor(x => x.Unit).NotEmpty();
        }

        private async Task<bool> OrderExists(int orderId, CancellationToken cancellationToken)
        {
            return await _orderService.CheckOrderAsync(new GetOrderRequest { Id = orderId });
        }
    }
}
