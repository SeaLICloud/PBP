﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PBP.Web.Models.Context;

namespace PBP.Web.Migrations.Organization
{
    [DbContext(typeof(OrganizationContext))]
    [Migration("20190421042708_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PBP.Web.Models.Domain.Organization", b =>
                {
                    b.Property<string>("OrgID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid>("Guid");

                    b.Property<string>("Name");

                    b.Property<int>("OrgType");

                    b.Property<string>("ShortName");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("OrgID");

                    b.HasAlternateKey("Guid");

                    b.ToTable("Organizations");
                });
#pragma warning restore 612, 618
        }
    }
}
