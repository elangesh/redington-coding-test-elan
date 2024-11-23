using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Redington.ProbabilityCalculator.Api.Middleware;

namespace Redington.ProbabilityCalculator.Tests;

public class GlobalExceptionMiddlewareTests
{
    private readonly GlobalExceptionMiddleware _middleware;
    private readonly Mock<RequestDelegate> _nextMock;

    public GlobalExceptionMiddlewareTests()
    {
        var loggerMock = new Mock<ILogger<GlobalExceptionMiddleware>>();
        _nextMock = new Mock<RequestDelegate>();
        _middleware = new GlobalExceptionMiddleware(_nextMock.Object, loggerMock.Object);
    }

    [Fact]
    public async Task InvokeAsync_WhenNoException_CallsNext()
    {
        // Arrange
        var context = new DefaultHttpContext();
        _nextMock.Setup(next => next(context))
            .Returns(Task.CompletedTask);

        // Act
        await _middleware.InvokeAsync(context);

        // Assert
        _nextMock.Verify(next => next(context), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_WhenExceptionOccurs_ReturnsErrorResponse()
    {
        // Arrange
        var context = new DefaultHttpContext
        {
            Response =
            {
                Body = new MemoryStream()
            }
        };
        var expectedException = new Exception("Test exception");

        _nextMock.Setup(next => next(context)).ThrowsAsync(expectedException);

        // Act
        await _middleware.InvokeAsync(context);

        // Assert
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var reader = new StreamReader(context.Response.Body);
        var responseBody = await reader.ReadToEndAsync();

        responseBody.Should().Contain("Test exception");
        context.Response.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        context.Response.ContentType.Should().Be("application/json; charset=utf-8");
    }
}