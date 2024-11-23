using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Redington.ProbabilityCalculator.Api.Constants;
using Redington.ProbabilityCalculator.Api.Core.Interfaces;
using Redington.ProbabilityCalculator.Api.Models;


namespace Redington.ProbabilityCalculator.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProbabilityController : ControllerBase
{
    
    private readonly IProbabilityCalculatorFactory _calculatorFactory;
    private readonly IValidator<ProbabilityInput> _validator;
    private readonly ILogger<ProbabilityController> _logger;

    public ProbabilityController(IProbabilityCalculatorFactory calculatorFactory, IValidator<ProbabilityInput> validator, ILogger<ProbabilityController> logger)
    {
        _calculatorFactory = calculatorFactory;
        _validator = validator;
        _logger = logger;
    }

    [HttpGet("combined")] 
    public async Task<ActionResult<ProbabilityResponse>> CombinedWith([FromQuery] ProbabilityInput input) 
    {
        var validationResult = await ValidateInput(input, nameof(CombinedWith));
        if (validationResult != null)
        {
            return validationResult;
        }

        var calculator = _calculatorFactory.CreateCalculator(Calculation.CombinedWith);
        var result = calculator.Calculate(input);
        return Ok(result);
    }

    [HttpGet("either")]
    public async Task<ActionResult<ProbabilityResponse>> Either([FromQuery] ProbabilityInput input)
    {
        var validationResult = await ValidateInput(input, nameof(Either));
        if (validationResult != null)
        {
            return validationResult;
        }

        var calculator = _calculatorFactory.CreateCalculator(Calculation.Either);
        var result = calculator.Calculate(input);
        return Ok(result);
    }

    private async Task<ActionResult<ProbabilityResponse>?> ValidateInput(ProbabilityInput input, string methodName)
    {
        var validationResult = await _validator.ValidateAsync(input);
        if (!validationResult.IsValid)
        {
            _logger.LogError($"Calculating {methodName}: P(A)={input.ProbabilityA}, P(B)={input.ProbabilityB}, Result: Bad Request due invalid input");
            return BadRequest(validationResult.Errors);
        }

        return null;
    }
}