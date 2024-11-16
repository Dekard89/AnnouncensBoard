namespace AnnouncensBoard.BLL.DTO
{
    public record TopicDTO : AbstractModel
    {
        public string Author { get; set; } = string.Empty;

        public CategoryDTO CategoryDto { get; set; }


        public DateTime CreatedTime { get; set; }

        public SubjectDTO Subject { get; set; }
    }
}
