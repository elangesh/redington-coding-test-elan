using Redington.ProbabilityCalculator.Api.Constants;
using Redington.ProbabilityCalculator.Api.Core.Interfaces;
using Redington.ProbabilityCalculator.Api.Models;

namespace Redington.ProbabilityCalculator.Api.Core.Calculations;

public interface ICombinedWithCalculation : IProbabilityCalculation;

public class CombinedWithCalculation : ProbabilityCalculation, ICombinedWithCalculation
{
    public CombinedWithCalculation(ILogger<CombinedWithCalculation> logger) : base(logger)
    {
    }

    protected override string CalculationType => Calculation.CombinedWith;

    protected override ProbabilityResponse CalculateCore(ProbabilityInput input)
    {
        return new ProbabilityResponse
        {
            Result = input.ProbabilityA * input.ProbabilityB,
            CalculationType = CalculationType,
            Input = input,
            CalculationTime = DateTime.UtcNow
        };

    }
}