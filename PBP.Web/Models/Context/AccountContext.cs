using Microsoft.EntityFrameworkCore;
using PBP.Web.Models.Common;
using PBP.Web.Models.Domain;

namespace PBP.Web.Models.Context
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasKey(account => account.Guid);
            modelBuilder.Entity<Account>()
                .HasAlternateKey(account => account.UserName);
            modelBuilder.Entity<Account>()
                .Property(account => account.CreateTime).ValueGeneratedOnAdd();
            modelBuilder.Entity<Account>()
                .Property(account => account.UpdateTime).ValueGeneratedOnAddOrUpdate();
        }
        public DbSet<Account> Accounts { get; set; }

    }
}
