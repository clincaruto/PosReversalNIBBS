using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosReversalNIBBS_API.Migrations
{
    /// <inheritdoc />
    public partial class addingfinnle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "finnacleDbs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FORACID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TRAN_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PART_TRAN_TYPE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TRAN_AMT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_finnacleDbs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "finnacleDbs");
        }
    }
}
