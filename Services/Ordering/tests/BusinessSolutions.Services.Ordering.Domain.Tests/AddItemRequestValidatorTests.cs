using Moq;
using BusinessSolutions.Services.Ordering.Domain.Services;
using BusinessSolutions.Services.Ordering.Domain.Requests.Item.Validators;
using BusinessSolutions.Services.Ordering.Domain.Requests.Order;
using BusinessSolutions.Services.Ordering.Domain.Responses;
using BusinessSolutions.Services.Ordering.Domain.Requests.Item;
using BusinessSolutions.Services.Ordering.Domain.Entities;

namespace BusinessSolutions.Services.Iteming.Domain.Tests
{
    public class AddItemRequestValidatorTests
    {
        private readonly Mock<IOrderService> _orderServiceMock;
        private readonly AddItemRequestValidator _validator;
        private readonly int _orderId = int.MaxValue;

        public AddItemRequestValidatorTests()
        {
            _orderServiceMock = new Mock<IOrderService>();
            _orderServiceMock
                .Setup(x => x.GetOrderAsync(It.IsAny<GetOrderRequest>()))
                .ReturnsAsync(() => new OrderResponse());

            _validator = new AddItemRequestValidator(_orderServiceMock.Object);
        }

        [Theory]
        [LoadData("item")]
        public async Task should_have_error_when_OrderId_is_null(Item item)
        {
            var addItemRequest = new AddItemRequest { 
                Name = item.Name, 
                Quantity = item.Quantity, 
                Unit = item.Unit 
            };

            var result = await _validator.ValidateAsync(addItemRequest);

            result.Errors.Select(e => e.PropertyName).ShouldContain("OrderId");
        }

        [Theory]
        [LoadData("item")]
        public async Task should_have_error_when_OrderId_doesnt_exist(Item item)
        {
            _orderServiceMock
                .Setup(x => x.GetOrderAsync(It.IsAny<GetOrderRequest>()))
                .ReturnsAsync(() => null);

            var addItemRequest = new AddItemRequest
            {
                OrderId = _orderId,
                Name = item.Name,
                Quantity = item.Quantity,
                Unit = item.Unit
            };

            var result = await _validator.ValidateAsync(addItemRequest);

            result.Errors.Select(e => e.PropertyName).ShouldContain("OrderId");
        }
    }
}
