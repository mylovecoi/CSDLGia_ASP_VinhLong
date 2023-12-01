using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CSDLGia_ASP.Migrations
{
    public partial class FixKkgiaKkgiact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KeKhaiGia");

            migrationBuilder.DropTable(
                name: "KeKhaiGiaCt");

            migrationBuilder.CreateTable(
                name: "KkGia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Manghe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dtll = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manghe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaCt", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KkGia");

            migrationBuilder.DropTable(
                name: "KkGiaCt");

            migrationBuilder.CreateTable(
                name: "KeKhaiGia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhanLoaiGia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeKhaiGia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeKhaiGiaCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeKhaiGiaCt", x => x.Id);
                });
        }
    }
}
