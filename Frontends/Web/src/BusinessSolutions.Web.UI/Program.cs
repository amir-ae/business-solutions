using BusinessSolutions.Web.Application;
using BusinessSolutions.Web.Application.Common.Filters;
using BusinessSolutions.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

builder.Services.AddControllers(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>());

builder.Services.AddApplication();
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.Configure<ApiBehaviorOptions>(options =>
    options.SuppressModelStateInvalidFilter = true);

builder.Services.AddOrderingService(new Uri(config["OrderingApiUrl"]));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.Run();
