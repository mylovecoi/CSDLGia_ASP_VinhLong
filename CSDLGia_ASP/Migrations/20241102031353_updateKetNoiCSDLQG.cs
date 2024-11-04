using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSDLGia_ASP.Migrations
{
    /// <inheritdoc />
    public partial class updateKetNoiCSDLQG : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaBM",
                table: "KetNoiAPI_DanhSach",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaDiaBan",
                table: "KetNoiAPI_DanhSach",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaDonVi",
                table: "KetNoiAPI_DanhSach",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiDuyet",
                table: "KetNoiAPI_DanhSach",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiTao",
                table: "KetNoiAPI_DanhSach",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaBM",
                table: "KetNoiAPI_DanhSach");

            migrationBuilder.DropColumn(
                name: "MaDiaBan",
                table: "KetNoiAPI_DanhSach");

            migrationBuilder.DropColumn(
                name: "MaDonVi",
                table: "KetNoiAPI_DanhSach");

            migrationBuilder.DropColumn(
                name: "NguoiDuyet",
                table: "KetNoiAPI_DanhSach");

            migrationBuilder.DropColumn(
                name: "NguoiTao",
                table: "KetNoiAPI_DanhSach");
        }
    }
}
