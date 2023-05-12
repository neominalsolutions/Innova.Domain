using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Account.Console.Data.Migrations
{
    public partial class BuyerInit3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Order_CustomerId1",
                schema: "OrderContext",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "OrderContext",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                schema: "OrderContext",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "BuyerId",
                schema: "OrderContext",
                table: "Order",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Buyer_BuyerId",
                schema: "OrderContext",
                table: "Order",
                column: "BuyerId",
                principalSchema: "OrderContext",
                principalTable: "Buyer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Buyer_BuyerId",
                schema: "OrderContext",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "BuyerId",
                schema: "OrderContext",
                table: "Order",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                schema: "OrderContext",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId1",
                schema: "OrderContext",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_Order_CustomerId1",
                schema: "OrderContext",
                table: "Order",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Buyer_BuyerId",
                schema: "OrderContext",
                table: "Order",
                column: "BuyerId",
                principalSchema: "OrderContext",
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
    }
}
