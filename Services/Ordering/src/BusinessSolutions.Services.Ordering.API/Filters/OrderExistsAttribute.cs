using AutoMapper.Execution;
using BusinessSolutions.Services.Ordering.Domain.Requests.Order;
using BusinessSolutions.Services.Ordering.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BusinessSolutions.Services.Ordering.API.Filters
{
    public class OrderExistsAttribute : TypeFilterAttribute
    {
        public OrderExistsAttribute() : base(typeof(OrderExistsFilter))
        {
        }

        public class OrderExistsFilter : IAsyncActionFilter
        {
            private readonly IOrderService _orderService;

            public OrderExistsFilter(IOrderService orderService)
            {
                _orderService = orderService;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                GetOrderRequest request;

                if (context.ActionArguments.ContainsKey("id"))
                {
                    var id = context.ActionArguments["id"] as int?;
                    request = new GetOrderRequest { Id = id ?? default };
                }
                else if (context.ActionArguments.ContainsKey("number"))
                {
                    var number = context.ActionArguments["number"] as string;
                    request = new GetOrderRequest { Number = number };
                }
                else
                {
                    context.Result = new BadRequestResult();
                    return;
                }

                var exists = await _orderService.CheckOrderAsync(request);

                if (!exists)
                {
                    var id = !string.IsNullOrEmpty(request.Number) ? request.Number : request.Id.ToString();
                    context.Result = new NotFoundObjectResult(
                        new { Message = $"Order with id { id } is not present." }
                    );
                    return;
                }

                await next();
            }
        }
    }
}
