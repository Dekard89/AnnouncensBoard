using System.Diagnostics;
using AnnouncensBoard.BLL.Models.Subject;

namespace AnnouncensBoard.BLL.Models;

public class TopicModel : AbstractModel
{
    public string Author { get; set; } = string.Empty;
    
    public string PhoneNumber { get; set; } = string.Empty;
    
    public Category Category { get; set; }
    
    public Actuality Actuality { get; set; }
    
    public DateTime CreatedTime { get; set; }
    
    public SubjectModel Subject { get; set; }
}