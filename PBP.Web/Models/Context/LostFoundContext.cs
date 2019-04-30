using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PBP.Web.Models.Domain;

namespace PBP.Web.Models.Context
{
    public class LostFoundContext : DbContext
    {
        public LostFoundContext(DbContextOptions<LostFoundContext> options) : base(options)
        {
        }

        public DbSet<LostFound> LostFounds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LostFound>()
                .HasKey(lF => lF.Guid);

            modelBuilder.Entity<LostFound>()
                .Property(lF => lF.CreateTime)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<LostFound>()
                .Property(lF => lF.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()")
                .Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            modelBuilder.Entity<LostFound>()
                .Property(lF => lF.Name);
            modelBuilder.Entity<LostFound>()
                .Property(lF => lF.Description);

            modelBuilder.Entity<LostFound>()
                .Property(lF => lF.Person);
            modelBuilder.Entity<LostFound>()
                .Property(lF => lF.Address);
            modelBuilder.Entity<LostFound>()
                .Property(lF => lF.Time);

            modelBuilder.Entity<LostFound>()
                .Property(lF => lF.FoundPerson);
            modelBuilder.Entity<LostFound>()
                .Property(lF => lF.FoundAddress);
            modelBuilder.Entity<LostFound>()
                .Property(lF => lF.FoundTime);

            modelBuilder.Entity<LostFound>()
                .Property(lF => lF.State);
        }
    }
}
