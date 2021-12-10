using Microsoft.EntityFrameworkCore.Migrations;

namespace Parser.Migrations
{
    public partial class UBIupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UBID",
                table: "Danger",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UBID",
                table: "Danger");
        }
    }
}
