using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSDLGia_ASP.Migrations
{
    /// <inheritdoc />
    public partial class updatedm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "STTHienthi",
                table: "DmThuocTinh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STTSapxep",
                table: "DmThuocTinh",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "DmThuocTinh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STTHienthi",
                table: "DmPhuongPhapThamDinh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STTSapxep",
                table: "DmPhuongPhapThamDinh",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "DmPhuongPhapThamDinh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STTHienthi",
                table: "DmPhanLoaiTaiSan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STTSapxep",
                table: "DmPhanLoaiTaiSan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "DmPhanLoaiTaiSan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STTHienthi",
                table: "DmPhanLoaiNhomHangHoaDichVu",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STTSapxep",
                table: "DmPhanLoaiNhomHangHoaDichVu",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "DmPhanLoaiNhomHangHoaDichVu",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STTHienthi",
                table: "DmPhanLoaiKhuVuc",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STTSapxep",
                table: "DmPhanLoaiKhuVuc",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "DmPhanLoaiKhuVuc",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STTHienthi",
                table: "DmPhanLoaiHangHoaDichVu",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STTSapxep",
                table: "DmPhanLoaiHangHoaDichVu",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "DmPhanLoaiHangHoaDichVu",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STTHienthi",
                table: "DmPhanLoaiGiaDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STTSapxep",
                table: "DmPhanLoaiGiaDat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "DmPhanLoaiGiaDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STTHienthi",
                table: "DmPhanLoaiGia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STTSapxep",
                table: "DmPhanLoaiGia",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "DmPhanLoaiGia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STTHienthi",
                table: "DmLoaiHinhDoanhNghiep",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STTSapxep",
                table: "DmLoaiHinhDoanhNghiep",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "DmLoaiHinhDoanhNghiep",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STTHienthi",
                table: "DmLinhVuc",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STTSapxep",
                table: "DmLinhVuc",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "DmLinhVuc",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STTHienthi",
                table: "DmGiaTriTaiSan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STTSapxep",
                table: "DmGiaTriTaiSan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "DmGiaTriTaiSan",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "STTHienthi",
                table: "DmThuocTinh");

            migrationBuilder.DropColumn(
                name: "STTSapxep",
                table: "DmThuocTinh");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "DmThuocTinh");

            migrationBuilder.DropColumn(
                name: "STTHienthi",
                table: "DmPhuongPhapThamDinh");

            migrationBuilder.DropColumn(
                name: "STTSapxep",
                table: "DmPhuongPhapThamDinh");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "DmPhuongPhapThamDinh");

            migrationBuilder.DropColumn(
                name: "STTHienthi",
                table: "DmPhanLoaiTaiSan");

            migrationBuilder.DropColumn(
                name: "STTSapxep",
                table: "DmPhanLoaiTaiSan");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "DmPhanLoaiTaiSan");

            migrationBuilder.DropColumn(
                name: "STTHienthi",
                table: "DmPhanLoaiNhomHangHoaDichVu");

            migrationBuilder.DropColumn(
                name: "STTSapxep",
                table: "DmPhanLoaiNhomHangHoaDichVu");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "DmPhanLoaiNhomHangHoaDichVu");

            migrationBuilder.DropColumn(
                name: "STTHienthi",
                table: "DmPhanLoaiKhuVuc");

            migrationBuilder.DropColumn(
                name: "STTSapxep",
                table: "DmPhanLoaiKhuVuc");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "DmPhanLoaiKhuVuc");

            migrationBuilder.DropColumn(
                name: "STTHienthi",
                table: "DmPhanLoaiHangHoaDichVu");

            migrationBuilder.DropColumn(
                name: "STTSapxep",
                table: "DmPhanLoaiHangHoaDichVu");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "DmPhanLoaiHangHoaDichVu");

            migrationBuilder.DropColumn(
                name: "STTHienthi",
                table: "DmPhanLoaiGiaDat");

            migrationBuilder.DropColumn(
                name: "STTSapxep",
                table: "DmPhanLoaiGiaDat");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "DmPhanLoaiGiaDat");

            migrationBuilder.DropColumn(
                name: "STTHienthi",
                table: "DmPhanLoaiGia");

            migrationBuilder.DropColumn(
                name: "STTSapxep",
                table: "DmPhanLoaiGia");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "DmPhanLoaiGia");

            migrationBuilder.DropColumn(
                name: "STTHienthi",
                table: "DmLoaiHinhDoanhNghiep");

            migrationBuilder.DropColumn(
                name: "STTSapxep",
                table: "DmLoaiHinhDoanhNghiep");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "DmLoaiHinhDoanhNghiep");

            migrationBuilder.DropColumn(
                name: "STTHienthi",
                table: "DmLinhVuc");

            migrationBuilder.DropColumn(
                name: "STTSapxep",
                table: "DmLinhVuc");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "DmLinhVuc");

            migrationBuilder.DropColumn(
                name: "STTHienthi",
                table: "DmGiaTriTaiSan");

            migrationBuilder.DropColumn(
                name: "STTSapxep",
                table: "DmGiaTriTaiSan");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "DmGiaTriTaiSan");
        }
    }
}
