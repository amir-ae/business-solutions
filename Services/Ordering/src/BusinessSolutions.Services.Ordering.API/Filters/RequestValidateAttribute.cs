using BusinessSolutions.Services.Ordering.Domain.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BusinessSolutions.Services.Ordering.API.Filters
{
    public class RequestValidateAttribute<T> : TypeFilterAttribute
    {
        public RequestValidateAttribute() : base(typeof(RequestValidateFilter<T>))
        {

        }

        public class RequestValidateFilter<R> : IAsyncActionFilter
        {
            private readonly IValidator<R> _validator;

            public RequestValidateFilter(IValidator<R> validator)
            {
                _validator = validator;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (!context.ActionArguments.ContainsKey("request"))
                {
                    context.Result = new BadRequestResult();
                    return;
                }
                if (!(context.ActionArguments["request"] is R request))
                {
                    context.Result = new BadRequestResult();
                    return;
                }

                var result = await _validator.ValidateAsync(request);

                if (!result.IsValid)
                {
                    var errorResponse = new ErrorResponse
                    {
                        Errors = result.Errors.GroupBy(it => it.PropertyName)
                            .ToDictionary(dict => dict.Key, dict => dict.Select(item => item.ErrorMessage).ToList())
                    };

                    context.Result = new BadRequestObjectResult(errorResponse);
                    return;
                }

                await next();
            }
        }
    }
}
