using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Search.Services.Customers;


namespace Search.Presentation
{
    public static class SearchEndpoints
    {
        public static void MapSearchEndpoints(this IEndpointRouteBuilder app)
        {
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

        }
    }
}