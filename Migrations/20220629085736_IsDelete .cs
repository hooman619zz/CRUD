using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudTest.Migrations
{
    public partial class IsDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Person",
                table: "Book",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Book",
                table: "Book",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Person",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Book",
                table: "Book");
        }
    }
}
