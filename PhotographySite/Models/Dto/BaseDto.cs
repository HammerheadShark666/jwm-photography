using FluentValidation.Results;

namespace PhotographySite.Models.Dto;

public class BaseDto
{
    public List<ValidationFailure> Errors { get; set; }

    public bool IsValid { get; set; }
}
