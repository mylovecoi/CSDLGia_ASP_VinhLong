using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class _100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CayTrongVatNuoi");

            migrationBuilder.DropTable(
                name: "CayTrongVatNuoiDm");

            migrationBuilder.DropTable(
                name: "CayTrongVatNuoiNhom");

            migrationBuilder.CreateTable(
                name: "GiaCayTrongVatNuoi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiemlk = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Soqdlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cqbh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_GiaCayTrongVatNuoi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaCayTrongVatNuoiDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sapxep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaCayTrongVatNuoiDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaCayTrongVatNuoiNhom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tennhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sapxep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaCayTrongVatNuoiNhom", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaCayTrongVatNuoi");

            migrationBuilder.DropTable(
                name: "GiaCayTrongVatNuoiDm");

            migrationBuilder.DropTable(
                name: "GiaCayTrongVatNuoiNhom");

            migrationBuilder.CreateTable(
                name: "CayTrongVatNuoi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cqbh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqdlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Thoidiemlk = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinhtrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CayTrongVatNuoi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CayTrongVatNuoiDm",
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
                    table.PrimaryKey("PK_CayTrongVatNuoiDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CayTrongVatNuoiNhom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sapxep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tennhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CayTrongVatNuoiNhom", x => x.Id);
                });
        }
    }
}
