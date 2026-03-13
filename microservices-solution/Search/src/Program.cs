using System;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;
using Search.Presentation;
using Search.Services.Customers;
using Search.Services.Products;
using Search.Services.Sales;
using Search.src.Services.Customers;

var builder = WebApplication.CreateBuilder(args);

var appSettingsPath = System.IO.Path.Combine(builder.Environment.ContentRootPath, "src", "appsettings.json");
builder.Configuration
    .AddJsonFile(appSettingsPath, optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddHttpClient();

builder.Services.AddHttpClient("customerApi", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Services:Customers"]); 
});

builder.Services.AddHttpClient("productApi", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Services:Products"]); 
});

builder.Services.AddHttpClient("salesApi", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Services:Sales"]); 
});

builder.Services.AddScoped<IServiceCustomer, ServiceCustomer>();
builder.Services.AddScoped<IServiceProduct, ServiceProduct>();
builder.Services.AddScoped<IServiceSale, ServiceSale>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.MapOpenApi();
app.MapScalarApiReference();

SearchEndpoints.MapSearchEndpoints(app);

app.Run();