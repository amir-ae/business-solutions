using BusinessSolutions.Services.Ordering.Domain.Requests.Provider;
using BusinessSolutions.Services.Ordering.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BusinessSolutions.Services.Ordering.API.Filters
{
    public class ProviderExistsAttribute : TypeFilterAttribute
    {
        public ProviderExistsAttribute() : base(typeof(ProviderExistsFilter))
        {
        }

        public class ProviderExistsFilter : IAsyncActionFilter
        {
            private readonly IProviderService _providerService;

            public ProviderExistsFilter(IProviderService providerService)
            {
                _providerService = providerService;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (!(context.ActionArguments["id"] is int id))
                {
                    context.Result = new BadRequestResult();
                    return;
                }

                var exists = await _providerService.CheckProviderAsync(new GetProviderRequest { ProviderId = id });

                if (!exists)
                {
                    context.Result = new NotFoundObjectResult(
                        new { Message = $"Provider with id {id} is not present." }
                    );
                    return;
                }

                await next();
            }
        }
    }
}
