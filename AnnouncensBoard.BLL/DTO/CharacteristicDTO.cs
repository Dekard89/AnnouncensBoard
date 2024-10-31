using AnnouncensBoard.BLL.Models.Subject;

namespace AnnouncensBoard.BLL.Models;

public record CharacteristicDTO: AbstractModel
{
    public string Value { get; set; } = String.Empty;
}