using BusinessSolutions.Services.Ordering.API.Filters;
using BusinessSolutions.Services.Ordering.API.ResponseModels;
using BusinessSolutions.Services.Ordering.Domain.Requests.Item;
using BusinessSolutions.Services.Ordering.Domain.Requests.Provider;
using BusinessSolutions.Services.Ordering.Domain.Responses;
using BusinessSolutions.Services.Ordering.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusinessSolutions.Services.Ordering.API.Controllers
{
    [Route("api/providers")]
    [ApiController]
    [JsonException]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _providerService;

        public ProviderController(IProviderService providerService)
        {
            _providerService = providerService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponseModel<ProviderResponse>>> Get(
            int pageSize = 10, int pageIndex = 1)
        {
            var result = await _providerService.GetProvidersAsync();
            var totalProviders = result.Count();

            var providersOnPage = result
                .OrderBy(c => c.ProviderName)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize);

            var model = new PaginatedResponseModel<ProviderResponse>(
                pageIndex, pageSize, totalProviders, providersOnPage);

            return Ok(model);
        }

        [HttpGet("{id:int}")]
        [ActionName(nameof(GetById))]
        [ProviderExists]
        public async Task<ActionResult<ProviderResponse>> GetById(int id)
        {
            var result = await _providerService.GetProviderAsync(new GetProviderRequest { ProviderId = id });

            return Ok(result);
        }

        [HttpPost]
        [RequestValidate<AddProviderRequest>]
        public async Task<CreatedAtActionResult> Post(AddProviderRequest request)
        {
            var result = await _providerService.AddProviderAsync(request);

            return CreatedAtAction(nameof(GetById), new { Id = result.ProviderId }, result);
        }

        [HttpDelete("{id:int}")]
        [ProviderExists]
        public async Task<NoContentResult> Delete(int id)
        {
            var request = new DeleteProviderRequest { ProviderId = id };

            await _providerService.DeleteProviderAsync(request);

            return NoContent();
        }
    }
}
