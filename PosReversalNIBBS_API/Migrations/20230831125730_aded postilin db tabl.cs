using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosReversalNIBBS_API.Migrations
{
    /// <inheritdoc />
    public partial class adedpostilindbtabl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "postilionDBs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ACCOUNT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    card_acceptor_id_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    terminal_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    system_trace_audit_nr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    retrieval_reference_nr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    transaction_amount = table.Column<double>(type: "float", nullable: false),
                    datetime_req = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_postilionDBs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "postilionDBs");
        }
    }
}
