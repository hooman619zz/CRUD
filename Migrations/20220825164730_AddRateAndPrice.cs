using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudTest.Migrations
{
    public partial class AddRateAndPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                schema: "Book",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rate",
                schema: "Book",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                schema: "Book",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "Rate",
                schema: "Book",
                table: "Book");
        }
    }
}
