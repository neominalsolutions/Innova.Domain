using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Account.Console.Data.Migrations
{
    public partial class BuyerInit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Buyer",
                schema: "BuyerContext",
                newName: "Buyer",
                newSchema: "OrderContext");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "BuyerContext");

            migrationBuilder.RenameTable(
                name: "Buyer",
                schema: "OrderContext",
                newName: "Buyer",
                newSchema: "BuyerContext");
        }
    }
}
