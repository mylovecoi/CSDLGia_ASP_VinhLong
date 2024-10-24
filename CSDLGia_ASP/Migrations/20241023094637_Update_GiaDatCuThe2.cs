using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSDLGia_ASP.Migrations
{
    /// <inheritdoc />
    public partial class Update_GiaDatCuThe2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaDatCuThe");

            migrationBuilder.DropTable(
                name: "GiaDatCuTheCt");

            migrationBuilder.DropTable(
                name: "GiaDatCuTheDmPPDGDat");

            migrationBuilder.DropTable(
                name: "GiaDatCuTheDmPPDGDatCt");

            migrationBuilder.CreateTable(
                name: "GiaDatCuTheVl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vitri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maloaidat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dientich = table.Column<double>(type: "float", nullable: false),
                    Giatri = table.Column<double>(type: "float", nullable: false),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thaotac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinhtrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDatCuTheVl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDatCuTheVlCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChiPhiNhanCong = table.Column<double>(type: "float", nullable: false),
                    ChiPhiDungCu = table.Column<double>(type: "float", nullable: false),
                    ChiPhiNangLuong = table.Column<double>(type: "float", nullable: false),
                    ChiPhiKhauHao = table.Column<double>(type: "float", nullable: false),
                    ChiPhiVatLieu = table.Column<double>(type: "float", nullable: false),
                    ChiPhiTrucTiepKkh = table.Column<double>(type: "float", nullable: false),
                    ChiPhiTrucTiepCkh = table.Column<double>(type: "float", nullable: false),
                    ChiPhiQlChungKkh = table.Column<double>(type: "float", nullable: false),
                    ChiPhiQlChungCkh = table.Column<double>(type: "float", nullable: false),
                    DonGiaKkh = table.Column<double>(type: "float", nullable: false),
                    DonGiaCkh = table.Column<double>(type: "float", nullable: false),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaDiaBan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaXaPhuong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STTSapXep = table.Column<int>(type: "int", nullable: false),
                    STTHienThi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDatCuTheVlCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDatCuTheVlDmPPDGDat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mapp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenpp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDatCuTheVlDmPPDGDat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDatCuTheVlDmPPDGDatCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mapp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SapXep = table.Column<double>(type: "float", nullable: false),
                    HienThi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Noidungcv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NhapGia = table.Column<bool>(type: "bit", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDatCuTheVlDmPPDGDatCt", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaDatCuTheVl");

            migrationBuilder.DropTable(
                name: "GiaDatCuTheVlCt");

            migrationBuilder.DropTable(
                name: "GiaDatCuTheVlDmPPDGDat");

            migrationBuilder.DropTable(
                name: "GiaDatCuTheVlDmPPDGDatCt");

            migrationBuilder.CreateTable(
                name: "GiaDatCuThe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dientich = table.Column<double>(type: "float", nullable: false),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giatri = table.Column<double>(type: "float", nullable: false),
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
                    Maloaidat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thaotac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinhtrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Vitri = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDatCuThe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDatCuTheCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChiPhiDungCu = table.Column<double>(type: "float", nullable: false),
                    ChiPhiKhauHao = table.Column<double>(type: "float", nullable: false),
                    ChiPhiNangLuong = table.Column<double>(type: "float", nullable: false),
                    ChiPhiNhanCong = table.Column<double>(type: "float", nullable: false),
                    ChiPhiQlChungCkh = table.Column<double>(type: "float", nullable: false),
                    ChiPhiQlChungKkh = table.Column<double>(type: "float", nullable: false),
                    ChiPhiTrucTiepCkh = table.Column<double>(type: "float", nullable: false),
                    ChiPhiTrucTiepKkh = table.Column<double>(type: "float", nullable: false),
                    ChiPhiVatLieu = table.Column<double>(type: "float", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DonGiaCkh = table.Column<double>(type: "float", nullable: false),
                    DonGiaKkh = table.Column<double>(type: "float", nullable: false),
                    MaDiaBan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaXaPhuong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STTHienThi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STTSapXep = table.Column<int>(type: "int", nullable: false),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDatCuTheCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDatCuTheDmPPDGDat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Mapp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenpp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDatCuTheDmPPDGDat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDatCuTheDmPPDGDatCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HienThi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mapp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NhapGia = table.Column<bool>(type: "bit", nullable: false),
                    Noidungcv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SapXep = table.Column<double>(type: "float", nullable: false),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDatCuTheDmPPDGDatCt", x => x.Id);
                });
        }
    }
}
