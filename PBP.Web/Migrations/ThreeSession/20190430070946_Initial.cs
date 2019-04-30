using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PBP.Web.Migrations.ThreeSession
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThreeSessions",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    UpdateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    Type = table.Column<int>(nullable: false),
                    Theme = table.Column<string>(nullable: true),
                    PrimaryCoverage = table.Column<string>(nullable: true),
                    ShouldArrive = table.Column<int>(nullable: false),
                    TrueTo = table.Column<int>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Person = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreeSessions", x => x.Guid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThreeSessions");
        }
    }
}
