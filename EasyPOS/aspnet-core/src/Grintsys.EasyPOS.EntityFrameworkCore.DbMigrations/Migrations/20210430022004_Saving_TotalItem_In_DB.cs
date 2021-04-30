using Microsoft.EntityFrameworkCore.Migrations;

namespace Grintsys.EasyPOS.Migrations
{
    public partial class Saving_TotalItem_In_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "TotalItem",
                table: "AppOrderItems",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TotalItem",
                table: "AppDebitNoteItems",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TotalItem",
                table: "AppCreditNoteItems",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalItem",
                table: "AppOrderItems");

            migrationBuilder.DropColumn(
                name: "TotalItem",
                table: "AppDebitNoteItems");

            migrationBuilder.DropColumn(
                name: "TotalItem",
                table: "AppCreditNoteItems");
        }
    }
}
