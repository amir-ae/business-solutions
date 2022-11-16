using BusinessSolutions.Services.Ordering.API.Filters;
using BusinessSolutions.Services.Ordering.API.ResponseModels;
using BusinessSolutions.Services.Ordering.Domain.Requests.Order;
using BusinessSolutions.Services.Ordering.Domain.Responses;
using BusinessSolutions.Services.Ordering.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusinessSolutions.Services.Ordering.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [JsonException]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IItemService _itemService;
        private readonly IProviderService _providerService;

        public OrderController(IOrderService orderService,
            IItemService itemService,
            IProviderService providerService)
        {
            _orderService = orderService;
            _itemService = itemService;
            _providerService = providerService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponseModel<OrderResponse>>> Get(
            int pageSize = 10, int pageIndex = 1)
        {
            var result = await _orderService.GetOrdersAsync();
            var totalOrders = result.Count();

            var ordersOnPage = result
                .OrderBy(c => c.Number)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize);

            var model = new PaginatedResponseModel<OrderResponse>(
                pageIndex, pageSize, totalOrders, ordersOnPage);

            return Ok(model);
        }

        [HttpGet("{id:int}")]
        [OrderExists]
        public async Task<ActionResult<OrderResponse>> GetById(int id)
        {
            var result = await _orderService.GetOrderAsync(new GetOrderRequest { Id = id });

            return Ok(result);
        }

        [HttpGet("{number}")]
        [ActionName(nameof(GetByNumber))]
        [OrderExists]
        public async Task<ActionResult<OrderResponse>> GetByNumber(string number)
        {
            var result = await _orderService.GetOrderAsync(new GetOrderRequest { Number = number });

            return Ok(result);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<PaginatedResponseModel<OrderResponse>>> GetByFilter(
            int? providerId, string? providerName, string? orderNumber, string? startTime, string? endTime,
            string? itemName, string? itemUnit, int pageSize = 10, int pageIndex = 1)
        {
            var request = new GetOrdersByFilterRequest
            {
                ProviderId = providerId,
                ProviderName = providerName,
                OrderNumber = orderNumber,
                StartTime = !string.IsNullOrEmpty(startTime) ? DateTimeOffset.Parse(startTime) : null,
                EndTime = !string.IsNullOrEmpty(endTime) ? DateTimeOffset.Parse(endTime) : null,
                ItemName = itemName,
                ItemUnit = itemUnit
            };

            var result = await _orderService.GetOrdersByFilterAsync(request);
            var totalOrders = result.Count();

            var ordersOnPage = result
                .OrderBy(c => c.Number)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize);

            var model = new PaginatedResponseModel<OrderResponse>(
                pageIndex, pageSize, totalOrders, ordersOnPage);

            return Ok(model);
        }

        [HttpPost]
        [RequestValidate<AddOrderRequest>]
        public async Task<CreatedAtActionResult> Post(AddOrderRequest request)
        {
            request.Number = Guid.NewGuid().ToString();
            if (request.Date == null)
            {
                request.Date = DateTimeOffset.UtcNow;
            }
            var result = await _orderService.AddOrderAsync(request);

            return CreatedAtAction(nameof(GetByNumber), new { Number = result.Number }, result);
        }

        [HttpPut("{number}")]
        [OrderExists]
        [RequestValidate<EditOrderRequest>]
        public async Task<ActionResult<OrderResponse>> Put(string number, EditOrderRequest request)
        {
            request.Number = number;
            if (request.Date == null)
            {
                request.Date = DateTimeOffset.UtcNow;
            }
            var result = await _orderService.EditOrderAsync(request);

            return Ok(result);
        }

        [HttpDelete("{number}")]
        [OrderExists]
        public async Task<NoContentResult> Delete(string number)
        {
            var request = new DeleteOrderRequest { Number = number };

            await _orderService.DeleteOrderAsync(request);

            return NoContent();
        }
    }
}
