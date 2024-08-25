using PhotographySite.Dto.Response;

namespace PhotographySite.Helpers.Exceptions;

public class FailedValidationException : Exception
{
    public FailedValidationException(FailedValidationResponse failedValidationResponse)
    {
        FailedValidationResponse = failedValidationResponse;
    }

    public FailedValidationResponse FailedValidationResponse { get; set; }
}