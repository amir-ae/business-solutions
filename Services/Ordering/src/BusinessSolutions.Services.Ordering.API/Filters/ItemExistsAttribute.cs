using BusinessSolutions.Services.Ordering.Domain.Requests.Item;
using BusinessSolutions.Services.Ordering.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BusinessSolutions.Services.Ordering.API.Filters
{
    public class ItemExistsAttribute : TypeFilterAttribute
    {
        public ItemExistsAttribute() : base(typeof(ItemExistsFilter))
        {
        }

        public class ItemExistsFilter : IAsyncActionFilter
        {
            private readonly IItemService _itemService;

            public ItemExistsFilter(IItemService itemService)
            {
                _itemService = itemService;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (!(context.ActionArguments["id"] is int id))
                {
                    context.Result = new BadRequestResult();
                    return;
                }

                var exists = await _itemService.CheckItemAsync(new GetItemRequest { ItemId = id });

                if (!exists)
                {
                    context.Result = new NotFoundObjectResult(
                        new { Message = $"Item with id {id} is not present." }
                    );
                    return;
                }

                await next();
            }
        }
    }
}
