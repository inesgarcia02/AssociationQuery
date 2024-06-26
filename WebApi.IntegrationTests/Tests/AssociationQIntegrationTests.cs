using System.Text.Json;
using DataModel.Repository;
using Microsoft.Extensions.DependencyInjection;
using WebApi.IntegrationTests.Helpers;

namespace WebApi.IntegrationTests;

public class AssociationQIntegrationTests : IClassFixture<IntegrationTestsWebApplicationFactory<Program>>
{
    private readonly IntegrationTestsWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public AssociationQIntegrationTests(IntegrationTestsWebApplicationFactory<Program> factory)
    {
        _factory = factory;

        // _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        // {
        //     AllowAutoRedirect = false
        // });

        _client = factory.CreateClient();
    }


    [Theory]
    [InlineData("/api/Association")]
    [InlineData("/api/Association/colaborator/1")]
    [InlineData("/api/Association/ByProject/1")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        // Arrange
        //var client = _factory.CreateClient();

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("application/json; charset=utf-8",
            response.Content.Headers.ContentType.ToString());
    }


    [Theory]
    [InlineData("/api/Association/1")]
    public async Task GetById_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        // Arrange
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AbsanteeContext>();

            Utilities.ReinitializeDbForTests(db);
        }

        // var client = _factory.CreateClient();

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("application/json; charset=utf-8",
            response.Content.Headers.ContentType.ToString());
    }

    [Theory]
    [InlineData("/api/Association/4")]
    public async Task GetById_EndpointsReturnNotFoundAndCorrectContentType(string url)
    {
        // Arrange
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AbsanteeContext>();

            Utilities.ReinitializeDbForTests(db);
        }

        // var client = _factory.CreateClient();

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }

    [Theory]
    [InlineData("/api/Association/1")]
    public async Task GetById_ReturnData(string url)
    {
        // Arrange
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AbsanteeContext>();

            Utilities.ReinitializeDbForTests(db);
        }

        // var client = _factory.CreateClient();

        var response = await _client.GetAsync(url);

        var responseBody = await response.Content.ReadAsStringAsync();
        Assert.NotNull(responseBody);

        var jsonDocument = JsonDocument.Parse(responseBody);
        var jsonObject = jsonDocument.RootElement;

        Assert.True(jsonObject.ValueKind == JsonValueKind.Object, "Response body is not a JSON object");

        Assert.True(jsonObject.TryGetProperty("id", out JsonElement idElement), "Object does not have 'id' property");
        Assert.True(jsonObject.TryGetProperty("colaboratorId", out JsonElement colabId), "Object does not have 'colabId' property");
        Assert.True(jsonObject.TryGetProperty("projectId", out JsonElement projId), "Object does not have 'projId' property");
        Assert.True(jsonObject.TryGetProperty("startDate", out JsonElement startDate), "Object does not have 'startDate' property");
        Assert.True(jsonObject.TryGetProperty("endDate", out JsonElement endDate), "Object does not have 'endDate' property");
        Assert.True(jsonObject.TryGetProperty("fundamental", out JsonElement fundamental), "Object does not have 'fundamental' property");
        Assert.True(jsonObject.TryGetProperty("isPendent", out JsonElement pendent), "Object does not have 'pendent' property");
    }


    [Theory]
    [InlineData("/api/Association", 3)]
    [InlineData("/api/Association/colaborator/1", 2)]
    [InlineData("/api/Association/ByProject/1", 2)]
    public async Task Get_ReturnData(string url, int expected)
    {
        // Arrange
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AbsanteeContext>();

            Utilities.ReinitializeDbForTests(db);
        }

        // var client = _factory.CreateClient();

        var response = await _client.GetAsync(url);

        var responseBody = await response.Content.ReadAsStringAsync();
        Assert.NotNull(responseBody);

        var jsonDocument = JsonDocument.Parse(responseBody);
        var jsonArray = jsonDocument.RootElement;

        Assert.True(jsonArray.ValueKind == JsonValueKind.Array, "Response body is not a JSON array");
        Assert.Equal(expected, jsonArray.GetArrayLength());
    }
}