using FluentValidation;
using PhotographySite.Dto.Response;
using PhotographySite.Helpers.Exceptions;
using PhotographySite.Helpers.Interface;

namespace PhotographySite.Helpers;
public class ValidatorHelper<T>(IValidator<T> validator) : IValidatorHelper<T>
{
    public async Task ValidateAsync(T itemToValidate, string ruleSet)
    {
        var validationResult = await validator.ValidateAsync(itemToValidate, options => options.IncludeRuleSets(ruleSet));
        if (!validationResult.IsValid)
            throw new FailedValidationException(new FailedValidationResponse(validationResult.Errors, false));
    }

    public async Task<FluentValidation.Results.ValidationResult> AfterEventAsync(T itemToValidate, string ruleSet)
    {
        return await validator.ValidateAsync(itemToValidate, options => options.IncludeRuleSets(ruleSet)); ;
    }
}