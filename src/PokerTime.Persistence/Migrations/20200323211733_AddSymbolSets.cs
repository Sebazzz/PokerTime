using Microsoft.EntityFrameworkCore.Migrations;

namespace PokerTime.Persistence.Migrations {
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public partial class AddSymbolSets : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "SymbolSet",
                columns: table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_SymbolSet", x => x.Id);
                });

            if (migrationBuilder.IsSqlServer()) {
                migrationBuilder.Sql(@"
IF EXISTS(SELECT 1 FROM Session)
    INSERT INTO SymbolSet (Name) VALUES ('Default');
");
            }

            if (migrationBuilder.IsSqlite()) {
                migrationBuilder.Sql("INSERT INTO SymbolSet (Id, Name) VALUES (1, 'Default')");
            }

            migrationBuilder.AddColumn<int>(
                name: "SymbolSetId",
                table: "Symbol",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "SymbolSetId",
                table: "Session",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Symbol_SymbolSetId",
                table: "Symbol",
                column: "SymbolSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_SymbolSetId",
                table: "Session",
                column: "SymbolSetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_SymbolSet_SymbolSetId",
                table: "Session",
                column: "SymbolSetId",
                principalTable: "SymbolSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Symbol_SymbolSet_SymbolSetId",
                table: "Symbol",
                column: "SymbolSetId",
                principalTable: "SymbolSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_SymbolSet_SymbolSetId",
                table: "Session");

            migrationBuilder.DropForeignKey(
                name: "FK_Symbol_SymbolSet_SymbolSetId",
                table: "Symbol");

            migrationBuilder.DropTable(
                name: "SymbolSet");

            migrationBuilder.DropIndex(
                name: "IX_Symbol_SymbolSetId",
                table: "Symbol");

            migrationBuilder.DropIndex(
                name: "IX_Session_SymbolSetId",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "SymbolSetId",
                table: "Symbol");

            migrationBuilder.DropColumn(
                name: "SymbolSetId",
                table: "Session");
        }
    }
}
