using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Sales.Data;
using Sales.Models;
using System.Threading.Tasks;

namespace Sales.Presentation
{
    public static class SalesEndpoints
    {
        public static void MapSalesEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/sales", async (ISalesRepository repository) =>
            {
                return await Task.FromResult(repository.GetAll());
            });

            app.MapGet("/sales/{id}", async (int id, ISalesRepository repository) =>
            {
                var sale = repository.GetById(id);
                return sale is not null ? Results.Ok(sale) : Results.NotFound();
            });

            app.MapPost("/sales", async (Sale sale, ISalesRepository repository) =>
            {
                repository.Add(sale);
                return Results.Created($"/sales/{sale.Id}", sale);
            });

            app.MapPut("/sales", async (Sale updatedSale, ISalesRepository repository) =>
            {
                repository.Update(updatedSale);
                return Results.NoContent();
            });

            app.MapDelete("/sales/{id}", async (int id, ISalesRepository repository) =>
            {
                repository.Delete(id);
                return Results.NoContent();
            });
        }
    }
}

