using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Redington.ProbabilityCalculator.Api.Constants;
using Redington.ProbabilityCalculator.Api.Core.Calculations;
using Redington.ProbabilityCalculator.Api.Models;
using Redington.ProbabilityCalculator.Api.Services;

namespace Redington.ProbabilityCalculator.Tests;

public class ProbabilityCalculatorFactoryTests
{
    private readonly ProbabilityCalculatorFactory _sut;

    public ProbabilityCalculatorFactoryTests()
    {
        var combinedWithCalculationLogger = new Mock<ILogger<CombinedWithCalculation>>();
        var eitherCalculationLogger = new Mock<ILogger<EitherCalculation>>();

        var combinedWithCalculation = new CombinedWithCalculation(combinedWithCalculationLogger.Object);
        var eitherCalculation = new EitherCalculation(eitherCalculationLogger.Object);
        _sut = new ProbabilityCalculatorFactory(combinedWithCalculation, eitherCalculation);
    }

    [Theory]
    [InlineData(0.5, 0.5, 0.25)]
    [InlineData(1, 1, 1)]
    [InlineData(0, 0, 0)]
    public void CombinedWith_ShouldCalculateCorrectly(double probA, double probB, double expected)
    {
        // Act
        var result =  _sut.CreateCalculator(Calculation.CombinedWith);
        var response = result.Calculate(new ProbabilityInput { ProbabilityA = probA, ProbabilityB = probB });
        
        // Assert
        response.Result.Should().Be(expected);
    }

    [Theory]
    [InlineData(0.5, 0.5, 0.75)]
    [InlineData(1, 1, 1)]
    [InlineData(0, 0, 0)]
    public void  Either_ShouldCalculateCorrectly(double probA, double probB, double expected)
    {
        // Act
        var result = _sut.CreateCalculator(Calculation.Either);
        var response = result.Calculate(new ProbabilityInput { ProbabilityA = probA, ProbabilityB = probB });

        // Assert
        response.Result.Should().Be(expected);
    }
}