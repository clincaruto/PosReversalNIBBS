using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosReversalNIBBS_API.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Responses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Terminal_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Merchant_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Processing_Code = table.Column<int>(type: "int", nullable: false),
                    Bin = table.Column<int>(type: "int", nullable: false),
                    Pan = table.Column<int>(type: "int", nullable: false),
                    Response_code = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    System_Trace_Number = table.Column<int>(type: "int", nullable: false),
                    Retrieval_Ref_Number = table.Column<int>(type: "int", nullable: false),
                    IssuingBankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transaction_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Original_Data_Element = table.Column<int>(type: "int", nullable: false),
                    Processor_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Account_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responses", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Responses");
        }
    }
}
