using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class UpdateGiaHhDvk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GiaHhDvk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Matt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiemlk = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Soqdlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinhtrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaHhDvk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaHhDvkCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahhdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Gia = table.Column<double>(type: "float", nullable: false),
                    Loaigia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguontt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaHhDvkCt", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaHhDvk");

            migrationBuilder.DropTable(
                name: "GiaHhDvkCt");
        }
    }
}
