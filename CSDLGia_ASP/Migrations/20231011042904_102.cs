using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class _102 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CayTrongVatNuoiCt");

            migrationBuilder.CreateTable(
                name: "GiaCayTrongVatNuoiCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gia = table.Column<double>(type: "float", nullable: false),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaCayTrongVatNuoiCt", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaCayTrongVatNuoiCt");

            migrationBuilder.CreateTable(
                name: "CayTrongVatNuoiCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cap1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gia = table.Column<double>(type: "float", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CayTrongVatNuoiCt", x => x.Id);
                });
        }
    }
}
