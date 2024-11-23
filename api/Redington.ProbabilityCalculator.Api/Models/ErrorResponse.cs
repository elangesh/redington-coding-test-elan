namespace Redington.ProbabilityCalculator.Api.Models;

public class ErrorResponse
{
    public string? Message { get; set; }
    public required string TraceId { get; set; }
}