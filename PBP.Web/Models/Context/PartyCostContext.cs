using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PBP.Web.Models.Domain;

namespace PBP.Web.Models.Context
{
    public class PartyCostContext: DbContext
    {
        public PartyCostContext(DbContextOptions<PartyCostContext> options) : base(options)
        {
        }
        public DbSet<PartyCost> PartyCosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PartyCost>()
                .HasKey(pC => pC.Guid);

            modelBuilder.Entity<PartyCost>()
                .HasAlternateKey(pC => pC.PartyCostID);

            modelBuilder.Entity<PartyCost>()
                .Property(pC => pC.PartyMemberID);

            modelBuilder.Entity<PartyCost>()
                .Property(pC => pC.PostWage);

            modelBuilder.Entity<PartyCost>()
                .Property(pC => pC.SalaryRankWage);

            modelBuilder.Entity<PartyCost>()
                .Property(pC => pC.Allowance);

            modelBuilder.Entity<PartyCost>()
                .Property(pC => pC.PerformanceWage);

            modelBuilder.Entity<PartyCost>()
                .Property(pC => pC.UnionCost);

            modelBuilder.Entity<PartyCost>()
                .Property(pC => pC.MedicalInsurance);

            modelBuilder.Entity<PartyCost>()
                .Property(pC => pC.UnemploymentInsurance);

            modelBuilder.Entity<PartyCost>()
                .Property(pC => pC.OldAgeInsurance);

            modelBuilder.Entity<PartyCost>()
                .Property(pC => pC.JobAnnuity);

            modelBuilder.Entity<PartyCost>()
                .Property(pC => pC.IndividualIncomeTax);

            modelBuilder.Entity<PartyCost>()
                .Property(pC => pC.CreateTime)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<PartyCost>()
                .Property(pC => pC.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()")
                .Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
        }
    }
}
