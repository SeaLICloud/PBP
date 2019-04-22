using Microsoft.EntityFrameworkCore;
using PBP.Web.Models.Domain;

namespace PBP.Web.Models.Context
{
    public class PartyMemberContext: DbContext
    {
        public PartyMemberContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<PartyMember> PartyMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PartyMember>()
                .HasKey(pM => pM.Guid);

            modelBuilder.Entity<PartyMember>()
                .HasAlternateKey(pM => pM.PartyMemberID);

            modelBuilder.Entity<PartyMember>()
                .Property(pM => pM.Name);

            modelBuilder.Entity<PartyMember>()
                .Property(pM => pM.Sex);

            modelBuilder.Entity<PartyMember>()
                .Property(pM => pM.IDCard);

            modelBuilder.Entity<PartyMember>()
                .Property(pM => pM.Birthday);

            modelBuilder.Entity<PartyMember>()
                .Property(pM => pM.National);

            modelBuilder.Entity<PartyMember>()
                .Property(pM => pM.Phone);

            modelBuilder.Entity<PartyMember>()
                .Property(pM => pM.Adress);

            modelBuilder.Entity<PartyMember>()
                .Property(pM => pM.Adress);

            modelBuilder.Entity<PartyMember>()
                .Property(pM => pM.Stage);

            modelBuilder.Entity<PartyMember>()
                .Property(pM => pM.BeginDate);

            modelBuilder.Entity<PartyMember>()
                .Property(pM => pM.PrepareDate);

            modelBuilder.Entity<PartyMember>()
                .Property(pM => pM.FormalDate);

            modelBuilder.Entity<PartyMember>()
                .Property(pM => pM.OrgID);                  
        }
    }
}
