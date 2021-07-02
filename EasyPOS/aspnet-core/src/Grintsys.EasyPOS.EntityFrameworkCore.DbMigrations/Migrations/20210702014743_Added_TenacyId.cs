using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Grintsys.EasyPOS.Migrations
{
    public partial class Added_TenacyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppWireTransfers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppWarehouses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppSincronizador",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppProductWarehouses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppProducts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppPaymentMethods",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppOrders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppOrderItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppDebitNotes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppDebitNoteItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppCustomers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppCreditNotes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppCreditNoteItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppCreditDebitCards",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppConfigurationManager",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppCash",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppBankChecks",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppWireTransfers");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppWarehouses");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppSincronizador");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppProductWarehouses");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppPaymentMethods");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppOrders");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppOrderItems");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppDebitNotes");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppDebitNoteItems");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppCustomers");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppCreditNotes");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppCreditNoteItems");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppCreditDebitCards");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppConfigurationManager");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppCash");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppBankChecks");
        }
    }
}
