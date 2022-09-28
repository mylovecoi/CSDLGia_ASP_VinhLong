using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class AddThamDinhGia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThamDinhGia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diadiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ppthamdinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mucdich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvyeucau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sotbkl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hosotdgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguonvon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Songaykq = table.Column<int>(type: "int", nullable: false),
                    Tttstd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thaotac = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThamDinhGia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThamDinhGiaCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mats = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tents = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dacdiempl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongsokt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguongoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguyengiadenghi = table.Column<double>(type: "float", nullable: false),
                    Giadenghi = table.Column<double>(type: "float", nullable: false),
                    Nguyengiathamdinh = table.Column<double>(type: "float", nullable: false),
                    Giaththamdinh = table.Column<double>(type: "float", nullable: false),
                    Giakththamdinh = table.Column<double>(type: "float", nullable: false),
                    Giatritstd = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThamDinhGiaCt", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThamDinhGia");

            migrationBuilder.DropTable(
                name: "ThamDinhGiaCt");
        }
    }
}
