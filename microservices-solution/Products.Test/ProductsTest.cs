using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Products.src.Data;

namespace Products.Test;

[TestClass]
public sealed class ProductsTest
{
    private WebApplicationFactory<Program> _factory = null!;
    private HttpClient _client = null!;
    private Mock<IProductRepository> _mockRepository = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockRepository = new Mock<IProductRepository>();
        
        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(IProductRepository));
                    
                    if (descriptor != null)
                        services.Remove(descriptor);

                    services.AddSingleton(_mockRepository.Object);
                });
            });

        _client = _factory.CreateClient();
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        _client.Dispose();
        await _factory.DisposeAsync();
    }

    [TestMethod]
    public async Task GetByIdReturnsOk()
    {
        // Arrange
        var productId = 1;
        var expectedProduct = new Product 
        {
            Id = 1,
            Name = "Laptop Pro 15",
            Price = 1299.99m,
            Description = "Potente laptop para desarrollo y diseño."
        };

        _mockRepository
            .Setup(r => r.GetById(productId))
            .ReturnsAsync(expectedProduct);

        // Act
        var response = await _client.GetAsync($"/api/products/{productId}");

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var content = await response.Content.ReadFromJsonAsync<Product>();
        Assert.IsNotNull(content);
        Assert.AreEqual(productId, content.Id);
        Assert.AreEqual("Laptop Pro 15", content.Name);
        Assert.AreEqual(1299.99m, content.Price);
        Assert.AreEqual("Potente laptop para desarrollo y diseño.", content.Description);
    }

    [TestMethod]
    public async Task GetByIdReturnsNotFound()
    {
        // Arrange
        var productId = 9999;

        _mockRepository
            .Setup(r => r.GetById(productId))
            .ReturnsAsync((Product?)null);

        // Act
        var response = await _client.GetAsync($"/api/products/{productId}");

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
}
