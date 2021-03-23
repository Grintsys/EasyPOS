using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Grintsys.EasyPOS.Migrations
{
    public partial class AddedPaymentMethodsAndOrderRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "AppDebitNotes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "AppCreditNotes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AppPaymentMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Method = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPaymentMethods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPaymentMethods_AppOrders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "AppOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppDebitNotes_OrderId",
                table: "AppDebitNotes",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCreditNotes_OrderId",
                table: "AppCreditNotes",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPaymentMethods_OrderId",
                table: "AppPaymentMethods",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCreditNotes_AppOrders_OrderId",
                table: "AppCreditNotes",
                column: "OrderId",
                principalTable: "AppOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppDebitNotes_AppOrders_OrderId",
                table: "AppDebitNotes",
                column: "OrderId",
                principalTable: "AppOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCreditNotes_AppOrders_OrderId",
                table: "AppCreditNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_AppDebitNotes_AppOrders_OrderId",
                table: "AppDebitNotes");

            migrationBuilder.DropTable(
                name: "AppPaymentMethods");

            migrationBuilder.DropIndex(
                name: "IX_AppDebitNotes_OrderId",
                table: "AppDebitNotes");

            migrationBuilder.DropIndex(
                name: "IX_AppCreditNotes_OrderId",
                table: "AppCreditNotes");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "AppDebitNotes");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "AppCreditNotes");
        }
    }
}
