namespace AnnoucensBoard.Domain.Entity
{
    public abstract class BaseEntity
    {
        public int Id { get; init; }

        public string Title { get; init; } = string.Empty;
    }
}
