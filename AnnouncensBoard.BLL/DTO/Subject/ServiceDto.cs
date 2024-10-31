using System.ComponentModel.DataAnnotations;

namespace AnnouncensBoard.BLL.Models.Subject;

public record ServiceDto : SubjectDTO
{
    [Required(ErrorMessage = "is required")]
    public TimeSpan LeadTime { get; set; } = new(0, 0, 0, 0);
}