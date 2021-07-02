using Microsoft.EntityFrameworkCore.Migrations;

namespace Grintsys.EasyPOS.Migrations
{
    public partial class Added_Columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SelectedTax",
                table: "AppOrderItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SelectedTax",
                table: "AppDebitNoteItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SelectedTax",
                table: "AppCreditNoteItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalesPersonCode",
                table: "AbpUsers",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedTax",
                table: "AppOrderItems");

            migrationBuilder.DropColumn(
                name: "SelectedTax",
                table: "AppDebitNoteItems");

            migrationBuilder.DropColumn(
                name: "SelectedTax",
                table: "AppCreditNoteItems");

            migrationBuilder.DropColumn(
                name: "SalesPersonCode",
                table: "AbpUsers");
        }
    }
}
