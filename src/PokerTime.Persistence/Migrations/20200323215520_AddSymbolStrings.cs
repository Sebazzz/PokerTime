using Microsoft.EntityFrameworkCore.Migrations;

namespace PokerTime.Persistence.Migrations {
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public partial class AddSymbolStrings : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AlterColumn<int>(
                name: "ValueAsNumber",
                table: "Symbol",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");


            // Set number value to NULL where symbol isn't a number
            migrationBuilder.Sql("UPDATE Symbol SET ValueAsNumber = NULL WHERE Type <> 0");

            migrationBuilder.AddColumn<string>(
                name: "ValueAsString",
                table: "Symbol",
                nullable: false,
                defaultValue: "!");

            // Migrate numbers, set the string value
            void MigrateNumber(int num) {
                migrationBuilder.Sql($"UPDATE Symbol SET ValueAsString = '{num}' WHERE ValueAsNumber = {num}");
            }

            MigrateNumber(0);
            MigrateNumber(1);
            MigrateNumber(2);
            MigrateNumber(3);
            MigrateNumber(5);
            MigrateNumber(8);
            MigrateNumber(13);
            MigrateNumber(20);
            MigrateNumber(40);
            MigrateNumber(100);

            // Migrate existing symbols
            // ... "Unknown"
            migrationBuilder.Sql("UPDATE Symbol SET ValueAsString = '?' WHERE Type = 3");

            // ... "Break"
            if (migrationBuilder.IsSqlServer()) migrationBuilder.Sql("UPDATE Symbol SET ValueAsString = N'☕' WHERE Type = 2");
            if (!migrationBuilder.IsSqlServer()) migrationBuilder.Sql("UPDATE Symbol SET ValueAsString = '☕' WHERE Type = 2");

            // ... "Infinite"
            if (migrationBuilder.IsSqlServer()) migrationBuilder.Sql("UPDATE Symbol SET ValueAsString = N'∞' WHERE Type = 1");
            if (!migrationBuilder.IsSqlServer()) migrationBuilder.Sql("UPDATE Symbol SET ValueAsString = '∞' WHERE Type = 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                name: "ValueAsString",
                table: "Symbol");

            migrationBuilder.AlterColumn<int>(
                name: "ValueAsNumber",
                table: "Symbol",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
