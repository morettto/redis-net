using Microsoft.EntityFrameworkCore;
using ProductApi.Application.Services.Products;
using ProductApi.Application.Services.Redis;
using ProductApi.Domain.Interfaces.Products;
using ProductApi.Domain.Interfaces.Redis;
using ProductApi.Infrastructure.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IRedisService, RedisService>();

builder.Services.AddStackExchangeRedisCache(o => {
    o.InstanceName = "redisProductDbApi";
    o.Configuration = "localhost:6381";
});

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();

builder.Services.AddDbContext<ProductApiContext>(o => o.UseInMemoryDatabase("ProductApiDb"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
