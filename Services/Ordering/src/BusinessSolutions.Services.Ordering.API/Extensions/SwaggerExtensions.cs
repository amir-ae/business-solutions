using Microsoft.OpenApi.Models;

namespace BusinessSolutions.Services.Ordering.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ordering API", Version = "v1" });
                });
            return services;
        }
    }
}
