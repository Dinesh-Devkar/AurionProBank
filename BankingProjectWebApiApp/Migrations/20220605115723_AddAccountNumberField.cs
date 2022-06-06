using Microsoft.EntityFrameworkCore.Migrations;

namespace BankingProjectWebApiApp.Migrations
{
    public partial class AddAccountNumberField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountNumber",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "Accounts");
        }
    }
}
