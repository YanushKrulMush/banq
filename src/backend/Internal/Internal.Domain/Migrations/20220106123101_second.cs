using Microsoft.EntityFrameworkCore.Migrations;

namespace Internal.Domain.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "description",
                table: "transactions",
                newName: "title");

            migrationBuilder.AddColumn<string>(
                name: "recipient_account_number",
                table: "transactions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "recipient_address",
                table: "transactions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "recipient_name",
                table: "transactions",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "recipient_account_number",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "recipient_address",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "recipient_name",
                table: "transactions");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "transactions",
                newName: "description");
        }
    }
}
