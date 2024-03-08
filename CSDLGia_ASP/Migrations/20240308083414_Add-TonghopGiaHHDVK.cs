using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class AddTonghopGiaHHDVK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Madv",
                table: "GiaHhDvkTh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tendv",
                table: "GiaHhDvkTh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ipf_excel",
                table: "GiaHhDvkTh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ipf_excel_base64",
                table: "GiaHhDvkTh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ipf_pdf",
                table: "GiaHhDvkTh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ipf_pdf_base64",
                table: "GiaHhDvkTh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ipf_word",
                table: "GiaHhDvkTh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ipf_word_base64",
                table: "GiaHhDvkTh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CoSoKinhDoanhDVLT",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    macskd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    masothue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tencskd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    loaihang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    diachikd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telkd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    toado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoSoKinhDoanhDVLT", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DoanhNghiepDVLT",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tendn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    masothue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    diachidn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    teldn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    faxdn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    noidknopthue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    giayphepkd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    chucdanhky = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nguoiky = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    diadanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tailieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cqcq = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoanhNghiepDVLT", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "HoSoKeKhaiGia",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    macskd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    masothue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: true),
                    socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ngaycvlk = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ngaychuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tendn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tencskd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    loaihang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    giaycnhangcs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    filedk1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    filedk2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    filedk3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    soqdgiaycnhangcs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    giaycnhangcstungay = table.Column<DateTime>(type: "datetime2", nullable: true),
                    giaycnhangcsdenngaypublic = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoSoKeKhaiGia", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "HoSoKeKhaiGia_ChiTiet",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    macskd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    maloaip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    loaip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sohieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mucgialk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mucgiakk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tendoituong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    apdungpublic = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoSoKeKhaiGia_ChiTiet", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoSoKinhDoanhDVLT");

            migrationBuilder.DropTable(
                name: "DoanhNghiepDVLT");

            migrationBuilder.DropTable(
                name: "HoSoKeKhaiGia");

            migrationBuilder.DropTable(
                name: "HoSoKeKhaiGia_ChiTiet");

            migrationBuilder.DropColumn(
                name: "Madv",
                table: "GiaHhDvkTh");

            migrationBuilder.DropColumn(
                name: "Tendv",
                table: "GiaHhDvkTh");

            migrationBuilder.DropColumn(
                name: "ipf_excel",
                table: "GiaHhDvkTh");

            migrationBuilder.DropColumn(
                name: "ipf_excel_base64",
                table: "GiaHhDvkTh");

            migrationBuilder.DropColumn(
                name: "ipf_pdf",
                table: "GiaHhDvkTh");

            migrationBuilder.DropColumn(
                name: "ipf_pdf_base64",
                table: "GiaHhDvkTh");

            migrationBuilder.DropColumn(
                name: "ipf_word",
                table: "GiaHhDvkTh");

            migrationBuilder.DropColumn(
                name: "ipf_word_base64",
                table: "GiaHhDvkTh");
        }
    }
}
