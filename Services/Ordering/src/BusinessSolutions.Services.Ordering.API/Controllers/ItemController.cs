using BusinessSolutions.Services.Ordering.API.Filters;
using BusinessSolutions.Services.Ordering.API.ResponseModels;
using BusinessSolutions.Services.Ordering.Domain.Requests.Item;
using BusinessSolutions.Services.Ordering.Domain.Requests.Order;
using BusinessSolutions.Services.Ordering.Domain.Responses;
using BusinessSolutions.Services.Ordering.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusinessSolutions.Services.Ordering.API.Controllers
{
    [Route("api/items")]
    [ApiController]
    [JsonException]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("{id:int}")]
        [ActionName(nameof(GetById))]
        [ItemExists]
        public async Task<ActionResult<OrderResponse>> GetById(int id)
        {
            var result = await _itemService.GetItemAsync(new GetItemRequest { ItemId = id });

            return Ok(result);
        }

        [HttpPost]
        [RequestValidate<AddItemRequest>]
        public async Task<CreatedAtActionResult> Post(AddItemRequest request)
        {
            var result = await _itemService.AddItemAsync(request);

            return CreatedAtAction(nameof(GetById), new { Id = result.ItemId }, result);
        }

        [HttpPut("{id:int}")]
        [ItemExists]
        [RequestValidate<EditItemRequest>]
        public async Task<ActionResult<ItemResponse>> Put(int id, EditItemRequest request)
        {
            request.ItemId = id;
            var result = await _itemService.EditItemAsync(request);

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [ItemExists]
        public async Task<NoContentResult> Delete(int id)
        {
            var request = new DeleteItemRequest { ItemId = id };

            await _itemService.DeleteItemAsync(request);

            return NoContent();
        }
    }
}
