using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Redington.ProbabilityCalculator.Api.Constants;
using Redington.ProbabilityCalculator.Api.Controllers;
using Redington.ProbabilityCalculator.Api.Core.Calculations;
using Redington.ProbabilityCalculator.Api.Core.Interfaces;
using Redington.ProbabilityCalculator.Api.Models;
using Redington.ProbabilityCalculator.Api.Validators;


namespace Redington.ProbabilityCalculator.Tests;

public class ProbabilityControllerTests
{
    private readonly ProbabilityController _sut;
    private readonly Mock<IProbabilityCalculatorFactory> _calculatorFactory;
    private readonly Mock<IValidator<ProbabilityInput>> _validator;
    private readonly Mock<ILogger<ProbabilityController>> _probabilityControllerLogger;
    private readonly Mock<ILogger<CombinedWithCalculation>> _combinedWithCalculationLogger;
    private readonly Mock<ILogger<EitherCalculation>> _eitherCalculationLogger;
    
    public ProbabilityControllerTests()
    {
        _calculatorFactory = new Mock<IProbabilityCalculatorFactory>();
        _validator = new Mock<IValidator<ProbabilityInput>>();
        _probabilityControllerLogger = new Mock<ILogger<ProbabilityController>>();
        _combinedWithCalculationLogger = new Mock<ILogger<CombinedWithCalculation>>();
        _eitherCalculationLogger = new Mock<ILogger<EitherCalculation>>();
        
        _sut = new ProbabilityController(_calculatorFactory.Object, _validator.Object, _probabilityControllerLogger.Object);
    }

    [Theory]
    [InlineData(-0.1, 0.5)]
    [InlineData(1.1, 0.5)]
    [InlineData(0.5, -0.1)]
    [InlineData(0.5, 1.1)]
    public async Task CombinedWith_WithInvalidProbabilities_ReturnsBadRequest(double probA, double probB)
    {
        var sut = new ProbabilityController(_calculatorFactory.Object, new ProbabilityInputValidator(), _probabilityControllerLogger.Object);

        // Arrange
        var request = new ProbabilityInput
        {
            ProbabilityA = probA,
            ProbabilityB = probB
        };

        // Act
        var combinedWithResult = await sut.CombinedWith(request);
        var eitherResult = await sut.Either(request);

        // Assert
        combinedWithResult.Result.Should().BeOfType<BadRequestObjectResult>();
        eitherResult.Result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task CombinedWith_WithValidProbabilities_ReturnsCorrectResult()
    {
        // Arrange
        var request = new ProbabilityInput
        {
            ProbabilityA = 0.5,
            ProbabilityB = 0.5
        };

        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<ProbabilityInput>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _calculatorFactory
            .Setup(c => c.CreateCalculator(Calculation.CombinedWith))
            .Returns(new CombinedWithCalculation(_combinedWithCalculationLogger.Object));
        
        // Act
        var result = await _sut.CombinedWith(request);

        // Assert
        var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var response = okResult.Value.Should().BeOfType<ProbabilityResponse>().Subject;
        response.Result.Should().Be(0.25);
        response.CalculationType.Should().Be(Calculation.CombinedWith);
        response.CalculationTime.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }


    [Fact]
    public async Task Either_WithValidProbabilities_ReturnsCorrectResult()
    {
        // Arrange
        var request = new ProbabilityInput
        {
            ProbabilityA = 0.5,
            ProbabilityB = 0.5
        };

        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<ProbabilityInput>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _calculatorFactory
            .Setup(c => c.CreateCalculator(Calculation.Either))
            .Returns(new EitherCalculation(_eitherCalculationLogger.Object));

        // Act
        var result = await _sut.Either(request);

        // Assert
        var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var response = okResult.Value.Should().BeOfType<ProbabilityResponse>().Subject;
        response.Result.Should().Be(0.75);
        response.CalculationType.Should().Be(Calculation.Either);
        response.CalculationTime.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
}