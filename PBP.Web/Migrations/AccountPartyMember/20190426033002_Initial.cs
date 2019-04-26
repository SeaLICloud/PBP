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
                    AccountID = table.Column<string>(nullable: false),
                    PartyMemberID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountPartyMembers", x => x.Guid);
                    table.UniqueConstraint("AK_AccountPartyMembers_AccountID", x => x.AccountID);
                    table.UniqueConstraint("AK_AccountPartyMembers_PartyMemberID", x => x.PartyMemberID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountPartyMembers");
        }
    }
}
