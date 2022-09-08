using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class AddDinhGia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KkCuocVcHk");

            migrationBuilder.DropTable(
                name: "KkGiaCatSan");

            migrationBuilder.DropTable(
                name: "KkGiaDatSanLap");

            migrationBuilder.DropTable(
                name: "KkGiaDaXayDung");

            migrationBuilder.DropTable(
                name: "KkGiaDvCang");

            migrationBuilder.DropTable(
                name: "KkGiaDvCh");

            migrationBuilder.DropTable(
                name: "KkGiaDvDlBb");

            migrationBuilder.DropTable(
                name: "KkGiaDvHdTm");

            migrationBuilder.DropTable(
                name: "KkGiaEtanol");

            migrationBuilder.DropTable(
                name: "KkGiaGiay");

            migrationBuilder.DropTable(
                name: "KkGiaHpLx");

            migrationBuilder.DropTable(
                name: "KkGiaKcbTn");

            migrationBuilder.DropTable(
                name: "KkGiaOtoNkSx");

            migrationBuilder.DropTable(
                name: "KkGiaSach");

            migrationBuilder.DropTable(
                name: "KkGiaTaCn");

            migrationBuilder.DropTable(
                name: "KkGiaThan");

            migrationBuilder.DropTable(
                name: "KkGiaVeTqKdl");

            migrationBuilder.DropTable(
                name: "KkGiaVtXb");

            migrationBuilder.DropTable(
                name: "KkGiaVtXk");

            migrationBuilder.DropTable(
                name: "KkGiaVtXtx");

            migrationBuilder.DropTable(
                name: "KkGiaXeMayNkSx");

            migrationBuilder.DropTable(
                name: "KkGiaXmTxd");

            migrationBuilder.DropTable(
                name: "KkGs");

            migrationBuilder.AddColumn<string>(
                name: "Maloaidat",
                table: "GiaDatPhanLoaiDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GiaRungDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tennhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenvitri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dientich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giatri = table.Column<double>(type: "float", nullable: false),
                    Mahuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaRungDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaThueMatDatMatNuoc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaThueMatDatMatNuoc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaThueMatDatMatNuocCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diemdau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diemcuoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vitri = table.Column<int>(type: "int", nullable: false),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dientich = table.Column<double>(type: "float", nullable: false),
                    Dongia = table.Column<double>(type: "float", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaThueMatDatMatNuocCt", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaRungDm");

            migrationBuilder.DropTable(
                name: "GiaThueMatDatMatNuoc");

            migrationBuilder.DropTable(
                name: "GiaThueMatDatMatNuocCt");

            migrationBuilder.DropColumn(
                name: "Maloaidat",
                table: "GiaDatPhanLoaiDm");

            migrationBuilder.CreateTable(
                name: "KkCuocVcHk",
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
                    table.PrimaryKey("PK_KkCuocVcHk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaCatSan",
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
                    table.PrimaryKey("PK_KkGiaCatSan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaDatSanLap",
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
                    table.PrimaryKey("PK_KkGiaDatSanLap", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaDaXayDung",
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
                    table.PrimaryKey("PK_KkGiaDaXayDung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaDvCang",
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
                    table.PrimaryKey("PK_KkGiaDvCang", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaDvCh",
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
                    table.PrimaryKey("PK_KkGiaDvCh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaDvDlBb",
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
                    table.PrimaryKey("PK_KkGiaDvDlBb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaDvHdTm",
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
                    table.PrimaryKey("PK_KkGiaDvHdTm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaEtanol",
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
                    table.PrimaryKey("PK_KkGiaEtanol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaGiay",
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
                    table.PrimaryKey("PK_KkGiaGiay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaHpLx",
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
                    table.PrimaryKey("PK_KkGiaHpLx", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaKcbTn",
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
                    table.PrimaryKey("PK_KkGiaKcbTn", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaOtoNkSx",
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
                    table.PrimaryKey("PK_KkGiaOtoNkSx", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaSach",
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
                    table.PrimaryKey("PK_KkGiaSach", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaTaCn",
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
                    table.PrimaryKey("PK_KkGiaTaCn", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaThan",
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
                    table.PrimaryKey("PK_KkGiaThan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaVeTqKdl",
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
                    table.PrimaryKey("PK_KkGiaVeTqKdl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaVtXb",
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
                    table.PrimaryKey("PK_KkGiaVtXb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaVtXk",
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
                    table.PrimaryKey("PK_KkGiaVtXk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaVtXtx",
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
                    table.PrimaryKey("PK_KkGiaVtXtx", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaXeMayNkSx",
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
                    table.PrimaryKey("PK_KkGiaXeMayNkSx", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaXmTxd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dtll = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_KkGiaXmTxd", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGs",
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
                    table.PrimaryKey("PK_KkGs", x => x.Id);
                });
        }
    }
}
