using FluentValidation;
using Redington.ProbabilityCalculator.Api.Core.Calculations;
using Redington.ProbabilityCalculator.Api.Core.Interfaces;
using Redington.ProbabilityCalculator.Api.Models;
using Redington.ProbabilityCalculator.Api.Services;
using Redington.ProbabilityCalculator.Api.Validators;

namespace Redington.ProbabilityCalculator.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
            
        services.AddScoped<IProbabilityCalculatorFactory, ProbabilityCalculatorFactory>();
        services.AddScoped<IValidator<ProbabilityInput>, ProbabilityInputValidator>();
        services.AddScoped<ICombinedWithCalculation, CombinedWithCalculation>();
        services.AddScoped<IEitherCalculation, EitherCalculation>();
            
        return services;
    }
}