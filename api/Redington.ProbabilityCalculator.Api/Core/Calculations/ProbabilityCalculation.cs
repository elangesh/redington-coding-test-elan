using Redington.ProbabilityCalculator.Api.Core.Interfaces;
using Redington.ProbabilityCalculator.Api.Models;

namespace Redington.ProbabilityCalculator.Api.Core.Calculations;

public abstract class ProbabilityCalculation : IProbabilityCalculation
{
    private readonly ILogger<ProbabilityCalculation> _logger;

    protected ProbabilityCalculation(ILogger<ProbabilityCalculation> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected abstract ProbabilityResponse CalculateCore(ProbabilityInput input);

    protected abstract string CalculationType { get; }


    public ProbabilityResponse Calculate(ProbabilityInput input)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        var probabilityResponse = CalculateCore(input);
        _logger.LogInformation($"Calculating {CalculationType}: P(A)={input.ProbabilityA}, P(B)={input.ProbabilityB}, Result: {probabilityResponse.Result}");
        
        return probabilityResponse;
    }
}