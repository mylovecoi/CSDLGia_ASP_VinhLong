using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CSDLGia_ASP.Migrations
{
    public partial class AddHangHoa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaThueTaiNguyenDmExcel");

            migrationBuilder.CreateTable(
                name: "DmNhomHh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tennhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmNhomHh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaHhDvkDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Matt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahhdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenhhdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dacdiemkt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Xuatxu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaHhDvkDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaHhDvkNhom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tentt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaHhDvkNhom", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DmNhomHh");

            migrationBuilder.DropTable(
                name: "GiaHhDvkDm");

            migrationBuilder.DropTable(
                name: "GiaHhDvkNhom");

            migrationBuilder.CreateTable(
                name: "GiaThueTaiNguyenDmExcel",
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
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sapxep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaThueTaiNguyenDmExcel", x => x.Id);
                });
        }
    }
}
