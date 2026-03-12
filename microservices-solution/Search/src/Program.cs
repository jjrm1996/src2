using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Search.Presentation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

SearchEndpoints.MapSearchEndpoints(app);

app.Run();