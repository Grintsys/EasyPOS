using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Grintsys.EasyPOS.Migrations
{
    public partial class Order_Relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrders_AppCustomers_CustomerId",
                table: "AppOrders");

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

            migrationBuilder.CreateIndex(
                name: "IX_AppDebitNotes_OrderId",
                table: "AppDebitNotes",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCreditNotes_OrderId",
                table: "AppCreditNotes",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCreditNotes_AppOrders_OrderId",
                table: "AppCreditNotes",
                column: "OrderId",
                principalTable: "AppOrders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDebitNotes_AppOrders_OrderId",
                table: "AppDebitNotes",
                column: "OrderId",
                principalTable: "AppOrders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrders_AppCustomers_CustomerId",
                table: "AppOrders",
                column: "CustomerId",
                principalTable: "AppCustomers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCreditNotes_AppOrders_OrderId",
                table: "AppCreditNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_AppDebitNotes_AppOrders_OrderId",
                table: "AppDebitNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_AppOrders_AppCustomers_CustomerId",
                table: "AppOrders");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrders_AppCustomers_CustomerId",
                table: "AppOrders",
                column: "CustomerId",
                principalTable: "AppCustomers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
