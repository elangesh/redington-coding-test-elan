using Redington.ProbabilityCalculator.Api.Models;

namespace Redington.ProbabilityCalculator.Api.Core.Interfaces;

public interface IProbabilityCalculation
{
    ProbabilityResponse Calculate(ProbabilityInput input);
}