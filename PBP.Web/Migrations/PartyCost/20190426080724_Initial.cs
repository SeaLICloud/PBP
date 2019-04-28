using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PBP.Web.Migrations.PartyCost
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PartyCosts",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    UpdateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    PartyCostID = table.Column<string>(nullable: false),
                    PartyMemberID = table.Column<string>(nullable: true),
                    PostWage = table.Column<decimal>(nullable: false),
                    SalaryRankWage = table.Column<decimal>(nullable: false),
                    Allowance = table.Column<decimal>(nullable: false),
                    PerformanceWage = table.Column<decimal>(nullable: false),
                    UnionCost = table.Column<decimal>(nullable: false),
                    MedicalInsurance = table.Column<decimal>(nullable: false),
                    UnemploymentInsurance = table.Column<decimal>(nullable: false),
                    OldAgeInsurance = table.Column<decimal>(nullable: false),
                    JobAnnuity = table.Column<decimal>(nullable: false),
                    IndividualIncomeTax = table.Column<decimal>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    CostBase = table.Column<decimal>(nullable: false),
                    Payable = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyCosts", x => x.Guid);
                    table.UniqueConstraint("AK_PartyCosts_PartyCostID", x => x.PartyCostID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartyCosts");
        }
    }
}
