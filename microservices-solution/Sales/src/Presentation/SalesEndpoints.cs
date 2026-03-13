using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Sales.Data;
using Sales.Models;
using System.Linq;

namespace Sales.Presentation;

public static class SalesEndpoints
{
    public static void MapSalesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/sales");

        group.MapGet("/", async (ISalesRepository repository) =>
        {
            var sales = await repository.GetAllAsync();
            return Results.Ok(sales);
        });

        group.MapGet("/{id}", async (int id, ISalesRepository repository) =>
        {
            var sale = await repository.GetByIdAsync(id);
            return sale is not null ? Results.Ok(sale) : Results.NotFound();
        });

        group.MapGet("/customer/{customerId}", async (int customerId, ISalesRepository repository) =>
        {
            var sales = await repository.GetByCustomerIdAsync(customerId);
            if (sales == null || !sales.Any())
            {
                return Results.NotFound();
            }
            return Results.Ok(sales);
        });

        group.MapPost("/", async (Sale sale, ISalesRepository repository) =>
        {
            await repository.AddAsync(sale);
            return Results.Created($"/api/sales/{sale.Id}", sale);
        });

        group.MapPut("/", async (Sale updatedSale, ISalesRepository repository) =>
        {
            await repository.UpdateAsync(updatedSale);
            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, ISalesRepository repository) =>
        {
            await repository.DeleteAsync(id);
            return Results.NoContent();
        });
    }
}


