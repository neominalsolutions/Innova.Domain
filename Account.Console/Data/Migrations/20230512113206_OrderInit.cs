using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Account.Console.Data.Migrations
{
    public partial class OrderInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "OrderContext");

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "OrderContext",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ship_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ship_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ship_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order_Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order_Code = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                schema: "OrderContext",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "OrderContext",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                schema: "OrderContext",
                table: "OrderItem",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem",
                schema: "OrderContext");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "OrderContext");
        }
    }
}
