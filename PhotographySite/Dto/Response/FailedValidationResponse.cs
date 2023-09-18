using FluentValidation;
using FluentValidation.Results;

namespace PhotographySite.Dto.Response;

public class FailedValidationResponse
{
    public List<Message> Messages { get; set; }
    public bool IsValid { get; set; }

    public FailedValidationResponse()
    {
        Messages = new();
        IsValid = true;
    }

    public FailedValidationResponse(List<ValidationFailure> rules, bool isValid)
    {
        IsValid = isValid;
        Rules = rules;
    }

    public List<ValidationFailure> Rules
    {
        set
        {
            Messages = new();
            foreach (ValidationFailure validationFailure in value)
            {
                Messages.Add(new Message() { Text = validationFailure.ErrorMessage, Severity = GetServerity(validationFailure.Severity) });
            }
        }
    }

    private string GetServerity(Severity severity)
    {
        return severity switch
        {
            Severity.Error => "error",
            Severity.Warning => "warning",
            Severity.Info => "info",
            _ => "",
        };
    }

    public void SetValidationResponse(List<ValidationFailure> rules, bool isValid)
    {
        IsValid = isValid;
        Rules = rules;
    }
}