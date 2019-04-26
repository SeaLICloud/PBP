using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PBP.Web.Models.Domain;

namespace PBP.Web.Models.Context
{
    public class OrganizationContext:DbContext
    {
        public OrganizationContext(DbContextOptions<OrganizationContext> options) : base(options)
        {
        }
        public DbSet<Organization> Organizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>()
                .HasKey(org => org.Guid);

            modelBuilder.Entity<Organization>()
                .HasAlternateKey(org => org.OrgID);

            modelBuilder.Entity<Organization>()
                .Property(org => org.Name);

            modelBuilder.Entity<Organization>()
                .Property(org => org.ShortName);

            modelBuilder.Entity<Organization>()
                .Property(org => org.OrgType);
        }
    }
}
