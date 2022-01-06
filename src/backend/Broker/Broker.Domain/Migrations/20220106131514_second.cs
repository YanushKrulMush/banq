using Microsoft.EntityFrameworkCore.Migrations;

namespace Broker.Domain.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_account_stocks_stock_stock_id",
                table: "account_stocks");

            migrationBuilder.DropPrimaryKey(
                name: "pk_stock",
                table: "stock");

            migrationBuilder.RenameTable(
                name: "stock",
                newName: "stocks");

            migrationBuilder.AddPrimaryKey(
                name: "pk_stocks",
                table: "stocks",
                column: "id");

            migrationBuilder.InsertData(
                table: "stocks",
                columns: new[] { "id", "name", "value" },
                values: new object[,]
                {
                    { 1, "Allegro.eu SA", 10.0 },
                    { 2, "Grupa Lotos SA", 20.0 },
                    { 3, "Orange Polska SA", 30.0 }
                });

            migrationBuilder.AddForeignKey(
                name: "fk_account_stocks_stocks_stock_id",
                table: "account_stocks",
                column: "stock_id",
                principalTable: "stocks",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_account_stocks_stocks_stock_id",
                table: "account_stocks");

            migrationBuilder.DropPrimaryKey(
                name: "pk_stocks",
                table: "stocks");

            migrationBuilder.DeleteData(
                table: "stocks",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "stocks",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "stocks",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.RenameTable(
                name: "stocks",
                newName: "stock");

            migrationBuilder.AddPrimaryKey(
                name: "pk_stock",
                table: "stock",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_account_stocks_stock_stock_id",
                table: "account_stocks",
                column: "stock_id",
                principalTable: "stock",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
