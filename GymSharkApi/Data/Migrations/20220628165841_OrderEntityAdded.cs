using Microsoft.EntityFrameworkCore.Migrations;

namespace GymSharkAPI.Data.Migrations
{
    public partial class OrderEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    SourceUsertId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderedProductId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => new { x.SourceUsertId, x.OrderedProductId });
                    table.ForeignKey(
                        name: "FK_Orders_Products_OrderedProductId",
                        column: x => x.OrderedProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_SourceUsertId",
                        column: x => x.SourceUsertId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderedProductId",
                table: "Orders",
                column: "OrderedProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
