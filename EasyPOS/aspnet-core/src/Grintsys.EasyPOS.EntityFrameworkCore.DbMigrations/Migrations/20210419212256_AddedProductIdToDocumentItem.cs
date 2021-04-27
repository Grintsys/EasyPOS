using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Grintsys.EasyPOS.Migrations
{
    public partial class AddedProductIdToDocumentItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "AppOrderItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "AppDebitNoteItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "AppCreditNoteItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "AppOrderItems");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "AppDebitNoteItems");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "AppCreditNoteItems");
        }
    }
}
