using BusinessSolutions.Services.Ordering.Domain.Requests.Order;
using BusinessSolutions.Services.Ordering.Domain.Services;
using BusinessSolutions.Services.Ordering.API.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;


namespace BusinessSolutions.Services.Ordering.API.Tests
{
    public class OrderExistsAttributeTests
    {
        [Fact]
        public async Task should_continue_pipeline_when_id_is_present()
        {
            int id = 1;
            var orderService = new Mock<IOrderService>();

            orderService
                .Setup(x => x.CheckOrderAsync(It.IsAny<GetOrderRequest>()))
                .ReturnsAsync(true);

            var filter = new OrderExistsAttribute.OrderExistsFilter(orderService.Object);

            var actionExecutedContext = new ActionExecutingContext(
                new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor()),
                new List<IFilterMetadata>(),
                new Dictionary<string, object?>
                {
                    { "id", id }
                }, new object());

            var nextCallback = new Mock<ActionExecutionDelegate>();

            await filter.OnActionExecutionAsync(actionExecutedContext, nextCallback.Object);

            nextCallback.Verify(executionDelegate => executionDelegate(), Times.Once);
        }
    }
}
