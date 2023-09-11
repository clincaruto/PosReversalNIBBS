using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosReversalNIBBS_API.Migrations
{
    /// <inheritdoc />
    public partial class addedextracolumnofterminalchannel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "terminal_channel",
                table: "ExcelResponses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "terminal_channel",
                table: "ExcelResponses");
        }
    }
}
