using AnnoucensBoard.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncensBoard.DAL.EntityConfigaration
{
    internal class SubjectConfiguration : IEntityTypeConfiguration<Subject>

    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("Subject_table");

            

            builder.HasOne(x => x.Characteristics).WithMany().OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Topic).WithOne(s => s.Subject).OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(t => t.Subject);
        }
    }
}
