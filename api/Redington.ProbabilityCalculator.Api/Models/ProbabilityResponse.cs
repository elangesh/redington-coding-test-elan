namespace Redington.ProbabilityCalculator.Api.Models;

public class ProbabilityResponse
{
    public double Result { get; set; }
    public required string CalculationType { get; set; }
    public required ProbabilityInput Input { get; set; }
    public DateTime CalculationTime { get; set; }

}