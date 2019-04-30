using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PBP.Web.Models.Domain;

namespace PBP.Web.Models.Context
{
    public class SystemUpdateContext : DbContext
    {
        public SystemUpdateContext(DbContextOptions<SystemUpdateContext> options) : base(options)
        {
        }

        public DbSet<SystemUpdate> SystemUpdates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SystemUpdate>()
                .HasKey(sU => sU.Guid);

            modelBuilder.Entity<SystemUpdate>()
                .Property(sU => sU.CreateTime)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<SystemUpdate>()
                .Property(sU => sU.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()")
                .Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            modelBuilder.Entity<SystemUpdate>()
                .Property(sU => sU.Version);

            modelBuilder.Entity<SystemUpdate>()
                .Property(sU => sU.Title);

            modelBuilder.Entity<SystemUpdate>()
                .Property(sU => sU.Content);

            modelBuilder.Entity<SystemUpdate>()
                .Property(sU => sU.Time);
        }
    }
}
