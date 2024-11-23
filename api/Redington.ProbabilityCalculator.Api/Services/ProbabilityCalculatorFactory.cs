using Redington.ProbabilityCalculator.Api.Constants;
using Redington.ProbabilityCalculator.Api.Core.Calculations;
using Redington.ProbabilityCalculator.Api.Core.Interfaces;

namespace Redington.ProbabilityCalculator.Api.Services;

public class ProbabilityCalculatorFactory : IProbabilityCalculatorFactory
{
    private readonly ICombinedWithCalculation _combinedWithCalculation;
    private readonly IEitherCalculation _eitherCalculation;

    public ProbabilityCalculatorFactory(ICombinedWithCalculation combinedWithCalculation, IEitherCalculation eitherCalculation)
    {
        _combinedWithCalculation = combinedWithCalculation;
        _eitherCalculation = eitherCalculation;
    }

    public IProbabilityCalculation CreateCalculator(string calculationType)
    {
        return calculationType switch
        {
            Calculation.CombinedWith => _combinedWithCalculation,
            Calculation.Either => _eitherCalculation,
            _ => throw new ArgumentException("Invalid calculation type", nameof(calculationType))
        };
    }
}