using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PBP.Web.Models.Domain;

namespace PBP.Web.Models.Context
{
    public class FileContext : DbContext
    {
        public FileContext(DbContextOptions<FileContext> options) : base(options)
        {
        }

        public DbSet<File> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<File>()
                .HasKey(f => f.Guid);

            modelBuilder.Entity<File>()
                .Property(f => f.CreateTime)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<File>()
                .Property(f => f.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()")
                .Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            modelBuilder.Entity<File>()
                .Property(f => f.Name);

            modelBuilder.Entity<File>()
                .Property(f => f.FileName);

            modelBuilder.Entity<File>()
                .Property(f => f.Length);

            modelBuilder.Entity<File>()
                .Property(f => f.ContentType);
        }
    }
}
