using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Products.src.Data;

namespace Products.Presentation
{
    public static class ProductEndpoints
    {
        public static void MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/products");

            group.MapGet("/", async ([FromServices] IProductRepository productRepository) =>
            {
            });

            group.MapGet("/{id}", async ([FromServices] IProductRepository productRepository, int id) =>
            {
                var product = await productRepository.GetById(id);
                return product is not null ? Results.Ok(product) : Results.NotFound();
            });

            group.MapPost("/", async ([FromServices] IProductRepository productRepository, Product product) =>
            {
            });

            group.MapPut("/{id}", async ([FromServices] IProductRepository productRepository, int id, Product updatedProduct) =>
            {
            });

            group.MapDelete("/{id}", async ([FromServices] IProductRepository productRepository, int id) =>
            {
            });

            group.MapPost("/filters", async ([FromServices] IProductRepository productRepository, [FromBody] IEnumerable<int> ids) =>
            {
                var products = await productRepository.GetProductsByIdAsync(ids);
                return Results.Ok(products);
            });
        }
    }
}