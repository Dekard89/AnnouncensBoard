using AnnoucensBoard.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnnouncensBoard.DAL.EntityConfigaration
{
    public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.ToTable("Topic_Table");

            builder.HasKey(t => t.Id);


           // builder.HasOne(t => t.Subject).WithMany(s => s.Topics).OnDelete(DeleteBehavior.Restrict);
           
        }
    }
}
