using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class addthgiahhdvk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GiaHhDvkCtTh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahhdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychotbc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tenhhdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dacdiemkt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Xuatxu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Gia = table.Column<double>(type: "float", nullable: false),
                    Loaigia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguontt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaHhDvkCtTh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaHhDvkTh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Matt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sobc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaybc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngaychotbc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ttbc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahstonghop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaHhDvkTh", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaHhDvkCtTh");

            migrationBuilder.DropTable(
                name: "GiaHhDvkTh");
        }
    }
}
