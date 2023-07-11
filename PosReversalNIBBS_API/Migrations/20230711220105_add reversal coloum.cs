using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosReversalNIBBS_API.Migrations
{
    /// <inheritdoc />
    public partial class addreversalcoloum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reversal",
                table: "ExcelResponses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reversal",
                table: "ExcelResponses");
        }
    }
}
