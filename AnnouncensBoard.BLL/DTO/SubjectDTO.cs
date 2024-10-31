using AnnouncensBoard.BLL.Models.Subject;

namespace AnnouncensBoard.BLL.Models;

public record SubjectDTO : AbstractModel
{
    public double Price { get; set; }

    public string Discription { get; set; } = string.Empty;
        
    public bool AdultOnly {get;set;}

    public List<CharacteristicDTO> Characteristics { get; set; } = new ();
    
}