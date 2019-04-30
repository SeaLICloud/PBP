using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PBP.Web.Models.Domain;

namespace PBP.Web.Models.Context
{
    public class ThreeSessionContext : DbContext
    {
        public ThreeSessionContext(DbContextOptions<ThreeSessionContext> options) : base(options)
        {
        }
        public DbSet<ThreeSession> ThreeSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ThreeSession>()
                .HasKey(tS => tS.Guid);

            modelBuilder.Entity<ThreeSession>()
                .Property(tS => tS.CreateTime)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ThreeSession>()
                .Property(tS => tS.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()")
                .Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;


            modelBuilder.Entity<ThreeSession>()
                .Property(lF => lF.Type);

            modelBuilder.Entity<ThreeSession>()
                .Property(lF => lF.Theme);

            modelBuilder.Entity<ThreeSession>()
                .Property(lF => lF.PrimaryCoverage);

            modelBuilder.Entity<ThreeSession>()
                .Property(lF => lF.ShouldArrive);

            modelBuilder.Entity<ThreeSession>()
                .Property(lF => lF.TrueTo);

            modelBuilder.Entity<ThreeSession>()
                .Property(lF => lF.Time);

            modelBuilder.Entity<ThreeSession>()
                .Property(lF => lF.Address);

            modelBuilder.Entity<ThreeSession>()
                .Property(lF => lF.Person);

        }
    }
}
