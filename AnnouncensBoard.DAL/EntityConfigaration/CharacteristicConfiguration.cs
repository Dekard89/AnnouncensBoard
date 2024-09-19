using AnnoucensBoard.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncensBoard.DAL.EntityConfigaration
{
    internal class CharacteristicConfiguration : IEntityTypeConfiguration<Characteristic>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Characteristic> builder)
        {
            builder.ToTable("Characteristics_Table");

            builder.Property(x => x.Title).HasMaxLength(50);

            builder.HasKey(x => x.Id);

            builder.HasOne(s => s.Subject).WithMany(Subject => Subject.Characteristics);
            
        }
    }
}
