using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosReversalNIBBS_API.Migrations
{
    /// <inheritdoc />
    public partial class Adtales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UploadedExcelDetails",
                columns: table => new
                {
                    BatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateUploaded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalTransaction = table.Column<double>(type: "float", nullable: true),
                    TotalAmount = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedExcelDetails", x => x.BatchId);
                });

            migrationBuilder.CreateTable(
                name: "ExcelResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TERMINAL_ID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MERCHANT_ID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AMOUNT = table.Column<double>(type: "float", nullable: false),
                    STAN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RRN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PAN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TRANSACTION_DATE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PROCESSOR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BANK = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ACCOUNT_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadedExcelDetailBatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExcelResponses_UploadedExcelDetails_UploadedExcelDetailBatchId",
                        column: x => x.UploadedExcelDetailBatchId,
                        principalTable: "UploadedExcelDetails",
                        principalColumn: "BatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExcelResponses_UploadedExcelDetailBatchId",
                table: "ExcelResponses",
                column: "UploadedExcelDetailBatchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcelResponses");

            migrationBuilder.DropTable(
                name: "UploadedExcelDetails");
        }
    }
}
