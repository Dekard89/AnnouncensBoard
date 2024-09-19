namespace AnnoucensBoard.Domain.Entity
{
    public class Characteristic: BaseEntity
    {
        public string Value { get; set; }=String.Empty;

        public Subject Subject { get; set; }
    }
    
       
}
