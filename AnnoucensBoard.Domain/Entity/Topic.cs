namespace AnnoucensBoard.Domain.Entity
{
    public class Topic : BaseEntity
    {
        public DateTime CreateTime { get; set; }

        public string Author { get; set; } = string.Empty;

        public Category Category { get; set; }

        public Subject Subject { get; set; }


    }
}
