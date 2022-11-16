using BusinessSolutions.Services.Ordering.Domain.Repositories;
using BusinessSolutions.Services.Ordering.Infrastructure;
using BusinessSolutions.Services.Ordering.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace BusinessSolutions.Services.Ordering.API.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddOrderingContext(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<OrderingContext>(contextOptions =>
                    contextOptions.UseSqlServer(configuration.GetConnectionString("OrderingManager"), serverOptions =>
                    {
                        serverOptions.MigrationsAssembly(typeof(Program).Assembly.FullName);
                    }));
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
                .AddScoped<IItemRepository, ItemRepository>()
                .AddScoped<IProviderRepository, ProviderRepository>()
                .AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }

        public static IApplicationBuilder ExecuteMigrations(this IApplicationBuilder app)
        {
            var retry = Policy.Handle<SqlException>()
                .WaitAndRetry(new TimeSpan[]
                {
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(6),
                    TimeSpan.FromSeconds(12)
                });

            retry.Execute(() =>
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<OrderingContext>();
                    if (!context.Database.CanConnect() 
                    || context.Database.GetPendingMigrations().Any())
                    {
                        context.Database.Migrate();
                    }                    
                }
            });
            return app;
        }
    }
}
