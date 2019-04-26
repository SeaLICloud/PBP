using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PBP.Web.Models.Domain;

namespace PBP.Web.Models.Context
{
    public class AccountPartyMemberContext : DbContext
    {
        public AccountPartyMemberContext(DbContextOptions<AccountPartyMemberContext> options) : base(options)
        {
        }
        public DbSet<AccountPartyMember> AccountPartyMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountPartyMember>()
                .HasKey(aP => aP.Guid);

            modelBuilder.Entity<AccountPartyMember>()
                .HasAlternateKey(aP => aP.AccountID);

            modelBuilder.Entity<AccountPartyMember>()
                .HasAlternateKey(aP => aP.PartyMemberID);

            modelBuilder.Entity<AccountPartyMember>()
                .Property(account => account.CreateTime)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<AccountPartyMember>()
                .Property(account => account.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()")
                .Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
        }
    }
}