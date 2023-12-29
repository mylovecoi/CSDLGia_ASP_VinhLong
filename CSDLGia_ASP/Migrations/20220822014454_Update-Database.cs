using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CSDLGia_ASP.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KkGiaCt");

            migrationBuilder.DropTable(
                name: "KkGiaDvLt");

            migrationBuilder.AddColumn<string>(
                name: "Chinhsachkm",
                table: "KkGia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "KkGia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "KkGia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Macqcq1",
                table: "KkGia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Macqcq2",
                table: "KkGia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Macskd",
                table: "KkGia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Maxp",
                table: "KkGia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ptnguyennhan",
                table: "KkGia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "KkGiaDvLtCskd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Macskd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tencskd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Loaihang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diachikd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telkd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaDvLtCskd", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KkGiaDvLtCskd");

            migrationBuilder.DropColumn(
                name: "Chinhsachkm",
                table: "KkGia");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "KkGia");

            migrationBuilder.DropColumn(
                name: "Fax",
                table: "KkGia");

            migrationBuilder.DropColumn(
                name: "Macqcq1",
                table: "KkGia");

            migrationBuilder.DropColumn(
                name: "Macqcq2",
                table: "KkGia");

            migrationBuilder.DropColumn(
                name: "Macskd",
                table: "KkGia");

            migrationBuilder.DropColumn(
                name: "Maxp",
                table: "KkGia");

            migrationBuilder.DropColumn(
                name: "Ptnguyennhan",
                table: "KkGia");

            migrationBuilder.CreateTable(
                name: "KkGiaCt",
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
                    Manghe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaDvLt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Chinhsachkm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dtll = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macskd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngaycvlk = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ptnguyennhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaDvLt", x => x.Id);
                });
        }
    }
}
