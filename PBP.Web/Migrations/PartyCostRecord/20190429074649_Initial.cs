using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PBP.Web.Migrations.PartyCostRecord
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PartyCostRecords",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    UpdateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    PartyMemberID = table.Column<string>(nullable: true),
                    PartyMemberName = table.Column<string>(nullable: true),
                    PartyCostID = table.Column<string>(nullable: true),
                    PayTime = table.Column<DateTime>(nullable: false),
                    PayFunc = table.Column<int>(nullable: false),
                    PayAmount = table.Column<decimal>(nullable: false),
                    BeginTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyCostRecords", x => x.Guid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartyCostRecords");
        }
    }
}
