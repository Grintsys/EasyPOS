using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Grintsys.EasyPOS.Migrations
{
    public partial class Added_PaymentMethods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDebitNotes_AppOrders_OrderId",
                table: "AppDebitNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPaymentMethods_AppPaymentMethodTypes_PaymentMethodTypeId",
                table: "AppPaymentMethods");

            migrationBuilder.DropTable(
                name: "AppPaymentMethodTypes");

            migrationBuilder.DropIndex(
                name: "IX_AppPaymentMethods_OrderId",
                table: "AppPaymentMethods");

            migrationBuilder.DropIndex(
                name: "IX_AppPaymentMethods_PaymentMethodTypeId",
                table: "AppPaymentMethods");

            migrationBuilder.DropIndex(
                name: "IX_AppDebitNotes_OrderId",
                table: "AppDebitNotes");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "AppPaymentMethods");

            migrationBuilder.DropColumn(
                name: "PaymentMethodTypeId",
                table: "AppPaymentMethods");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "AppDebitNotes");

            migrationBuilder.CreateTable(
                name: "AppBankChecks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Total = table.Column<float>(type: "real", nullable: false),
                    Bank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_AppBankChecks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppBankChecks_AppPaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "AppPaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppCash",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Total = table.Column<float>(type: "real", nullable: false),
                    PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_AppCash", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCash_AppPaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "AppPaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppCreditDebitCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Total = table.Column<float>(type: "real", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidThru = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CertificateRetentionNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_AppCreditDebitCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCreditDebitCards_AppPaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "AppPaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppWireTransfers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Account = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total = table.Column<float>(type: "real", nullable: false),
                    PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_AppWireTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppWireTransfers_AppPaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "AppPaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppPaymentMethods_OrderId",
                table: "AppPaymentMethods",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppBankChecks_PaymentMethodId",
                table: "AppBankChecks",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCash_PaymentMethodId",
                table: "AppCash",
                column: "PaymentMethodId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppCreditDebitCards_PaymentMethodId",
                table: "AppCreditDebitCards",
                column: "PaymentMethodId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppWireTransfers_PaymentMethodId",
                table: "AppWireTransfers",
                column: "PaymentMethodId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppBankChecks");

            migrationBuilder.DropTable(
                name: "AppCash");

            migrationBuilder.DropTable(
                name: "AppCreditDebitCards");

            migrationBuilder.DropTable(
                name: "AppWireTransfers");

            migrationBuilder.DropIndex(
                name: "IX_AppPaymentMethods_OrderId",
                table: "AppPaymentMethods");

            migrationBuilder.AddColumn<float>(
                name: "Amount",
                table: "AppPaymentMethods",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentMethodTypeId",
                table: "AppPaymentMethods",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "AppDebitNotes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AppPaymentMethodTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPaymentMethodTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppPaymentMethods_OrderId",
                table: "AppPaymentMethods",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPaymentMethods_PaymentMethodTypeId",
                table: "AppPaymentMethods",
                column: "PaymentMethodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDebitNotes_OrderId",
                table: "AppDebitNotes",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDebitNotes_AppOrders_OrderId",
                table: "AppDebitNotes",
                column: "OrderId",
                principalTable: "AppOrders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPaymentMethods_AppPaymentMethodTypes_PaymentMethodTypeId",
                table: "AppPaymentMethods",
                column: "PaymentMethodTypeId",
                principalTable: "AppPaymentMethodTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
