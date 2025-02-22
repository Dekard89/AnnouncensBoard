using AnnouncensBoard.BLL.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncensBoard.BLL.Validation
{
    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(u => u.Username).NotEmpty().WithMessage("Username is required");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Password is required").MinimumLength(8)
                .WithMessage("Password must be at least 6 characters long").Matches("[A-Z]")
                .WithMessage("Password must contains at least one upper case letter").Matches("[a-z]")
                .WithMessage("Password must contains at least one lower case letter").Matches("[0-9]")
                .WithMessage("Password must contains numbers").Equal(u => u.ConfirmPassword)
                .WithMessage("Passwords do not match");
            RuleFor(u => u.ConfirmPassword).Equal(u => u.Password).WithMessage("Passwords do not match");
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email is required").EmailAddress()
                .WithMessage("Email-format is required");
            RuleFor(u => u.Phone).NotEmpty().WithMessage("Phone is required").Matches("[0-9]")
                .WithMessage("Phone format is required").MinimumLength(11).WithMessage("Phone must contain at least 11 digits")
                .MaximumLength(11).WithMessage("Phone must contain at most 9 digits");
            RuleFor(u => u.Birthday).NotEmpty().WithMessage("Birthday is required");
               
        }
    }
}
