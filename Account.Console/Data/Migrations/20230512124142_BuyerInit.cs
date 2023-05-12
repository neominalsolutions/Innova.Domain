using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Account.Console.Data.Migrations
{
    public partial class BuyerInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "BuyerContext");

            migrationBuilder.AddColumn<string>(
                name: "BuyerId",
                schema: "OrderContext",
                table: "Order",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId1",
                schema: "OrderContext",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Buyer",
                schema: "BuyerContext",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buyer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_BuyerId",
                schema: "OrderContext",
                table: "Order",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId1",
                schema: "OrderContext",
                table: "Order",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Buyer_BuyerId",
                schema: "OrderContext",
                table: "Order",
                column: "BuyerId",
                principalSchema: "BuyerContext",
                principalTable: "Buyer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerId1",
                schema: "OrderContext",
                table: "Order",
                column: "CustomerId1",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Buyer_BuyerId",
                schema: "OrderContext",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId1",
                schema: "OrderContext",
                table: "Order");

            migrationBuilder.DropTable(
                name: "Buyer",
                schema: "BuyerContext");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Order_BuyerId",
                schema: "OrderContext",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_CustomerId1",
                schema: "OrderContext",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                schema: "OrderContext",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                schema: "OrderContext",
                table: "Order");
        }
    }
}
