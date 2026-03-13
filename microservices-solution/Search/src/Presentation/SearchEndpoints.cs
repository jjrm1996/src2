using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Search.Services.Customers;
using Search.Services.Products;
using Search.Services.Sales;


namespace Search.Presentation
{
    public static class SearchEndpoints
    {
        public static void MapSearchEndpoints(this IEndpointRouteBuilder app)
        {
            #region Customers
            app.MapGet("/customers", async ([FromServices] IServiceCustomer customer) =>
            {
                var result = await customer.GetAllAsync();
                if (result == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(result);
            });

            app.MapGet("/customers/{id}", async ([FromServices] IServiceCustomer customer,
             [FromRoute] int id) =>
            {   
                var result = await customer.GetCustomerById(id);
                if (result == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(result);
            });
            #endregion

            #region Products
            app.MapGet("/products", async ([FromServices] IServiceProduct product) =>
            {
                var result = await product.GetAllAsync();
                if (result == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(result);
            });

            app.MapGet("/products/{id}", async ([FromServices] IServiceProduct product, [FromRoute] int id) =>
            {
                var result = await product.GetProductByIdAsync(id);
                if (result == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(result);
            });
            #endregion

            #region Sales
            app.MapGet("/sales", async ([FromServices] IServiceSale sale) =>
            {
                var result = await sale.GetAllAsync();
                if (result == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(result);
            });

            app.MapGet("/sales/customer/{customerId}", async (
                [FromServices] IServiceSale sale,
                [FromServices] IServiceCustomer customer,
                [FromServices] IServiceProduct product,
                [FromRoute] int customerId) =>
            {
                var customerInfo = await customer.GetCustomerById(customerId);
                if (customerInfo == null)
                {
                    return Results.NotFound("Cliente no encontrado");
                }

                var sales = await sale.GetByCustomerIdAsync(customerId);
                if (sales == null)
                {
                    return Results.NotFound("No hay ventas para este cliente");
                }

                var products = await product.GetProductsByIdAsync(sales.Select(s => s.ProductId));

                var result = new
                {
                    Customer = customerInfo,
                    Sales = products
                };

                return Results.Ok(result);
            });
            #endregion

        }
    }
}