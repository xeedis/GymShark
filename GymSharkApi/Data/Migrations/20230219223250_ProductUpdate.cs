using Microsoft.EntityFrameworkCore.Migrations;

namespace GymSharkAPI.Data.Migrations
{
    public partial class ProductUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutCompany",
                table: "Products",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecifiedFor",
                table: "Products",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutCompany",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SpecifiedFor",
                table: "Products");
        }
    }
}
