using AnnouncensBoard.BLL.Models.Subject;
using FluentValidation;

namespace AnnouncensBoard.BLL.Validation;

public class LoginValidator:AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
    }
}