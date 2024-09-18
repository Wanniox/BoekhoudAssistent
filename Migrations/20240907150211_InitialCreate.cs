using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoekhoudAssistent.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BKFP",
                columns: table => new
                {
                    BUKRS = table.Column<int>(type: "int", nullable: false),
                    BELNR = table.Column<int>(type: "int", nullable: false),
                    GJAHR = table.Column<int>(type: "int", nullable: false),
                    BLART = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BLDAT = table.Column<DateOnly>(type: "date", nullable: false),
                    BUDAT = table.Column<DateOnly>(type: "date", nullable: false),
                    MONAT = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    CPUDT = table.Column<DateOnly>(type: "date", nullable: false),
                    CPUTM = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BKFP", x => new { x.BUKRS, x.BELNR, x.GJAHR });
                });

            migrationBuilder.CreateTable(
                name: "BSEG",
                columns: table => new
                {
                    BUKRS = table.Column<int>(type: "int", nullable: false),
                    BELNR = table.Column<int>(type: "int", nullable: false),
                    GJAHR = table.Column<int>(type: "int", nullable: false),
                    BUZEI = table.Column<int>(type: "int", nullable: false),
                    BUZID = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    AUGDT = table.Column<DateOnly>(type: "date", nullable: false),
                    AUGCP = table.Column<DateOnly>(type: "date", nullable: false),
                    AUGBL = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BSCHL = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BSEG", x => new { x.BUKRS, x.BELNR, x.GJAHR, x.BUZEI });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BKFP");

            migrationBuilder.DropTable(
                name: "BSEG");
        }
    }
}
