using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PBP.Web.Migrations.AccountPartyMember
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountPartyMembers",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    UpdateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    AccountID = table.Column<string>(nullable: true),
                    PartyMemberID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountPartyMembers", x => x.Guid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountPartyMembers");
        }
    }
}
