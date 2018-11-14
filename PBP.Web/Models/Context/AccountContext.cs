using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PBP.Web.Models.Domain;

namespace PBP.Web.Models.Context
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasKey(account => account.Guid);

            modelBuilder.Entity<Account>()
                .HasAlternateKey(account => account.UserName);

            modelBuilder.Entity<Account>()
                .Property(account => account.CreateTime)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Account>()
                .Property(account => account.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()")
                .Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
        }
    }
}