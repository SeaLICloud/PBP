using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PBP.Web.Migrations.Organization
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    OrgID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    OrgType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.OrgID);
                    table.UniqueConstraint("AK_Organizations_Guid", x => x.Guid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
