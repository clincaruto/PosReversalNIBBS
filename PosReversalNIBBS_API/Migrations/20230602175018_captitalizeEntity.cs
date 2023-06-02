using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosReversalNIBBS_API.Migrations
{
    /// <inheritdoc />
    public partial class captitalizeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Transaction_Date",
                table: "ExcelResponses",
                newName: "TRANSACTION_DATE");

            migrationBuilder.RenameColumn(
                name: "Terminal_Id",
                table: "ExcelResponses",
                newName: "TERMINAL_ID");

            migrationBuilder.RenameColumn(
                name: "Stan",
                table: "ExcelResponses",
                newName: "STAN");

            migrationBuilder.RenameColumn(
                name: "Processor",
                table: "ExcelResponses",
                newName: "PROCESSOR");

            migrationBuilder.RenameColumn(
                name: "Pan",
                table: "ExcelResponses",
                newName: "PAN");

            migrationBuilder.RenameColumn(
                name: "Merchant_Id",
                table: "ExcelResponses",
                newName: "MERCHANT_ID");

            migrationBuilder.RenameColumn(
                name: "Bank",
                table: "ExcelResponses",
                newName: "BANK");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "ExcelResponses",
                newName: "AMOUNT");

            migrationBuilder.RenameColumn(
                name: "Account_Id",
                table: "ExcelResponses",
                newName: "ACCOUNT_ID");

            migrationBuilder.AlterColumn<string>(
                name: "TRANSACTION_DATE",
                table: "ExcelResponses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "STAN",
                table: "ExcelResponses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RRN",
                table: "ExcelResponses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PAN",
                table: "ExcelResponses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "AMOUNT",
                table: "ExcelResponses",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TRANSACTION_DATE",
                table: "ExcelResponses",
                newName: "Transaction_Date");

            migrationBuilder.RenameColumn(
                name: "TERMINAL_ID",
                table: "ExcelResponses",
                newName: "Terminal_Id");

            migrationBuilder.RenameColumn(
                name: "STAN",
                table: "ExcelResponses",
                newName: "Stan");

            migrationBuilder.RenameColumn(
                name: "PROCESSOR",
                table: "ExcelResponses",
                newName: "Processor");

            migrationBuilder.RenameColumn(
                name: "PAN",
                table: "ExcelResponses",
                newName: "Pan");

            migrationBuilder.RenameColumn(
                name: "MERCHANT_ID",
                table: "ExcelResponses",
                newName: "Merchant_Id");

            migrationBuilder.RenameColumn(
                name: "BANK",
                table: "ExcelResponses",
                newName: "Bank");

            migrationBuilder.RenameColumn(
                name: "AMOUNT",
                table: "ExcelResponses",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "ACCOUNT_ID",
                table: "ExcelResponses",
                newName: "Account_Id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Transaction_Date",
                table: "ExcelResponses",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Stan",
                table: "ExcelResponses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "RRN",
                table: "ExcelResponses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Pan",
                table: "ExcelResponses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "ExcelResponses",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
