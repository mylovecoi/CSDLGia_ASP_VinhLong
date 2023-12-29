using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CSDLGia_ASP.Migrations
{
    public partial class updatecpi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Trangthai",
                table: "GiaHhDvCnCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Trangthai",
                table: "GiaDatPhanLoaiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Giavt1",
                table: "GiaDatDiaBanCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Giavt2",
                table: "GiaDatDiaBanCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Giavt3",
                table: "GiaDatDiaBanCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Giavt4",
                table: "GiaDatDiaBanCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Giavt5",
                table: "GiaDatDiaBanCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Trangthai",
                table: "GiaDatDiaBanCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChiSoGiaTd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtinbc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giagoc = table.Column<double>(type: "float", nullable: false),
                    Giakychon = table.Column<double>(type: "float", nullable: false),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diaphuong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipt1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipt2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipt3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipt4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipt5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_ChiSoGiaTd", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChiSoGiaTdDd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Noidung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giagoc = table.Column<double>(type: "float", nullable: false),
                    Giakychon = table.Column<double>(type: "float", nullable: false),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diaphuong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiSoGiaTdDd", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChiSoGiaTdDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Masonhomhanghoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Masohanghoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenhanghoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Masogoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Baocao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diaphuong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuyensoTt = table.Column<double>(type: "float", nullable: false),
                    QuyensoNt = table.Column<double>(type: "float", nullable: false),
                    Gia = table.Column<double>(type: "float", nullable: false),
                    Thang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiSoGiaTdDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChiSoGiaTdDmCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Masohanghoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Masonhomhanghoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenhanghoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Masogoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stt = table.Column<int>(type: "int", nullable: false),
                    SttBc = table.Column<int>(type: "int", nullable: false),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Baocao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giagoc = table.Column<double>(type: "float", nullable: false),
                    Giakychon = table.Column<double>(type: "float", nullable: false),
                    QuyensoTt = table.Column<double>(type: "float", nullable: false),
                    QuyensoNt = table.Column<double>(type: "float", nullable: false),
                    Thang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiSoGiaTdDmCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChiSoGiaTdDmCtDd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Masohanghoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Masonhomhanghoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenhanghoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Masogoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stt = table.Column<int>(type: "int", nullable: false),
                    SttBc = table.Column<int>(type: "int", nullable: false),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Baocao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giagoc = table.Column<double>(type: "float", nullable: false),
                    Giakychon = table.Column<double>(type: "float", nullable: false),
                    QuyensoTt = table.Column<double>(type: "float", nullable: false),
                    QuyensoNt = table.Column<double>(type: "float", nullable: false),
                    Thang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiSoGiaTdDmCtDd", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChiSoGiaTdHh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Masonhomhanghoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Masohanghoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenhanghoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Masogoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gia = table.Column<double>(type: "float", nullable: false),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiSoGiaTdHh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChiSoGiaTdHhCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Masohanghoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Masonhomhanghoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenhanghoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Masogoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giagoc = table.Column<double>(type: "float", nullable: false),
                    Giakychon = table.Column<double>(type: "float", nullable: false),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiSoGiaTdHhCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmLoaiDat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maloaidat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Loaidat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmLoaiDat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaHhHaiQuanXnk",
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
                    table.PrimaryKey("PK_GiaHhHaiQuanXnk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaHhHaiQuanXnkCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenHh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiaTruocThue = table.Column<double>(type: "float", nullable: false),
                    PhanTramThue = table.Column<double>(type: "float", nullable: false),
                    GiaSauThue = table.Column<double>(type: "float", nullable: false),
                    MaThue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaHhHaiQuanXnkCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaHhHaiQuanXnkDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tennhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaHhHaiQuanXnkDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaHhHaiQuanXnkThue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaThue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenThue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaHhHaiQuanXnkThue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaTaiSanTths",
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
                    table.PrimaryKey("PK_GiaTaiSanTths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaTaiSanTthsCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mataisan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tentaisan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaisanTd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DacdiemKt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinhtrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dactinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giabanbuon = table.Column<double>(type: "float", nullable: false),
                    Giabanle = table.Column<double>(type: "float", nullable: false),
                    Phantram = table.Column<double>(type: "float", nullable: false),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaTaiSanTthsCt", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiSoGiaTd");

            migrationBuilder.DropTable(
                name: "ChiSoGiaTdDd");

            migrationBuilder.DropTable(
                name: "ChiSoGiaTdDm");

            migrationBuilder.DropTable(
                name: "ChiSoGiaTdDmCt");

            migrationBuilder.DropTable(
                name: "ChiSoGiaTdDmCtDd");

            migrationBuilder.DropTable(
                name: "ChiSoGiaTdHh");

            migrationBuilder.DropTable(
                name: "ChiSoGiaTdHhCt");

            migrationBuilder.DropTable(
                name: "DmLoaiDat");

            migrationBuilder.DropTable(
                name: "GiaHhHaiQuanXnk");

            migrationBuilder.DropTable(
                name: "GiaHhHaiQuanXnkCt");

            migrationBuilder.DropTable(
                name: "GiaHhHaiQuanXnkDm");

            migrationBuilder.DropTable(
                name: "GiaHhHaiQuanXnkThue");

            migrationBuilder.DropTable(
                name: "GiaTaiSanTths");

            migrationBuilder.DropTable(
                name: "GiaTaiSanTthsCt");

            migrationBuilder.DropColumn(
                name: "Trangthai",
                table: "GiaHhDvCnCt");

            migrationBuilder.DropColumn(
                name: "Trangthai",
                table: "GiaDatPhanLoaiCt");

            migrationBuilder.DropColumn(
                name: "Giavt1",
                table: "GiaDatDiaBanCt");

            migrationBuilder.DropColumn(
                name: "Giavt2",
                table: "GiaDatDiaBanCt");

            migrationBuilder.DropColumn(
                name: "Giavt3",
                table: "GiaDatDiaBanCt");

            migrationBuilder.DropColumn(
                name: "Giavt4",
                table: "GiaDatDiaBanCt");

            migrationBuilder.DropColumn(
                name: "Giavt5",
                table: "GiaDatDiaBanCt");

            migrationBuilder.DropColumn(
                name: "Trangthai",
                table: "GiaDatDiaBanCt");
        }
    }
}
