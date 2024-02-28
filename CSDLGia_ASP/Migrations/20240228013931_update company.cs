using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updatecompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KkGiaVlXd");

            migrationBuilder.DropTable(
                name: "KkGiaVlXdDm");

            migrationBuilder.AddColumn<double>(
                name: "VlXd",
                table: "Company",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VlXd",
                table: "Company");

            migrationBuilder.CreateTable(
                name: "KkGiaVlXd",
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
                    table.PrimaryKey("PK_KkGiaVlXd", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaVlXdDm",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tennhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaVlXdDm", x => x.id);
                });
        }
    }
}
