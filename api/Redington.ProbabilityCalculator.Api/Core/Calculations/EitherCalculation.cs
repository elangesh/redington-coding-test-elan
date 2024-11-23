using Redington.ProbabilityCalculator.Api.Constants;
using Redington.ProbabilityCalculator.Api.Core.Interfaces;
using Redington.ProbabilityCalculator.Api.Models;

namespace Redington.ProbabilityCalculator.Api.Core.Calculations;

public interface IEitherCalculation : IProbabilityCalculation;

public class EitherCalculation : ProbabilityCalculation, IEitherCalculation
{
    public EitherCalculation(ILogger<EitherCalculation> logger) : base(logger)
    {
    }

    protected override string CalculationType => Calculation.Either;

    protected override ProbabilityResponse CalculateCore(ProbabilityInput input)
    {
        return new ProbabilityResponse
        {
            Result = ((input.ProbabilityA + input.ProbabilityB) - (input.ProbabilityA * input.ProbabilityB)),
            CalculationType = CalculationType,
            Input = input,
            CalculationTime = DateTime.UtcNow
        };

    }
}