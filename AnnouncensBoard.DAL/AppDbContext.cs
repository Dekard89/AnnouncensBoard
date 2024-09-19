using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AnnoucensBoard.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AnnouncensBoard.DAL.EntityConfigaration;

namespace AnnouncensBoard.DAL
{
    public  class AppDbContext : IdentityDbContext<IdentityUser>
    {
        private readonly IConfiguration _config;

        public DbSet<Topic> Topics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_config.GetConnectionString("Postgres"))
                .EnableSensitiveDataLogging();
        }

        public AppDbContext(IConfiguration configuration)
        {
            _config=configuration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TopicConfiguration());
            builder.ApplyConfiguration(new SubjectConfiguration());
            builder.ApplyConfiguration(new CharacteristicConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.Entity<IdentityUserLogin<string>>().HasKey(x => x.UserId);
            builder.Entity<IdentityUserRole<string>>().HasKey(x => x.UserId);
            builder.Entity<IdentityUserToken<string>>().HasKey(x => x.UserId);
            builder.Entity<IdentityUserClaim<string>>().HasKey(x => x.UserId);
            
        }
    }
}
