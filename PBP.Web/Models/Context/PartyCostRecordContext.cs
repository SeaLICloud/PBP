using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PBP.Web.Models.Domain;

namespace PBP.Web.Models.Context
{
    public class PartyCostRecordContext:DbContext
    {
        public PartyCostRecordContext(DbContextOptions<PartyCostRecordContext> options) : base(options)
        {
        }
        public DbSet<PartyCostRecord> PartyCostRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PartyCostRecord>()
                .HasKey(pCR => pCR.Guid);

            modelBuilder.Entity<PartyCostRecord>()
                .Property(pCR => pCR.CreateTime)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<PartyCostRecord>()
                .Property(pCR => pCR.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()")
                .Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            modelBuilder.Entity<PartyCostRecord>()
                .Property(pCR => pCR.PartyMemberID);

            modelBuilder.Entity<PartyCostRecord>()
                .Property(pCR => pCR.PartyMemberName);

            modelBuilder.Entity<PartyCostRecord>()
                .Property(pCR => pCR.PartyCostID);

            modelBuilder.Entity<PartyCostRecord>()
                .Property(pCR => pCR.PayTime);

            modelBuilder.Entity<PartyCostRecord>()
                .Property(pCR => pCR.PayFunc);

            modelBuilder.Entity<PartyCostRecord>()
                .Property(pCR => pCR.PayAmount);

            modelBuilder.Entity<PartyCostRecord>()
                .Property(pCR => pCR.BeginTime);

            modelBuilder.Entity<PartyCostRecord>()
                .Property(pCR => pCR.EndTime);

            modelBuilder.Entity<PartyCostRecord>()
                .Property(pCR => pCR.State);
        }
    }
}
