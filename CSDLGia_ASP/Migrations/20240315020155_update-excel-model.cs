using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updateexcelmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaXayDungMoi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaXayDungMoi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaVatLieuXayDung",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaVatLieuXayDung",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaVangNgoaiTe",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaVangNgoaiTe",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaTroGiaTroCuoc",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaTroGiaTroCuoc",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaThueTaiSanCong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaThueTaiSanCong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaThueTaiNguyen",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaThueTaiNguyen",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaThueNhaSV",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaThueNhaSV",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaThueNhaCongVu",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaThueNhaCongVu",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaThueMuaNhaXh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaThueMuaNhaXh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaThueMatDatMatNuoc",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaThueMatDatMatNuoc",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaThiTruong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaThiTruong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaTaiSanTths",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaTaiSanTths",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaTaiSanCong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaTaiSanCong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaSpDvToiDa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaSpDvToiDa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaSpDvKhungGia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaSpDvKhungGia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaSpDvCuThe",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaSpDvCuThe",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaSpDvCongIch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaSpDvCongIch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaSpDvCi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaSpDvCi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaRung",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaRung",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaPhiLePhi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaPhiLePhi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaPhiChuyenGia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaPhiChuyenGia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaNuocSh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaNuocSh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaMuaTaiSan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaMuaTaiSan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaLpTbNha",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaLpTbNha",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaKhungGiaDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaKhungGiaDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaHhHaiQuanXnk",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaHhHaiQuanXnk",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaHhDvk",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaHhDvk",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaHhDvCn",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaHhDvCn",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaHangHoaTaiSieuThi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaHangHoaTaiSieuThi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaGocVlxdTh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaGocVlxdTh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaGiaoDichDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaGiaoDichDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaGiaoDichBDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaGiaoDichBDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaDvKcb",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaDvKcb",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaDvGdDt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaDvGdDt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaDauGiaDatTs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaDauGiaDatTs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaDauGiaDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaDauGiaDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaDatThiTruong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaDatThiTruong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaDatPhanLoai",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaDatPhanLoai",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaDatDuAn",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaDatDuAn",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaDatDiaBan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaDatDiaBan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaCuocVanChuyen",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaCuocVanChuyen",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaCayTrongVatNuoi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaCayTrongVatNuoi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaBanNhaTaiDinhCu",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaBanNhaTaiDinhCu",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaXayDungMoi");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaXayDungMoi");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaVatLieuXayDung");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaVatLieuXayDung");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaVangNgoaiTe");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaVangNgoaiTe");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaTroGiaTroCuoc");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaTroGiaTroCuoc");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaThueTaiSanCong");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaThueTaiSanCong");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaThueTaiNguyen");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaThueTaiNguyen");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaThueNhaSV");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaThueNhaSV");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaThueNhaCongVu");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaThueNhaCongVu");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaThueMuaNhaXh");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaThueMuaNhaXh");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaThueMatDatMatNuoc");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaThueMatDatMatNuoc");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaThiTruong");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaThiTruong");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaTaiSanTths");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaTaiSanTths");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaTaiSanCong");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaTaiSanCong");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaSpDvToiDa");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaSpDvToiDa");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaSpDvKhungGia");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaSpDvKhungGia");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaSpDvCuThe");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaSpDvCuThe");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaSpDvCongIch");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaSpDvCongIch");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaSpDvCi");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaSpDvCi");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaRung");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaRung");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaPhiLePhi");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaPhiLePhi");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaPhiChuyenGia");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaPhiChuyenGia");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaNuocSh");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaNuocSh");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaMuaTaiSan");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaMuaTaiSan");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaLpTbNha");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaLpTbNha");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaKhungGiaDat");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaKhungGiaDat");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaHhHaiQuanXnk");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaHhHaiQuanXnk");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaHhDvk");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaHhDvk");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaHhDvCn");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaHhDvCn");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaHangHoaTaiSieuThi");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaHangHoaTaiSieuThi");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaGocVlxdTh");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaGocVlxdTh");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaGiaoDichDat");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaGiaoDichDat");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaGiaoDichBDS");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaGiaoDichBDS");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaDvKcb");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaDvKcb");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaDvGdDt");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaDvGdDt");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaDauGiaDatTs");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaDauGiaDatTs");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaDauGiaDat");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaDauGiaDat");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaDatThiTruong");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaDatThiTruong");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaDatPhanLoai");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaDatPhanLoai");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaDatDuAn");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaDatDuAn");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaDatDiaBan");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaDatDiaBan");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaCuocVanChuyen");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaCuocVanChuyen");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaCayTrongVatNuoi");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaCayTrongVatNuoi");

            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaBanNhaTaiDinhCu");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaBanNhaTaiDinhCu");
        }
    }
}
