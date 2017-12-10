using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Bitcoin.Migrations
{
    public partial class AddTxIdKeyAndAccountField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionDetails_Transactions_TransactionId",
                table: "TransactionDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_TransactionDetails_TransactionId",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "TransactionDetails");

            migrationBuilder.AlterColumn<string>(
                name: "TxId",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Account",
                table: "TransactionDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionTxId",
                table: "TransactionDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "TxId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_TransactionTxId",
                table: "TransactionDetails",
                column: "TransactionTxId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionDetails_Transactions_TransactionTxId",
                table: "TransactionDetails",
                column: "TransactionTxId",
                principalTable: "Transactions",
                principalColumn: "TxId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionDetails_Transactions_TransactionTxId",
                table: "TransactionDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_TransactionDetails_TransactionTxId",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "Account",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "TransactionTxId",
                table: "TransactionDetails");

            migrationBuilder.AlterColumn<string>(
                name: "TxId",
                table: "Transactions",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Transactions",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "TransactionDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_TransactionId",
                table: "TransactionDetails",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionDetails_Transactions_TransactionId",
                table: "TransactionDetails",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
