
using System.ComponentModel.DataAnnotations;

namespace AnnouncensBoard.BLL.Models.Subject;

public record ProductDto : SubjectDTO
{
    [Required(ErrorMessage = "Is required")]
    [Range(1,100,ErrorMessage ="Out of range")]
    public int Quantity { get; set; }
}