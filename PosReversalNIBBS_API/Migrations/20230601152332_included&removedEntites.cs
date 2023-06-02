using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosReversalNIBBS_API.Migrations
{
    /// <inheritdoc />
    public partial class includedremovedEntites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bin",
                table: "ExcelResponses");

            migrationBuilder.DropColumn(
                name: "Original_Data_Element",
                table: "ExcelResponses");

            migrationBuilder.DropColumn(
                name: "Processing_Code",
                table: "ExcelResponses");

            migrationBuilder.DropColumn(
                name: "Response_code",
                table: "ExcelResponses");

            migrationBuilder.RenameColumn(
                name: "System_Trace_Number",
                table: "ExcelResponses",
                newName: "Stan");

            migrationBuilder.RenameColumn(
                name: "Retrieval_Ref_Number",
                table: "ExcelResponses",
                newName: "RRN");

            migrationBuilder.RenameColumn(
                name: "Processor_Name",
                table: "ExcelResponses",
                newName: "Processor");

            migrationBuilder.RenameColumn(
                name: "IssuingBankName",
                table: "ExcelResponses",
                newName: "Bank");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stan",
                table: "ExcelResponses",
                newName: "System_Trace_Number");

            migrationBuilder.RenameColumn(
                name: "RRN",
                table: "ExcelResponses",
                newName: "Retrieval_Ref_Number");

            migrationBuilder.RenameColumn(
                name: "Processor",
                table: "ExcelResponses",
                newName: "Processor_Name");

            migrationBuilder.RenameColumn(
                name: "Bank",
                table: "ExcelResponses",
                newName: "IssuingBankName");

            migrationBuilder.AddColumn<int>(
                name: "Bin",
                table: "ExcelResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Original_Data_Element",
                table: "ExcelResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Processing_Code",
                table: "ExcelResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Response_code",
                table: "ExcelResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
