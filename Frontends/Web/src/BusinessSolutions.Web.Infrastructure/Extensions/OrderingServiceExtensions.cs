using BusinessSolutions.Services.Ordering.API.Client;
using BusinessSolutions.Web.Domain.Services;
using BusinessSolutions.Web.Infrastructure.Extensions.Policies;
using BusinessSolutions.Web.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessSolutions.Web.Infrastructure.Extensions
{
    public static class OrderingServiceExtensions
    {
        public static IServiceCollection AddOrderingService(this IServiceCollection services, Uri uri)
        {
            services.AddScoped<IOrderingService, OrderingService>();

            services.AddHttpClient<IOrderingClient, OrderingClient>(client => { client.BaseAddress = uri; })
                .SetHandlerLifetime(TimeSpan.FromMinutes(2)) //Set lifetime to 2 minutes
                .AddPolicyHandler(OrderingServicePolicies.RetryPolicy())
                .AddPolicyHandler(OrderingServicePolicies.CircuitBreakerPolicy());

            return services;
        }
    }
}