using AnnouncensBoard.BLL.Models;
using FluentValidation;

namespace AnnouncensBoard.BLL.Validation;

public class CharacteristicValidator : AbstractValidator<CharacteristicDTO>
{
    public CharacteristicValidator()
    {
        RuleFor(c=>c.Title).NotEmpty().WithMessage("Title is required").Must(t=>t.Length is > 1 and < 100)
            .WithMessage("Title must be between 0 and 100 characters");
        RuleFor(c=>c.Value).NotEmpty().WithMessage("Value is required").Must(t=>t.Length is > 1 and < 100)
            .WithMessage("Type must be between 0 and 100 characters");
    }
}