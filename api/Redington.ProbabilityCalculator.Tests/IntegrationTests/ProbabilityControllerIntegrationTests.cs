using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Redington.ProbabilityCalculator.Api;
using Redington.ProbabilityCalculator.Api.Models;

namespace Redington.ProbabilityCalculator.Tests.IntegrationTests;

public class ProbabilityControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    public ProbabilityControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData(-0.1, 0.5)]
    [InlineData(1.1, 0.5)]
    [InlineData(0.5, -0.1)]
    [InlineData(0.5, 1.1)]
    public async Task CombinedWith_WithInvalidProbabilities_ReturnsBadRequest(double probA, double probB)
    {
        // Act
        var response = await _client.GetAsync($"/api/probability/combined?probabilityA={probA}&probabilityB={probB}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("must be between 0 and 1");
    }

    [Fact]
    public async Task CombinedWith_WithValidProbabilities_ReturnsSuccess()
    {
        // Act
        var response = await _client.GetAsync("/api/probability/combined?probabilityA=0.5&probabilityB=0.5");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<ProbabilityResponse>();
        result.Should().NotBeNull();
        result!.Result.Should().Be(0.25);
    }
}