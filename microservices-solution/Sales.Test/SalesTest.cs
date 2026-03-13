using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Sales.Data;
using Sales.Models;

namespace Sales.Test;

[TestClass]
public sealed class SalesTest
{
    private WebApplicationFactory<Program> _factory = null!;
    private HttpClient _client = null!;
    private Mock<ISalesRepository> _mockRepository = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockRepository = new Mock<ISalesRepository>();
        
        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(ISalesRepository));
                    
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
        var saleId = 1;
        var expectedSale = new Sale
        {
            Id = saleId,
            ProductId = 1,
            CustomerId = 1,
            Quantity = 5
        };

        _mockRepository
            .Setup(r => r.GetByIdAsync(saleId))
            .ReturnsAsync(expectedSale);

        // Act
        var response = await _client.GetAsync($"/api/sales/{saleId}");

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        Assert.IsFalse(string.IsNullOrEmpty(content));
        
        var jsonElement = JsonSerializer.Deserialize<JsonElement>(content);
        Assert.IsTrue(jsonElement.TryGetProperty("id", out var idProperty));
        Assert.AreEqual(saleId, idProperty.GetInt32());
        Assert.IsTrue(jsonElement.TryGetProperty("quantity", out var quantityProperty));
        Assert.AreEqual(5, quantityProperty.GetInt32());
    }

    [TestMethod]
    public async Task GetByIdReturnsNotFound()
    {
        // Arrange
        var saleId = 9999;

        _mockRepository
            .Setup(r => r.GetByIdAsync(saleId))
            .ReturnsAsync((Sale?)null);

        // Act
        var response = await _client.GetAsync($"/api/sales/{saleId}");

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
}
