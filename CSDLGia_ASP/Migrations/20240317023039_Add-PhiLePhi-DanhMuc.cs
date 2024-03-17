using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class AddPhiLePhiDanhMuc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "SapXep",
                table: "PhiLePhiCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "PhiLePhiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PhiLePhiDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HienTrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Magoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capdo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SapXep = table.Column<double>(type: "float", nullable: false),
                    HienThi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhiLePhiDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhiLePhiNhom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tennhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhiLePhiNhom", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhiLePhiDm");

            migrationBuilder.DropTable(
                name: "PhiLePhiNhom");

            migrationBuilder.DropColumn(
                name: "SapXep",
                table: "PhiLePhiCt");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "PhiLePhiCt");
        }
    }
}
