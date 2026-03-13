using Customers.src.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Customers.Presentation;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/customers");

        group.MapGet("/", async ([FromServices] ICustomerRepository customerRepository) =>
        {
            var customers = await customerRepository.GetAllAsync();
            return Results.Ok(customers);
        });

        group.MapGet("/{id}", async ([FromServices] ICustomerRepository customerRepository, int id) =>
        {
            var customer = await customerRepository.GetByIdAsync(id);
            return customer is not null ? Results.Ok(customer) : Results.NotFound();
        });

        group.MapPost("/", async ([FromServices] ICustomerRepository customerRepository, Customer customer) =>
        {
            await customerRepository.AddAsync(customer);
            return Results.Created($"{customer.Id}", customer);
        });

        group.MapPut("/{id}", async ([FromServices] ICustomerRepository customerRepository, int id, Customer updatedCustomer) =>
        {
            var existingCustomer = await customerRepository.GetByIdAsync(id);
            if (existingCustomer is null) return Results.NotFound();

            await customerRepository.UpdateAsync(updatedCustomer);
            return Results.NoContent();
        });

        group.MapDelete("/{id}", async ([FromServices] ICustomerRepository customerRepository, int id) =>
        {
            var customer = await customerRepository.GetByIdAsync(id);
            if (customer is null) return Results.NotFound();

            await customerRepository.DeleteAsync(id);
            return Results.NoContent();
        });
    }
}
