using System.Collections;

namespace AnnoucensBoard.Domain.Entity
{
    public abstract class Subject : BaseEntity
    {
        public double Price { get; set; }

        public string Discription { get; set; } = string.Empty;

        public ICollection<Characteristic> Characteristics { get; set; } = new HashSet<Characteristic>();

        public ICollection<Topic> Topic { get; set; }
    }
}
