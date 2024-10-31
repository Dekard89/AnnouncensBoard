using AnnouncensBoard.BLL.Models;
using AnnouncensBoard.BLL.Models.Subject;
using FluentValidation;

namespace AnnouncensBoard.BLL.Validation;

public class SubjectValidator: AbstractValidator<SubjectDTO>
{
    public SubjectValidator()
    {
        RuleFor(p=>p.Title).NotEmpty().WithMessage("Title is required").Must(x=>x.Length <= 100)
            .WithMessage("Title must be between 3 and 100 characters").Must(x=>x.Length>3)
            .WithMessage("Title must be between 3 and 100 characters");
        RuleFor(p => p.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(p => p.AdultOnly).NotEmpty().WithMessage("is required");
        RuleForEach(p=>p.Characteristics).SetValidator(new CharacteristicValidator());
        
    }
}