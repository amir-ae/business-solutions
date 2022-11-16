using BusinessSolutions.Services.Ordering.API.Extensions;
using BusinessSolutions.Services.Ordering.Domain.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddOrderingContext(builder.Configuration)
    .AddRepositories()
    .AddMappers()
    .AddServices()
    .AddValidation()
    .AddSwagger()
    .AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("https://localhost:5003");
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
