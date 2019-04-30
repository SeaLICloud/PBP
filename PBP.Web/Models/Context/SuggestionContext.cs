using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PBP.Web.Models.Domain;

namespace PBP.Web.Models.Context
{
    public class SuggestionContext : DbContext
    {
        public SuggestionContext(DbContextOptions<SuggestionContext> options) : base(options)
        {
        }

        public DbSet<Suggestion> Suggestions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Suggestion>()
                .HasKey(s => s.Guid);

            modelBuilder.Entity<Suggestion>()
                .Property(s => s.CreateTime)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Suggestion>()
                .Property(s => s.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()")
                .Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            modelBuilder.Entity<Suggestion>()
                .Property(s => s.SendEmail);
            modelBuilder.Entity<Suggestion>()
                .Property(s => s.AcceptEmail);
            modelBuilder.Entity<Suggestion>()
                .Property(s => s.Title);
            modelBuilder.Entity<Suggestion>()
                .Property(s => s.Content);
            modelBuilder.Entity<Suggestion>()
                .Property(s => s.AcceptEmail);
        }
    }
}
