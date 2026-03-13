using System.Net;
using System.Text.Json;
using Customers.src.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Customers.Test;

[TestClass]
public sealed class CustomersTest
{
    private WebApplicationFactory<Program> _factory = null!;
    private HttpClient _client = null!;
    private Mock<ICustomerRepository> _mockRepository = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockRepository = new Mock<ICustomerRepository>();
        
        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Reemplazar el repositorio real con el mock
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(ICustomerRepository));
                    
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
    public async Task GetAsyncReturnsOk()
    {
        // Arrange
        var customerId = 1;
        var expectedCustomer = new Customer
        {
            Id = customerId,
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com"
        };

        _mockRepository
            .Setup(r => r.GetByIdAsync(customerId))
            .ReturnsAsync(expectedCustomer);

        // Act
        var response = await _client.GetAsync($"/api/customers/{customerId}");

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        Assert.IsFalse(string.IsNullOrEmpty(content));
        
        var jsonElement = JsonSerializer.Deserialize<JsonElement>(content);
        Assert.IsTrue(jsonElement.TryGetProperty("id", out var idProperty));
        Assert.AreEqual(customerId, idProperty.GetInt32());
        Assert.IsTrue(jsonElement.TryGetProperty("firstName", out var firstNameProperty));
        Assert.AreEqual("John", firstNameProperty.GetString());
    }

    [TestMethod]
    public async Task GetAsyncReturnsNotFound()
    {
        // Arrange
        var customerId = 9999;

        _mockRepository
            .Setup(r => r.GetByIdAsync(customerId))
            .ReturnsAsync((Customer?)null);

        // Act
        var response = await _client.GetAsync($"/api/customers/{customerId}");

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
}
