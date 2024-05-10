using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvaloniaTemplate.Migrations
{
    /// <inheritdoc />
    public partial class AnimalTypeFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AnimalTypes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "AnimalTypes");
        }
    }
}
