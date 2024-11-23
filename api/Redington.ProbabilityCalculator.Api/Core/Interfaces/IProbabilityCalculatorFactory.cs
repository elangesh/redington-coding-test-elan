namespace Redington.ProbabilityCalculator.Api.Core.Interfaces;

public interface IProbabilityCalculatorFactory
{
    IProbabilityCalculation CreateCalculator(string calculationType);
}