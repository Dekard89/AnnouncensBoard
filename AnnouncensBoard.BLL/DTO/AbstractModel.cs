namespace AnnouncensBoard.BLL.Models;

public record AbstractModel
{
    public int Id { get; set; }
    
    public string Title { get; set; }=String.Empty;
}