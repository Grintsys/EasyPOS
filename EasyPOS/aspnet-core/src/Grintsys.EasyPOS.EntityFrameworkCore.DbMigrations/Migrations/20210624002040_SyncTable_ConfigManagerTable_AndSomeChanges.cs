using Microsoft.EntityFrameworkCore.Migrations;

namespace Grintsys.EasyPOS.Migrations
{
    public partial class SyncTable_ConfigManagerTable_AndSomeChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "AppWarehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Taxes",
                table: "AppProducts",
                type: "bit",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<int>(
                name: "SalesPersonId",
                table: "AppOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WarehouseCode",
                table: "AppOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalesPersonId",
                table: "AppDebitNotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WarehouseCode",
                table: "AppDebitNotes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalesPersonId",
                table: "AppCreditNotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WarehouseCode",
                table: "AppCreditNotes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "AppWarehouses");

            migrationBuilder.DropColumn(
                name: "SalesPersonId",
                table: "AppOrders");

            migrationBuilder.DropColumn(
                name: "WarehouseCode",
                table: "AppOrders");

            migrationBuilder.DropColumn(
                name: "SalesPersonId",
                table: "AppDebitNotes");

            migrationBuilder.DropColumn(
                name: "WarehouseCode",
                table: "AppDebitNotes");

            migrationBuilder.DropColumn(
                name: "SalesPersonId",
                table: "AppCreditNotes");

            migrationBuilder.DropColumn(
                name: "WarehouseCode",
                table: "AppCreditNotes");

            migrationBuilder.AlterColumn<float>(
                name: "Taxes",
                table: "AppProducts",
                type: "real",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
