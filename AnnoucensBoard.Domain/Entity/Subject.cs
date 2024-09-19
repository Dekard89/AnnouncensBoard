using System.Collections;

namespace AnnoucensBoard.Domain.Entity
{
    public class Subject : BaseEntity
    {
        public double Price { get; set; }

        public string Discription { get; set; } = string.Empty;

        public List<Characteristic> Characteristics { get; set; } = new List<Characteristic>();

        public List<Topic> Topics { get; set; }= new List<Topic>();
    }
}
