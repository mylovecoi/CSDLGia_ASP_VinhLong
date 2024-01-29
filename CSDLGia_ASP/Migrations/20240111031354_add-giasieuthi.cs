using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class addgiasieuthi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GiaHangHoaTaiSieuThi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Tunam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dennam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaHangHoaTaiSieuThi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaHangHoaTaiSieuThiCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madoituong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doituongsd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Namchuathue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giachuathue = table.Column<double>(type: "float", nullable: false),
                    Namchuathue1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giachuathue1 = table.Column<double>(type: "float", nullable: false),
                    Namchuathue2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giachuathue2 = table.Column<double>(type: "float", nullable: false),
                    Namchuathue3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giachuathue3 = table.Column<double>(type: "float", nullable: false),
                    Namchuathue4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giachuathue4 = table.Column<double>(type: "float", nullable: false),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giacothue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phibvmttyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phibvmt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thanhtien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaHangHoaTaiSieuThiCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaHangHoaTaiSieuThiDmHHTaiSieuThi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Madoituong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doituongsd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaHangHoaTaiSieuThiDmHHTaiSieuThi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaHangHoaTaiSieuThiDmSieuThi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Madoituong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doituongsd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaHangHoaTaiSieuThiDmSieuThi", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaHangHoaTaiSieuThi");

            migrationBuilder.DropTable(
                name: "GiaHangHoaTaiSieuThiCt");

            migrationBuilder.DropTable(
                name: "GiaHangHoaTaiSieuThiDmHHTaiSieuThi");

            migrationBuilder.DropTable(
                name: "GiaHangHoaTaiSieuThiDmSieuThi");
        }
    }
}
