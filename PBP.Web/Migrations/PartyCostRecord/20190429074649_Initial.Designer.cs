﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PBP.Web.Models.Context;

namespace PBP.Web.Migrations.PartyCostRecord
{
    [DbContext(typeof(PartyCostRecordContext))]
    [Migration("20190429074649_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PBP.Web.Models.Domain.PartyCostRecord", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BeginTime");

                    b.Property<DateTime>("CreateTime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("EndTime");

                    b.Property<string>("PartyCostID");

                    b.Property<string>("PartyMemberID");

                    b.Property<string>("PartyMemberName");

                    b.Property<decimal>("PayAmount");

                    b.Property<int>("PayFunc");

                    b.Property<DateTime>("PayTime");

                    b.Property<int>("State");

                    b.Property<DateTime>("UpdateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GETDATE()");

                    b.HasKey("Guid");

                    b.ToTable("PartyCostRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
