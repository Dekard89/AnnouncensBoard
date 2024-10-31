using AnnouncensBoard.BLL.Models;
using AnnouncensBoard.BLL.Models.Subject;
using FluentValidation;

namespace AnnouncensBoard.BLL.Validation;

public class TopicValidator : AbstractValidator<TopicDTO>
{
    public TopicValidator()
    {
        RuleFor(t => t.Title).NotEmpty().WithMessage("Title is required").Must(x=>x.Length >1)
            .WithMessage("Title must be at least 2 characters long")
            .MaximumLength(50).WithMessage("Title must be no more than 50 characters");
        RuleFor(t => t.CategoryDto).NotEmpty().WithMessage("Category is required");
        RuleFor(t => t.Subject).NotEmpty().WithMessage("Subject is required")
            .SetValidator(new SubjectValidator());
        

    }
}