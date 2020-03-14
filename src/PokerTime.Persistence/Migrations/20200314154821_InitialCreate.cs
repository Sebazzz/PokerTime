using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PokerTime.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PredefinedParticipantColor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Color_R = table.Column<byte>(nullable: true),
                    Color_G = table.Column<byte>(nullable: true),
                    Color_B = table.Column<byte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredefinedParticipantColor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlId_StringId = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    CurrentStage = table.Column<int>(nullable: false),
                    HashedPassphrase = table.Column<string>(unicode: false, fixedLength: true, maxLength: 64, nullable: true),
                    Title = table.Column<string>(maxLength: 256, nullable: false),
                    FacilitatorHashedPassphrase = table.Column<string>(unicode: false, fixedLength: true, maxLength: 64, nullable: false),
                    CreationTimestamp = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color_R = table.Column<byte>(nullable: true),
                    Color_G = table.Column<byte>(nullable: true),
                    Color_B = table.Column<byte>(nullable: true),
                    SessionId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    IsFacilitator = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participant_Session_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Session",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participant_SessionId",
                table: "Participant",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_UrlId_StringId",
                table: "Session",
                column: "UrlId_StringId",
                unique: true,
                filter: "[UrlId_StringId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participant");

            migrationBuilder.DropTable(
                name: "PredefinedParticipantColor");

            migrationBuilder.DropTable(
                name: "Session");
        }
    }
}
