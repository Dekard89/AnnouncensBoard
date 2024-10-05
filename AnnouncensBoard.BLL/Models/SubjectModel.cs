using AnnouncensBoard.BLL.Models.Subject;

namespace AnnouncensBoard.BLL.Models;

public class SubjectModel : AbstractModel
{
    public double Price { get; set; }

    public string Discription { get; set; } = string.Empty;
        
    public bool AdultOnly {get;set;}
    
    public bool IsAvailable {get;set;}
    
    public List<CharacteristicModel> Characteristics { get; set; } = new ();
    
    public List<TopicModel> Topics { get; set; }= new ();
}