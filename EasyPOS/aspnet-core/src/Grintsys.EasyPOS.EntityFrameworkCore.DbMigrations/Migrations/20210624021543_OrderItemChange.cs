using Microsoft.EntityFrameworkCore.Migrations;

namespace Grintsys.EasyPOS.Migrations
{
    public partial class OrderItemChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Taxes",
                table: "AppOrderItems",
                type: "bit",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<float>(
                name: "TaxAmount",
                table: "AppOrderItems",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<bool>(
                name: "Taxes",
                table: "AppDebitNoteItems",
                type: "bit",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<float>(
                name: "TaxAmount",
                table: "AppDebitNoteItems",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<bool>(
                name: "Taxes",
                table: "AppCreditNoteItems",
                type: "bit",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<float>(
                name: "TaxAmount",
                table: "AppCreditNoteItems",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxAmount",
                table: "AppOrderItems");

            migrationBuilder.DropColumn(
                name: "TaxAmount",
                table: "AppDebitNoteItems");

            migrationBuilder.DropColumn(
                name: "TaxAmount",
                table: "AppCreditNoteItems");

            migrationBuilder.AlterColumn<float>(
                name: "Taxes",
                table: "AppOrderItems",
                type: "real",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<float>(
                name: "Taxes",
                table: "AppDebitNoteItems",
                type: "real",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<float>(
                name: "Taxes",
                table: "AppCreditNoteItems",
                type: "real",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
