using Microsoft.EntityFrameworkCore.Migrations;

namespace Parser.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Danger",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    SourceOfThreat = table.Column<string>(type: "TEXT", nullable: true),
                    Object = table.Column<string>(type: "TEXT", nullable: true),
                    BreachOfConfidentiality = table.Column<bool>(type: "INTEGER", nullable: false),
                    IntegrityViolation = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessibilityViolation = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Danger", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Danger");
        }
    }
}
