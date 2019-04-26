using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PBP.Web.Migrations.PartyMember
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PartyMembers",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    PartyMemberID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Sex = table.Column<int>(nullable: false),
                    IDCard = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: false),
                    National = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Adress = table.Column<string>(nullable: true),
                    Stage = table.Column<int>(nullable: false),
                    BeginDate = table.Column<DateTime>(nullable: false),
                    PrepareDate = table.Column<DateTime>(nullable: false),
                    FormalDate = table.Column<DateTime>(nullable: false),
                    OrgID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyMembers", x => x.Guid);
                    table.UniqueConstraint("AK_PartyMembers_PartyMemberID", x => x.PartyMemberID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartyMembers");
        }
    }
}
