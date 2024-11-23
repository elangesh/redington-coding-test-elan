using FluentValidation;
using Redington.ProbabilityCalculator.Api.Models;

namespace Redington.ProbabilityCalculator.Api.Validators;

public class ProbabilityInputValidator : AbstractValidator<ProbabilityInput>
{
    public ProbabilityInputValidator()
    {
        const string validationMessageA = "Probability A must be between 0 and 1";
        const string validationMessageB = "Probability B must be between 0 and 1";

        RuleFor(x => x.ProbabilityA)
            .InclusiveBetween(0, 1)
            .WithMessage(validationMessageA);

        
        RuleFor(x => x.ProbabilityB)
            .InclusiveBetween(0, 1)
            .WithMessage(validationMessageB);
    }
}