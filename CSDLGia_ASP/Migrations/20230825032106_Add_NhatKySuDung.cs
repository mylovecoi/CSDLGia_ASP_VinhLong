using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class Add_NhatKySuDung : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dschucnang1st");

            migrationBuilder.DropTable(
                name: "Dschucnang2nd");

            migrationBuilder.DropTable(
                name: "Dschucnang3rd");

            migrationBuilder.CreateTable(
                name: "NhatKySuDung",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diachitruycap = table.Column<string>(type: "nvarchar(45)", nullable: true),
                    Nguoisudung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendangnhap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoigian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Chucnang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hanhdong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Noidung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhatKySuDung", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NhatKySuDung");

            migrationBuilder.CreateTable(
                name: "Dschucnang1st",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Chucnang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Maso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dschucnang1st", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dschucnang2nd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Chucnang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Machucnang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dschucnang2nd", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dschucnang3rd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Chucnang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Machucnang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Machucnang2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dschucnang3rd", x => x.Id);
                });
        }
    }
}
